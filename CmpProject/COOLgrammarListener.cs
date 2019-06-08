//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from COOLgrammar.g4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419


using CmpProject;

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="COOLgrammarParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public interface ICOOLgrammarListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] COOLgrammarParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] COOLgrammarParser.ProgramContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.class"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClass([NotNull] COOLgrammarParser.ClassContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.class"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClass([NotNull] COOLgrammarParser.ClassContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>method</c>
	/// labeled alternative in <see cref="COOLgrammarParser.feature"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethod([NotNull] COOLgrammarParser.MethodContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>method</c>
	/// labeled alternative in <see cref="COOLgrammarParser.feature"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethod([NotNull] COOLgrammarParser.MethodContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>attribute</c>
	/// labeled alternative in <see cref="COOLgrammarParser.feature"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAttribute([NotNull] COOLgrammarParser.AttributeContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>attribute</c>
	/// labeled alternative in <see cref="COOLgrammarParser.feature"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAttribute([NotNull] COOLgrammarParser.AttributeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.formal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFormal([NotNull] COOLgrammarParser.FormalContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.formal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFormal([NotNull] COOLgrammarParser.FormalContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>arith</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArith([NotNull] COOLgrammarParser.ArithContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>arith</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArith([NotNull] COOLgrammarParser.ArithContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>dispatch</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDispatch([NotNull] COOLgrammarParser.DispatchContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>dispatch</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDispatch([NotNull] COOLgrammarParser.DispatchContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>integerExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIntegerExpr([NotNull] COOLgrammarParser.IntegerExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>integerExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIntegerExpr([NotNull] COOLgrammarParser.IntegerExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>inParenthesisExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInParenthesisExpr([NotNull] COOLgrammarParser.InParenthesisExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>inParenthesisExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInParenthesisExpr([NotNull] COOLgrammarParser.InParenthesisExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>condExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondExpr([NotNull] COOLgrammarParser.CondExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>condExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondExpr([NotNull] COOLgrammarParser.CondExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>letExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLetExpr([NotNull] COOLgrammarParser.LetExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>letExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLetExpr([NotNull] COOLgrammarParser.LetExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>stringExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStringExpr([NotNull] COOLgrammarParser.StringExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>stringExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStringExpr([NotNull] COOLgrammarParser.StringExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>compaExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompaExpr([NotNull] COOLgrammarParser.CompaExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>compaExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompaExpr([NotNull] COOLgrammarParser.CompaExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>notExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNotExpr([NotNull] COOLgrammarParser.NotExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>notExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNotExpr([NotNull] COOLgrammarParser.NotExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>whileExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhileExpr([NotNull] COOLgrammarParser.WhileExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>whileExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhileExpr([NotNull] COOLgrammarParser.WhileExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>negExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNegExpr([NotNull] COOLgrammarParser.NegExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>negExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNegExpr([NotNull] COOLgrammarParser.NegExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>isvoidExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIsvoidExpr([NotNull] COOLgrammarParser.IsvoidExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>isvoidExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIsvoidExpr([NotNull] COOLgrammarParser.IsvoidExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>blockExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockExpr([NotNull] COOLgrammarParser.BlockExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>blockExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockExpr([NotNull] COOLgrammarParser.BlockExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>caseExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCaseExpr([NotNull] COOLgrammarParser.CaseExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>caseExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCaseExpr([NotNull] COOLgrammarParser.CaseExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>boolExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBoolExpr([NotNull] COOLgrammarParser.BoolExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>boolExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBoolExpr([NotNull] COOLgrammarParser.BoolExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>selfDispatch</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSelfDispatch([NotNull] COOLgrammarParser.SelfDispatchContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>selfDispatch</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSelfDispatch([NotNull] COOLgrammarParser.SelfDispatchContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>assignExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignExpr([NotNull] COOLgrammarParser.AssignExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>assignExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignExpr([NotNull] COOLgrammarParser.AssignExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>newTypeExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNewTypeExpr([NotNull] COOLgrammarParser.NewTypeExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>newTypeExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNewTypeExpr([NotNull] COOLgrammarParser.NewTypeExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>idExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdExpr([NotNull] COOLgrammarParser.IdExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>idExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdExpr([NotNull] COOLgrammarParser.IdExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>letDecl</c>
	/// labeled alternative in <see cref="COOLgrammarParser.letRule"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLetDecl([NotNull] COOLgrammarParser.LetDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>letDecl</c>
	/// labeled alternative in <see cref="COOLgrammarParser.letRule"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLetDecl([NotNull] COOLgrammarParser.LetDeclContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>letBody</c>
	/// labeled alternative in <see cref="COOLgrammarParser.letRule"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLetBody([NotNull] COOLgrammarParser.LetBodyContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>letBody</c>
	/// labeled alternative in <see cref="COOLgrammarParser.letRule"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLetBody([NotNull] COOLgrammarParser.LetBodyContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParams([NotNull] COOLgrammarParser.ParamsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParams([NotNull] COOLgrammarParser.ParamsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDeclaration([NotNull] COOLgrammarParser.DeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDeclaration([NotNull] COOLgrammarParser.DeclarationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.branch"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBranch([NotNull] COOLgrammarParser.BranchContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.branch"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBranch([NotNull] COOLgrammarParser.BranchContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.expr_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpr_list([NotNull] COOLgrammarParser.Expr_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.expr_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpr_list([NotNull] COOLgrammarParser.Expr_listContext context);
}
