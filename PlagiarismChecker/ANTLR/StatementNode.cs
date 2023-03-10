using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlagiarismChecker.ANTLR
{
    public class StatementNode
    {
        public StatementNode(string name)
        {
            Name = name;
        }
        public string Name { get; }
        public dynamic Statements { get; set; }
    }
}
