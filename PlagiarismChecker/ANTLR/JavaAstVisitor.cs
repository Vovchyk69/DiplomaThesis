using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using PlagiarismChecker.Grammars;
using System.Collections.Generic;

public class ASTNode
{
    public ASTNode(string type)
    {
        Type = type;
        Children = new List<ASTNode>();
        Metadata = new Dictionary<string, string>();
    }

    public string Type { get; set; }
    public List<ASTNode> Children { get; set; }

    public Dictionary<string, string> Metadata { get; set; }

}
public class JavaASTVisitor : JavaParserBaseVisitor<ASTNode>
{
    public override ASTNode VisitCompilationUnit([NotNull] JavaParser.CompilationUnitContext context)
    {
        ASTNode rootNode = new ASTNode("compilationUnit");
        for (int i = 0; i < context.ChildCount; i++)
        {
            ASTNode childNode = context.GetChild(i).Accept(this);
            if (childNode != null)
            {
                rootNode.Children.Add(childNode);
            }
        }
        return rootNode;
    }

    public override ASTNode VisitClassDeclaration([NotNull] JavaParser.ClassDeclarationContext context)
    {
        var classNode = new ASTNode("classDeclaration");
        classNode.Metadata.Add("title", context.identifier().GetText());
        for (int i = 0; i < context.ChildCount; i++)
        {
            ASTNode childNode = context.GetChild(i).Accept(this);
            if (childNode != null)
            {
                classNode.Children.Add(childNode);
            }
        }
        return classNode;
    }

    public override ASTNode VisitMethodDeclaration([NotNull] JavaParser.MethodDeclarationContext context)
    {
        ASTNode methodNode = new ASTNode("methodDeclaration");
        methodNode.Metadata.Add("title", context.identifier().GetText());
        for (int i = 0; i < context.ChildCount; i++)
        {
            var test = context.GetChild(i).GetText();
            var childNode = context.GetChild(i).Accept(this);
            if (childNode != null)
            {
                methodNode.Children.Add(childNode);
            }
        }
        return methodNode;
    }

    public override ASTNode VisitClassBodyDeclaration([NotNull] JavaParser.ClassBodyDeclarationContext context)
    {
        ASTNode methodNode = new ASTNode("classBodyDeclaration");
        for (int i = 0; i < context.ChildCount; i++)
        {
            var test = context.GetChild(i).GetText();
            var childNode = context.GetChild(i).Accept(this);
            if (childNode != null)
            {
                methodNode.Children.Add(childNode);
            }
        }
        return methodNode;
    }

    public override ASTNode VisitBlock([NotNull] JavaParser.BlockContext context)
    {
        var methodNode = new ASTNode("block");
        for (int i = 0; i < context.ChildCount; i++)
        {
            var childNode = context.GetChild(i).Accept(this);
            if (childNode != null)
            {
                methodNode.Children.Add(childNode);
            }
        }
        return methodNode;
    }

    public override ASTNode VisitBlockStatement([NotNull] JavaParser.BlockStatementContext context)
    {
        var methodNode = new ASTNode("blockStatement");
        methodNode.Metadata.Add("title", context.GetText());
        return methodNode;
    }

    public override ASTNode VisitMethodBody([NotNull] JavaParser.MethodBodyContext context)
    {
        var methodNode = new ASTNode("methodBody");
        for (int i = 0; i < context.ChildCount; i++)
        {
            var childNode = context.GetChild(i).Accept(this);
            if (childNode != null)
            {
                methodNode.Children.Add(childNode);
            }
        }
        return methodNode;
    }

    public override ASTNode VisitMethodCall([NotNull] JavaParser.MethodCallContext context)
    {
        var methodNode = new ASTNode("methodCall");
        methodNode.Metadata.Add("value", context.GetText());
        return methodNode;
    }

    public override ASTNode VisitVariableDeclarators([NotNull] JavaParser.VariableDeclaratorsContext context)
    {
        var methodNode = new ASTNode("variableDeclarator");
        for (int i = 0; i < context.ChildCount; i++)
        {
            var childNode = context.GetChild(i).Accept(this);
            if (childNode != null)
            {
                methodNode.Children.Add(childNode);
            }
        }
        return methodNode;
    }

    public override ASTNode VisitClassBody([NotNull] JavaParser.ClassBodyContext context)
    {
        var methodNode = new ASTNode("classBody");
        for (int i = 0; i < context.ChildCount; i++)
        {
            var childNode = context.GetChild(i).Accept(this);
            if (childNode != null)
            {
                methodNode.Children.Add(childNode);
            }
        }
        return methodNode;
    }

    //public override ASTNode VisitChildren([NotNull] IRuleNode node)
    //{
    //    var test = node.GetText();
    //    return new ASTNode("VisitedChildren");
    //}
}