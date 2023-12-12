using System;
using System.Collections.Generic;
using System.Linq;

class AstComparer
{
    public float GetSimilarityPercentage(ASTNode tree1, ASTNode tree2)
    {
        var totalNodes = CountNodes(tree1) + CountNodes(tree2);
        var treeEditDistance = CalculateTreeEditDistance(tree1, tree2);

        return treeEditDistance* 1.0f / totalNodes;
    }

    public static List<ASTNode> FindCommonSubtrees(ASTNode root1, ASTNode root2)
    {
        var commonSubtrees = new List<ASTNode>();

        foreach (var node1 in TraverseAst(root1))
        {
            var matchingNodes = new List<ASTNode>();
            foreach (var node2 in TraverseAst(root2))
            {
                if (AreSubtreesEqual(node1, node2))
                {
                    matchingNodes.Add(node2);
                }
            }

            if (matchingNodes.Count > 0)
            {
                // Add the common subtree to the list of common subtrees
                commonSubtrees.Add(node1);
            }
        }

        return commonSubtrees;
    }

    private int CountNodes(ASTNode tree)
    {
        var increment = 0;
        return increment + tree.Children.Sum(CountNodes);
    }

    private int CalculateTreeEditDistance(ASTNode tree1, ASTNode tree2)
    {
        var tree1String = tree1.ToPostOrderString();
        var tree2String = tree2.ToPostOrderString();

        var tree1Array = tree1String.Split(',').Where(el => el!=string.Empty).ToArray();
        var tree2Array = tree2String.Split(',').Where(el => el != string.Empty).ToArray();

        var editDistance = new int[tree1Array.Length + 1, tree2Array.Length + 1];

        for (int i = 0; i <= tree1Array.Length; i++)
        {
            editDistance[i, 0] = i;
        }

        for (int j = 0; j <= tree2Array.Length; j++)
        {
            editDistance[0, j] = j;
        }

        for (int i = 1; i <= tree1Array.Length; i++)
        {
            for (int j = 1; j <= tree2Array.Length; j++)
            {
                var cost = tree1Array[i - 1] == tree2Array[j - 1] ? 0 : 1;
                editDistance[i, j] = Math.Min(Math.Min(
                    editDistance[i - 1, j] + 1,
                    editDistance[i, j - 1] + 1),
                    editDistance[i - 1, j - 1] + cost);
            }
        }

        return editDistance[tree1Array.Length, tree2Array.Length];
    }

    private static IEnumerable<ASTNode> TraverseAst(ASTNode root)
    {
        yield return root;

        foreach (var child in root.Children)
        {
            foreach (var node in TraverseAst(child))
            {
                yield return node;
            }
        }
    }

    private static bool AreSubtreesEqual(ASTNode node1, ASTNode node2)
    {
        if (node1.Type != node2.Type) return false;


        for (int i = 0; i < node1.Children.Count; i++)
        {
            if (!AreSubtreesEqual(node1.Children[i], node2.Children[i]))
            {
                return false;
            }
        }

        return true;
    }
}