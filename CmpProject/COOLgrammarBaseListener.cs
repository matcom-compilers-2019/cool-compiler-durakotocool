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
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="ICOOLgrammarListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public partial class COOLgrammarBaseListener : ICOOLgrammarListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterProgram([NotNull] COOLgrammarParser.ProgramContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitProgram([NotNull] COOLgrammarParser.ProgramContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.class"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterClass([NotNull] COOLgrammarParser.ClassContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.class"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitClass([NotNull] COOLgrammarParser.ClassContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>method</c>
	/// labeled alternative in <see cref="COOLgrammarParser.feature"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterMethod([NotNull] COOLgrammarParser.MethodContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>method</c>
	/// labeled alternative in <see cref="COOLgrammarParser.feature"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitMethod([NotNull] COOLgrammarParser.MethodContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>attribute</c>
	/// labeled alternative in <see cref="COOLgrammarParser.feature"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAttribute([NotNull] COOLgrammarParser.AttributeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>attribute</c>
	/// labeled alternative in <see cref="COOLgrammarParser.feature"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAttribute([NotNull] COOLgrammarParser.AttributeContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.formal"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFormal([NotNull] COOLgrammarParser.FormalContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.formal"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFormal([NotNull] COOLgrammarParser.FormalContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>arith</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArith([NotNull] COOLgrammarParser.ArithContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>arith</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArith([NotNull] COOLgrammarParser.ArithContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>dispatch</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDispatch([NotNull] COOLgrammarParser.DispatchContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>dispatch</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDispatch([NotNull] COOLgrammarParser.DispatchContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>integerExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIntegerExpr([NotNull] COOLgrammarParser.IntegerExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>integerExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIntegerExpr([NotNull] COOLgrammarParser.IntegerExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>inParenthesisExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterInParenthesisExpr([NotNull] COOLgrammarParser.InParenthesisExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>inParenthesisExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitInParenthesisExpr([NotNull] COOLgrammarParser.InParenthesisExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>condExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCondExpr([NotNull] COOLgrammarParser.CondExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>condExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCondExpr([NotNull] COOLgrammarParser.CondExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>letExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLetExpr([NotNull] COOLgrammarParser.LetExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>letExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLetExpr([NotNull] COOLgrammarParser.LetExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>stringExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStringExpr([NotNull] COOLgrammarParser.StringExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>stringExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStringExpr([NotNull] COOLgrammarParser.StringExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>compaExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompaExpr([NotNull] COOLgrammarParser.CompaExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>compaExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompaExpr([NotNull] COOLgrammarParser.CompaExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>notExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNotExpr([NotNull] COOLgrammarParser.NotExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>notExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNotExpr([NotNull] COOLgrammarParser.NotExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>whileExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterWhileExpr([NotNull] COOLgrammarParser.WhileExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>whileExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitWhileExpr([NotNull] COOLgrammarParser.WhileExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>negExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNegExpr([NotNull] COOLgrammarParser.NegExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>negExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNegExpr([NotNull] COOLgrammarParser.NegExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>isvoidExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIsvoidExpr([NotNull] COOLgrammarParser.IsvoidExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>isvoidExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIsvoidExpr([NotNull] COOLgrammarParser.IsvoidExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>blockExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBlockExpr([NotNull] COOLgrammarParser.BlockExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>blockExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBlockExpr([NotNull] COOLgrammarParser.BlockExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>caseExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCaseExpr([NotNull] COOLgrammarParser.CaseExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>caseExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCaseExpr([NotNull] COOLgrammarParser.CaseExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>boolExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBoolExpr([NotNull] COOLgrammarParser.BoolExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>boolExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBoolExpr([NotNull] COOLgrammarParser.BoolExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>selfDispatch</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSelfDispatch([NotNull] COOLgrammarParser.SelfDispatchContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>selfDispatch</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSelfDispatch([NotNull] COOLgrammarParser.SelfDispatchContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>assignExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssignExpr([NotNull] COOLgrammarParser.AssignExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>assignExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssignExpr([NotNull] COOLgrammarParser.AssignExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>newTypeExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNewTypeExpr([NotNull] COOLgrammarParser.NewTypeExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>newTypeExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNewTypeExpr([NotNull] COOLgrammarParser.NewTypeExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>idExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIdExpr([NotNull] COOLgrammarParser.IdExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>idExpr</c>
	/// labeled alternative in <see cref="COOLgrammarParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIdExpr([NotNull] COOLgrammarParser.IdExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>letDecl</c>
	/// labeled alternative in <see cref="COOLgrammarParser.letRule"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLetDecl([NotNull] COOLgrammarParser.LetDeclContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>letDecl</c>
	/// labeled alternative in <see cref="COOLgrammarParser.letRule"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLetDecl([NotNull] COOLgrammarParser.LetDeclContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>letBody</c>
	/// labeled alternative in <see cref="COOLgrammarParser.letRule"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLetBody([NotNull] COOLgrammarParser.LetBodyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>letBody</c>
	/// labeled alternative in <see cref="COOLgrammarParser.letRule"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLetBody([NotNull] COOLgrammarParser.LetBodyContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.params"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParams([NotNull] COOLgrammarParser.ParamsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.params"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParams([NotNull] COOLgrammarParser.ParamsContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.declaration"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDeclaration([NotNull] COOLgrammarParser.DeclarationContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.declaration"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDeclaration([NotNull] COOLgrammarParser.DeclarationContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.branch"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBranch([NotNull] COOLgrammarParser.BranchContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.branch"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBranch([NotNull] COOLgrammarParser.BranchContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="COOLgrammarParser.expr_list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_list([NotNull] COOLgrammarParser.Expr_listContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="COOLgrammarParser.expr_list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_list([NotNull] COOLgrammarParser.Expr_listContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
