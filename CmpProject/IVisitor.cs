using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmpProject.CIL;
namespace CmpProject
{
    interface IVisitor<T> where T: ParserRuleContext
    {
        void Visit(T parserRule);
    }
    interface IVisitor<T,  R> where T : ParserRuleContext
    {
        R Visit(T parserRule);
    }
    interface IVisitorC<T,C> where T : ParserRuleContext where C:IContext
    {
        void Visit(T parserRule,C context);
    }
    interface IVisitorCil<T> where T : ParserRuleContext
    {
        void Visit(T parserRule);
    }
    interface IVisitorCil<T,G> where T: ParserRuleContext where G: ICil
    {
        void Visit(T parserRule, G cilTree);
    }
    interface IVisitorCilWhitContext<T, G> where T : ParserRuleContext where G : ICil 
    {
        void Visit(T parserRule, G cilTree,IContextCil contextCil);
    }
    interface IVisitorCilWhitContext<T, G, K> where T : ParserRuleContext where G : ICil where K : IHolderCil
    {
        K Visit(T parserRule, G cilTree,IContextCil contextCil);
    }
    interface IVisitorString<T, G, K> where T : ParserRuleContext where G : ICil where K : IHolderCil
    {
        K VisitString(T parserRule, G cilTree, IContextCil contextCil);
    }
    interface IVisitorCtor
    {
        void Visit();
    }
}
