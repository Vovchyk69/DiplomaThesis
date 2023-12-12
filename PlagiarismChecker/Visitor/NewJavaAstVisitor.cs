using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using PlagiarismChecker.Grammars;

namespace PlagiarismChecker.Visitor;

public class NewJavaAstVisitor : JavaParserBaseVisitor<Node>
{
    public override Node VisitCompilationUnit(JavaParser.CompilationUnitContext context)
    {
        var node = new Node("Compilation Unit");

        foreach (var child in context.children)
        {
            node.Children.Add(Visit(child));
        }

        return node;
    }

    public override Node VisitClassDeclaration(JavaParser.ClassDeclarationContext context)
    {
        var node = new Node("Class Declaration: " + context.identifier().GetText());

        var test = context.GetText();
        foreach (var member in context.classBody().classBodyDeclaration())
        {
            node.Children.Add(Visit(member));
        }
        
        return node;
    }

    public override Node VisitMethodDeclaration(JavaParser.MethodDeclarationContext context)
    {
        var node = new Node("Method Declaration: " + context.identifier().GetText());
        var test = context.GetText();
        foreach (var member in context.methodBody().children)
        {
            node.Children.Add(Visit(member));
        }

        return node;
    }

    public override Node VisitBlock(JavaParser.BlockContext context)
    {
        var node = new Node("Block");
        var test = context.GetText();

        foreach (var statement in context.blockStatement())
        {
            node.Children.Add(Visit(statement));
        }

        return node;
    }

    public override Node VisitStatement(JavaParser.StatementContext context)
    {
        var node = new Node("Statement: "+context.GetText());
        var test = context.GetText();

        foreach (var statement in context.expression())
        {
            node.Children.Add(Visit(statement));
        }

        return node;
    }
        
    public override Node VisitExpression(JavaParser.ExpressionContext context)
    {
        var node = new Node("Expression: "+context.GetText());
        var test = context.GetText();
        foreach (var statement in context.children)
        {
            node.Children.Add(Visit(statement));
        }

        return node;
    }
        
    public override Node VisitExpressionList(JavaParser.ExpressionListContext context)
    {
        var node = new Node("Expression List:"+context.GetText());
        var test = context.GetText();
        foreach (var statement in context.children)
        {
            node.Children.Add(Visit(statement));
        }

        return node;
    }

    public override Node VisitMethodCall(JavaParser.MethodCallContext context)
    {
        var node = new Node("Method Call: " + context.identifier().GetText());
        var test = context.GetText();

        foreach (var argument in context.children)
        {
            node.Children.Add(Visit(argument));
        }

        return node;
    }
    
    public override Node VisitFieldDeclaration([NotNull] JavaParser.FieldDeclarationContext context)
    {
        var node = new Node("Field Declaration: "+ context.GetText());
        var test = context.GetText();
        foreach (var variableDeclarator in context.variableDeclarators().variableDeclarator())
        {
            node.Children.Add(Visit(variableDeclarator));
        }

        return node;
    }
    
    public override Node VisitVariableDeclarator([NotNull] JavaParser.VariableDeclaratorContext context)
    {
        var node = new Node("Variable: "+ context.GetText());
        var test = context.GetText();

        return node;
    }

    public override Node VisitTypeType(JavaParser.TypeTypeContext context)
    {
        var node = new Node("Type: " + context.GetText());
        var test = context.GetText();

        return node;
    }
    
    public override Node VisitLiteral(JavaParser.LiteralContext context)
    {
        var node = new Node("Literal: " + context.GetText());
        var test = context.GetText();

        return node;
    }
    
    public override Node VisitIdentifier(JavaParser.IdentifierContext context)
    {
        var node = new Node("Identifier: " + context.GetText());
        var test = context.GetText();

        return node;
    }
}

public class Node
{
    public string Text { get; set; }
    public List<Node> Children { get; set; }

    public Node(string text)
    {
        Text = text;
        Children = new List<Node>();
    }
}