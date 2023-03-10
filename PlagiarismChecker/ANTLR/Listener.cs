using PlagiarismChecker.Grammars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlagiarismChecker.ANTLR
{
    public class MyListener : JavaParserBaseListener
    {
        private MethodNode currentMethod;
        private List<StatementNode> currentBlock = new List<StatementNode>();
        private List<MethodNode> methods = new List<MethodNode>();

        public override void EnterMethodDeclaration(JavaParser.MethodDeclarationContext context)
        {
            currentMethod = new MethodNode(context.identifier().GetText());
        }

        public override void ExitMethodDeclaration(JavaParser.MethodDeclarationContext context)
        {
            currentMethod.Statements = currentBlock;
            methods.Add(currentMethod);
            currentMethod = null;
            currentBlock = new List<StatementNode>();
        }

        public override void EnterStatement(JavaParser.StatementContext context)
        {
            // Create a new StatementNode and add it to the current block
            StatementNode statement = new StatementNode(context.GetText());
            currentBlock.Add(statement);
        }

        public List<MethodNode> GetMethods()
        {
            return methods;
        }
        public List<StatementNode> Statements()
        {
            return currentBlock;
        }
    }
}
