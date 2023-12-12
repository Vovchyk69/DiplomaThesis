using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SimMetrics.Net.Metric;

namespace PlagiarismChecker.Visitor;

public class AstSimilarity {

    public static double CalculateSimilarityScore(Node tree1, Node tree2) {
        int score = CalculateScore(tree1, tree2);
        int totalNodes = CountNodes(tree1);

        double percentageScore = (double) score / totalNodes * 100;

        return percentageScore;
    }
    
    private static int CalculateScore(Node node1, Node node2) {
        if (node1 is null || node2 is null) {
            return 0;
        }
        
        if (node1.Text != node2.Text) {
            return 0;
        }

        if (node1.Children.Count != node2.Children.Count) {
            return 0;
        }

        int score = 1; // Match on node type

        for (int i = 0; i < node1.Children.Count; i++) {
            score += CalculateScore(node1.Children[i], node2.Children[i]);
        }

        return score;
    }

    private static int CountNodes(Node node) {
        int count = 1;
        if (node == null) return count;
        foreach (Node child in node.Children) {
            count += CountNodes(child);
        }

        return count;
    }
    
    static string TokenizeCode(string code)
    {
        var tokens = Regex.Split(code, @"\s+|\b")
            .Where(token => !string.IsNullOrWhiteSpace(token))
            .ToList();

        tokens.RemoveAll(token =>
            token == "public" ||
            token == "private" ||
            token == "protected" ||
            token == "static" ||
            token == "{" ||
            token == "}" ||
            token == "(" ||
            token == ")" ||
            token == ";" ||
            token == ",");

        return string.Join(" ", tokens);
    }

    public static double CalculateSimilarity(string code1, string code2)
    {
        var metric = new Levenstein();
        code1  = TokenizeCode(code1);
        code2 = TokenizeCode(code2);

        double similarity = metric.GetSimilarity(code1, code2);

        return similarity;
    }

    public static List<double> CalculateSimilarities(List<string> files)
    {
        List<double> similarities = new List<double>();
        var tokenizedFiles = files.Select(TokenizeCode).ToList();

        for (var i = 0; i < files.Count; i++)
        {
            for (var j = i + 1; j < files.Count; j++)
            {
                var code1 = tokenizedFiles[i];
                var code2 = tokenizedFiles[j];

                var similarity = CalculateSimilarity(code1, code2);
                similarities.Add(similarity);
            }
        }

        return similarities;
    }
}