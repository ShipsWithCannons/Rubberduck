﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Rubberduck.VBA.Nodes;

namespace Rubberduck.VBA
{
    public class VBParser
    {
        public Node Parse(string projectName, string componentName, string code)
        {
            var result = ParseInternal(code);
            var walker = new ParseTreeWalker();
            
            var listener = new VBTreeListener(projectName, componentName);
            walker.Walk(listener, result);

            return listener.Root;
        }

        private IParseTree ParseInternal(string code)
        {
            var input = new AntlrInputStream(code);
            var lexer = new VisualBasic6Lexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new VisualBasic6Parser(tokens);
            
            return parser.startRule();
        }
    }
}