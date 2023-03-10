using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;

public class TreeComparer
{
    public static double TreeEditDistance(IParseTree tree1, IParseTree tree2)
    {
        int n = CountNodes(tree1), m = CountNodes(tree2);
        int[] d = new int[m + 1], dPrev = new int[m + 1];
        for (int j = 0; j <= m; j++) dPrev[j] = j;
        for (int i = 1; i <= n; i++)
        {
            int[] temp = dPrev;
            dPrev = d;
            d = temp;
            d[0] = i;
            for (int j = 1; j <= m; j++)
            {
                int cost = tree1.GetChild(i - 1)?.GetType() == tree2.GetChild(j - 1)?.GetType() ? 0 : 1;
                d[j] = Math.Min(d[j - 1] + 1, Math.Min(dPrev[j] + 1, dPrev[j - 1] + cost));
            }
        }

        var tree1Nodes = Trees.GetDescendants(tree1);
        var tree2Nodes = Trees.GetDescendants(tree2);
        var totalNodes = tree1Nodes.Count + tree2Nodes.Count;
        return (1 - (double)d[m] / totalNodes) * 100;
    }

    private static int CountNodes(IParseTree tree)
    {
        int count = 1;
        for (int i = 0; i < tree.ChildCount; i++)
        {
            count += CountNodes(tree.GetChild(i));
        }
        return count;
    }
}