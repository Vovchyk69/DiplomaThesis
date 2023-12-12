using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using PlagiarismChecker.Grammars;
using System.Collections.Generic;
using System.Linq;

public record ASTNode(string Type)
{
    public string Type { get; } = Type;
    public List<ASTNode> Children { get; } = new();

    public Dictionary<string, string> Metadata { get; } = new();

    public void AddChild(ASTNode child)
    {
        Children.Add(child);
    }

    public List<ASTNode> GetAllNodes()
    {
        var allNodes = new List<ASTNode> { this };
        foreach (var child in Children)
        {
            allNodes.AddRange(child.GetAllNodes());
        }
        return allNodes;
    }

    public string ToPostOrderString()
    {
        var childrenStrings = Children.Select(child => child.ToPostOrderString()).ToArray();
        var postOrderString = string.Join(",", childrenStrings);
        return postOrderString;
    }

}
public class JavaASTVisitor : JavaParserBaseVisitor<ASTNode>
{
    public override ASTNode VisitCompilationUnit([NotNull] JavaParser.CompilationUnitContext context)
    {
        var rootNode = new ASTNode("Entry Point");
        for (int i = 0; i < context.ChildCount; i++)
        {
            var childNode = context.GetChild(i).Accept(this);
            if (childNode != null)
            {
                rootNode.Children.Add(childNode);
            }
        }
        return rootNode;
    }

    public override ASTNode VisitClassDeclaration([NotNull] JavaParser.ClassDeclarationContext context)
    {
        var classNode = new ASTNode("Class Declaration");
        classNode.Metadata.Add("title", context.identifier().GetText());
        return Traverse(classNode, context);
    }

    public override ASTNode VisitClassBody([NotNull] JavaParser.ClassBodyContext context)
    {
        ASTNode methodNode = new ASTNode("Class Body");
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitMethodDeclaration([NotNull] JavaParser.MethodDeclarationContext context)
    {
        ASTNode methodNode = new ASTNode("Method Declaration");
        methodNode.Metadata.Add("title", context.identifier().GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitBlock([NotNull] JavaParser.BlockContext context)
    {
        var methodNode = new ASTNode("Block");
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitMethodBody([NotNull] JavaParser.MethodBodyContext context)
    {
        var methodNode = new ASTNode("Method Body");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitMethodCall([NotNull] JavaParser.MethodCallContext context)
    {
        var methodNode = new ASTNode("Method Call");
        methodNode.Metadata.Add("title", context.GetText());
        return methodNode;
    }

    public override ASTNode VisitFieldDeclaration([NotNull] JavaParser.FieldDeclarationContext context)
    {
        var methodNode = new ASTNode("Field");
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitTypeType([NotNull] JavaParser.TypeTypeContext context)
    {
        var methodNode = new ASTNode("Type");
        methodNode.Metadata.Add("title", context.GetText());
        return methodNode;
    }

    public override ASTNode VisitStatement([NotNull] JavaParser.StatementContext context)
    {
        var methodNode = new ASTNode("Statement:");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitConstructorDeclaration([NotNull] JavaParser.ConstructorDeclarationContext context)
    {
        var methodNode = new ASTNode("Constructor");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitArrayInitializer([NotNull] JavaParser.ArrayInitializerContext context)
    {
        var methodNode = new ASTNode("Array Initializer");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitCatchClause([NotNull] JavaParser.CatchClauseContext context)
    {
        var methodNode = new ASTNode("Catch");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitConstDeclaration([NotNull] JavaParser.ConstDeclarationContext context)
    {
        var methodNode = new ASTNode("Const Declaration");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitExpression([NotNull] JavaParser.ExpressionContext context)
    {
        var methodNode = new ASTNode("Expression");
        methodNode.Metadata.Add("title", context.GetText());
        return methodNode;
    }

    public override ASTNode VisitExpressionList([NotNull] JavaParser.ExpressionListContext context)
    {
        var methodNode = new ASTNode("Expression List");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitParExpression([NotNull] JavaParser.ParExpressionContext context)
    {
        var methodNode = new ASTNode("Par expression");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitSwitchExpression([NotNull] JavaParser.SwitchExpressionContext context)
    {
        var methodNode = new ASTNode("Switch expression");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitFormalParameters([NotNull] JavaParser.FormalParametersContext context)
    {
        var methodNode = new ASTNode("Parameters");
        var test = context.Parent;
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitFormalParameter([NotNull] JavaParser.FormalParameterContext context)
    {
        var methodNode = new ASTNode("Parameter");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitFormalParameterList([NotNull] JavaParser.FormalParameterListContext context)
    {
        var methodNode = new ASTNode("Parameter List");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitIdentifier([NotNull] JavaParser.IdentifierContext context)
    {
        var methodNode = new ASTNode("Identifier");
        methodNode.Metadata.Add("title", context.GetText());
        return Traverse(methodNode, context);
    }

    public override ASTNode VisitTerminal([NotNull] ITerminalNode node)
    {
        var text = node.GetText();
        if (!operators.Contains(text))
            return base.VisitTerminal(node);

        var methodNode = new ASTNode("Op");
        methodNode.Metadata.Add("title", text);
        return methodNode;
    }

    private ASTNode Traverse(ASTNode node, RuleContext context)
    {
        for (int i = 0; i < context.ChildCount; i++)
        {
            var test = context.GetChild(i).GetText();
            var type = context.GetChild(i).GetType();
            var childNode = context.GetChild(i).Accept(this);
            if (childNode == null) continue;
            if (!NodesToSkip.Contains(childNode.Type))
                node.Children.Add(childNode);
        }
        return node;
    }

    private List<string> Types = new() { "Type", "Variable Declarator", "Expression", "Identifier" };
    private static string operators = "+-=*/^";

    private List<string> NodesToSkip = new() { "Parameter List", "Method Body", "Class Body", "Identifier" };
}