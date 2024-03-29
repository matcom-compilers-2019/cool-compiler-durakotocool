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

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public partial class COOLgrammarLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		POINT_COMMA=1, COMA=2, CORCHETE_AB=3, CORCHETE_CE=4, LLAVE_AB=5, LLAVE_CE=6, 
		POINT=7, PARENT_AB=8, PARENT_CE=9, OP_EQUAL=10, OP_MINOR=11, OP_MINOR_EQUAL=12, 
		OP_PLUS=13, OP_MINUS=14, OP_MULT=15, OP_DIV=16, OP_NEG=17, OP_ASSIGN=18, 
		OP_TYPED=19, OP_CASE=20, OP_CLASS=21, IF=22, THEN=23, ELSE=24, FI=25, 
		WHILE=26, LOOP=27, POOL=28, LET=29, IN=30, CASE=31, OF=32, ESAC=33, TRUE=34, 
		FALSE=35, CLASS=36, INHERITS=37, ISVOID=38, NEW=39, NOT=40, INTEGER=41, 
		TYPE=42, ID=43, STRING=44, WS=45, COMMENTS=46;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"POINT_COMMA", "COMA", "CORCHETE_AB", "CORCHETE_CE", "LLAVE_AB", "LLAVE_CE", 
		"POINT", "PARENT_AB", "PARENT_CE", "OP_EQUAL", "OP_MINOR", "OP_MINOR_EQUAL", 
		"OP_PLUS", "OP_MINUS", "OP_MULT", "OP_DIV", "OP_NEG", "OP_ASSIGN", "OP_TYPED", 
		"OP_CASE", "OP_CLASS", "IF", "THEN", "ELSE", "FI", "WHILE", "LOOP", "POOL", 
		"LET", "IN", "CASE", "OF", "ESAC", "TRUE", "FALSE", "CLASS", "INHERITS", 
		"ISVOID", "NEW", "NOT", "INT", "MALETTER", "MILETTER", "LETTER", "INTEGER", 
		"TYPE", "ID", "STRING", "WS", "COMMENTS"
	};


	public COOLgrammarLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public COOLgrammarLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "';'", "','", "'['", "']'", "'{'", "'}'", "'.'", "'('", "')'", "'='", 
		"'<'", "'<='", "'+'", "'-'", "'*'", "'/'", "'~'", "'<-'", "':'", "'=>'", 
		"'@'", "'if'", "'then'", "'else'", "'fi'", "'while'", "'loop'", "'pool'", 
		"'let'", "'in'", "'case'", "'of'", "'esac'", "'true'", "'false'", "'class'", 
		"'inherits'", "'isvoid'", "'new'", "'not'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "POINT_COMMA", "COMA", "CORCHETE_AB", "CORCHETE_CE", "LLAVE_AB", 
		"LLAVE_CE", "POINT", "PARENT_AB", "PARENT_CE", "OP_EQUAL", "OP_MINOR", 
		"OP_MINOR_EQUAL", "OP_PLUS", "OP_MINUS", "OP_MULT", "OP_DIV", "OP_NEG", 
		"OP_ASSIGN", "OP_TYPED", "OP_CASE", "OP_CLASS", "IF", "THEN", "ELSE", 
		"FI", "WHILE", "LOOP", "POOL", "LET", "IN", "CASE", "OF", "ESAC", "TRUE", 
		"FALSE", "CLASS", "INHERITS", "ISVOID", "NEW", "NOT", "INTEGER", "TYPE", 
		"ID", "STRING", "WS", "COMMENTS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "COOLgrammar.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static COOLgrammarLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '\x30', '\x141', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', '\x4', 
		' ', '\t', ' ', '\x4', '!', '\t', '!', '\x4', '\"', '\t', '\"', '\x4', 
		'#', '\t', '#', '\x4', '$', '\t', '$', '\x4', '%', '\t', '%', '\x4', '&', 
		'\t', '&', '\x4', '\'', '\t', '\'', '\x4', '(', '\t', '(', '\x4', ')', 
		'\t', ')', '\x4', '*', '\t', '*', '\x4', '+', '\t', '+', '\x4', ',', '\t', 
		',', '\x4', '-', '\t', '-', '\x4', '.', '\t', '.', '\x4', '/', '\t', '/', 
		'\x4', '\x30', '\t', '\x30', '\x4', '\x31', '\t', '\x31', '\x4', '\x32', 
		'\t', '\x32', '\x4', '\x33', '\t', '\x33', '\x3', '\x2', '\x3', '\x2', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x4', '\x3', '\x4', '\x3', '\x5', 
		'\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\a', '\x3', '\a', '\x3', 
		'\b', '\x3', '\b', '\x3', '\t', '\x3', '\t', '\x3', '\n', '\x3', '\n', 
		'\x3', '\v', '\x3', '\v', '\x3', '\f', '\x3', '\f', '\x3', '\r', '\x3', 
		'\r', '\x3', '\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xF', '\x3', '\xF', 
		'\x3', '\x10', '\x3', '\x10', '\x3', '\x11', '\x3', '\x11', '\x3', '\x12', 
		'\x3', '\x12', '\x3', '\x13', '\x3', '\x13', '\x3', '\x13', '\x3', '\x14', 
		'\x3', '\x14', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x16', 
		'\x3', '\x16', '\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', '\x18', 
		'\x3', '\x18', '\x3', '\x18', '\x3', '\x18', '\x3', '\x18', '\x3', '\x19', 
		'\x3', '\x19', '\x3', '\x19', '\x3', '\x19', '\x3', '\x19', '\x3', '\x1A', 
		'\x3', '\x1A', '\x3', '\x1A', '\x3', '\x1B', '\x3', '\x1B', '\x3', '\x1B', 
		'\x3', '\x1B', '\x3', '\x1B', '\x3', '\x1B', '\x3', '\x1C', '\x3', '\x1C', 
		'\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1D', '\x3', '\x1D', 
		'\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1E', '\x3', '\x1E', 
		'\x3', '\x1E', '\x3', '\x1E', '\x3', '\x1F', '\x3', '\x1F', '\x3', '\x1F', 
		'\x3', ' ', '\x3', ' ', '\x3', ' ', '\x3', ' ', '\x3', ' ', '\x3', '!', 
		'\x3', '!', '\x3', '!', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\x3', 
		'\"', '\x3', '\"', '\x3', '#', '\x3', '#', '\x3', '#', '\x3', '#', '\x3', 
		'#', '\x3', '$', '\x3', '$', '\x3', '$', '\x3', '$', '\x3', '$', '\x3', 
		'$', '\x3', '%', '\x3', '%', '\x3', '%', '\x3', '%', '\x3', '%', '\x3', 
		'%', '\x3', '&', '\x3', '&', '\x3', '&', '\x3', '&', '\x3', '&', '\x3', 
		'&', '\x3', '&', '\x3', '&', '\x3', '&', '\x3', '\'', '\x3', '\'', '\x3', 
		'\'', '\x3', '\'', '\x3', '\'', '\x3', '\'', '\x3', '\'', '\x3', '(', 
		'\x3', '(', '\x3', '(', '\x3', '(', '\x3', ')', '\x3', ')', '\x3', ')', 
		'\x3', ')', '\x3', '*', '\x3', '*', '\x3', '+', '\x3', '+', '\x3', ',', 
		'\x3', ',', '\x3', '-', '\x3', '-', '\x5', '-', '\xFA', '\n', '-', '\x3', 
		'.', '\x6', '.', '\xFD', '\n', '.', '\r', '.', '\xE', '.', '\xFE', '\x3', 
		'/', '\x3', '/', '\x3', '/', '\x3', '/', '\a', '/', '\x105', '\n', '/', 
		'\f', '/', '\xE', '/', '\x108', '\v', '/', '\x3', '\x30', '\x3', '\x30', 
		'\x3', '\x30', '\x3', '\x30', '\a', '\x30', '\x10E', '\n', '\x30', '\f', 
		'\x30', '\xE', '\x30', '\x111', '\v', '\x30', '\x3', '\x31', '\x3', '\x31', 
		'\x3', '\x31', '\x3', '\x31', '\x3', '\x31', '\x3', '\x31', '\a', '\x31', 
		'\x119', '\n', '\x31', '\f', '\x31', '\xE', '\x31', '\x11C', '\v', '\x31', 
		'\x3', '\x31', '\x3', '\x31', '\x3', '\x32', '\x6', '\x32', '\x121', '\n', 
		'\x32', '\r', '\x32', '\xE', '\x32', '\x122', '\x3', '\x32', '\x3', '\x32', 
		'\x3', '\x33', '\x3', '\x33', '\x3', '\x33', '\x3', '\x33', '\a', '\x33', 
		'\x12B', '\n', '\x33', '\f', '\x33', '\xE', '\x33', '\x12E', '\v', '\x33', 
		'\x3', '\x33', '\x5', '\x33', '\x131', '\n', '\x33', '\x3', '\x33', '\x3', 
		'\x33', '\x3', '\x33', '\x3', '\x33', '\a', '\x33', '\x137', '\n', '\x33', 
		'\f', '\x33', '\xE', '\x33', '\x13A', '\v', '\x33', '\x3', '\x33', '\x3', 
		'\x33', '\x5', '\x33', '\x13E', '\n', '\x33', '\x3', '\x33', '\x3', '\x33', 
		'\x5', '\x11A', '\x12C', '\x138', '\x2', '\x34', '\x3', '\x3', '\x5', 
		'\x4', '\a', '\x5', '\t', '\x6', '\v', '\a', '\r', '\b', '\xF', '\t', 
		'\x11', '\n', '\x13', '\v', '\x15', '\f', '\x17', '\r', '\x19', '\xE', 
		'\x1B', '\xF', '\x1D', '\x10', '\x1F', '\x11', '!', '\x12', '#', '\x13', 
		'%', '\x14', '\'', '\x15', ')', '\x16', '+', '\x17', '-', '\x18', '/', 
		'\x19', '\x31', '\x1A', '\x33', '\x1B', '\x35', '\x1C', '\x37', '\x1D', 
		'\x39', '\x1E', ';', '\x1F', '=', ' ', '?', '!', '\x41', '\"', '\x43', 
		'#', '\x45', '$', 'G', '%', 'I', '&', 'K', '\'', 'M', '(', 'O', ')', 'Q', 
		'*', 'S', '\x2', 'U', '\x2', 'W', '\x2', 'Y', '\x2', '[', '+', ']', ',', 
		'_', '-', '\x61', '.', '\x63', '/', '\x65', '\x30', '\x3', '\x2', '\x4', 
		'\x5', '\x2', '\v', '\f', '\xE', '\xF', '\"', '\"', '\x3', '\x3', '\f', 
		'\f', '\x2', '\x14B', '\x2', '\x3', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x5', '\x3', '\x2', '\x2', '\x2', '\x2', '\a', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\t', '\x3', '\x2', '\x2', '\x2', '\x2', '\v', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\r', '\x3', '\x2', '\x2', '\x2', '\x2', '\xF', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x11', '\x3', '\x2', '\x2', '\x2', '\x2', '\x13', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x15', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x17', '\x3', '\x2', '\x2', '\x2', '\x2', '\x19', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x1B', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1D', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x1F', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '!', '\x3', '\x2', '\x2', '\x2', '\x2', '#', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '%', '\x3', '\x2', '\x2', '\x2', '\x2', '\'', '\x3', '\x2', 
		'\x2', '\x2', '\x2', ')', '\x3', '\x2', '\x2', '\x2', '\x2', '+', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '-', '\x3', '\x2', '\x2', '\x2', '\x2', '/', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x31', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x33', '\x3', '\x2', '\x2', '\x2', '\x2', '\x35', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x37', '\x3', '\x2', '\x2', '\x2', '\x2', '\x39', 
		'\x3', '\x2', '\x2', '\x2', '\x2', ';', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'=', '\x3', '\x2', '\x2', '\x2', '\x2', '?', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x41', '\x3', '\x2', '\x2', '\x2', '\x2', '\x43', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x45', '\x3', '\x2', '\x2', '\x2', '\x2', 'G', '\x3', 
		'\x2', '\x2', '\x2', '\x2', 'I', '\x3', '\x2', '\x2', '\x2', '\x2', 'K', 
		'\x3', '\x2', '\x2', '\x2', '\x2', 'M', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'O', '\x3', '\x2', '\x2', '\x2', '\x2', 'Q', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '[', '\x3', '\x2', '\x2', '\x2', '\x2', ']', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '_', '\x3', '\x2', '\x2', '\x2', '\x2', '\x61', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x63', '\x3', '\x2', '\x2', '\x2', '\x2', '\x65', 
		'\x3', '\x2', '\x2', '\x2', '\x3', 'g', '\x3', '\x2', '\x2', '\x2', '\x5', 
		'i', '\x3', '\x2', '\x2', '\x2', '\a', 'k', '\x3', '\x2', '\x2', '\x2', 
		'\t', 'm', '\x3', '\x2', '\x2', '\x2', '\v', 'o', '\x3', '\x2', '\x2', 
		'\x2', '\r', 'q', '\x3', '\x2', '\x2', '\x2', '\xF', 's', '\x3', '\x2', 
		'\x2', '\x2', '\x11', 'u', '\x3', '\x2', '\x2', '\x2', '\x13', 'w', '\x3', 
		'\x2', '\x2', '\x2', '\x15', 'y', '\x3', '\x2', '\x2', '\x2', '\x17', 
		'{', '\x3', '\x2', '\x2', '\x2', '\x19', '}', '\x3', '\x2', '\x2', '\x2', 
		'\x1B', '\x80', '\x3', '\x2', '\x2', '\x2', '\x1D', '\x82', '\x3', '\x2', 
		'\x2', '\x2', '\x1F', '\x84', '\x3', '\x2', '\x2', '\x2', '!', '\x86', 
		'\x3', '\x2', '\x2', '\x2', '#', '\x88', '\x3', '\x2', '\x2', '\x2', '%', 
		'\x8A', '\x3', '\x2', '\x2', '\x2', '\'', '\x8D', '\x3', '\x2', '\x2', 
		'\x2', ')', '\x8F', '\x3', '\x2', '\x2', '\x2', '+', '\x92', '\x3', '\x2', 
		'\x2', '\x2', '-', '\x94', '\x3', '\x2', '\x2', '\x2', '/', '\x97', '\x3', 
		'\x2', '\x2', '\x2', '\x31', '\x9C', '\x3', '\x2', '\x2', '\x2', '\x33', 
		'\xA1', '\x3', '\x2', '\x2', '\x2', '\x35', '\xA4', '\x3', '\x2', '\x2', 
		'\x2', '\x37', '\xAA', '\x3', '\x2', '\x2', '\x2', '\x39', '\xAF', '\x3', 
		'\x2', '\x2', '\x2', ';', '\xB4', '\x3', '\x2', '\x2', '\x2', '=', '\xB8', 
		'\x3', '\x2', '\x2', '\x2', '?', '\xBB', '\x3', '\x2', '\x2', '\x2', '\x41', 
		'\xC0', '\x3', '\x2', '\x2', '\x2', '\x43', '\xC3', '\x3', '\x2', '\x2', 
		'\x2', '\x45', '\xC8', '\x3', '\x2', '\x2', '\x2', 'G', '\xCD', '\x3', 
		'\x2', '\x2', '\x2', 'I', '\xD3', '\x3', '\x2', '\x2', '\x2', 'K', '\xD9', 
		'\x3', '\x2', '\x2', '\x2', 'M', '\xE2', '\x3', '\x2', '\x2', '\x2', 'O', 
		'\xE9', '\x3', '\x2', '\x2', '\x2', 'Q', '\xED', '\x3', '\x2', '\x2', 
		'\x2', 'S', '\xF1', '\x3', '\x2', '\x2', '\x2', 'U', '\xF3', '\x3', '\x2', 
		'\x2', '\x2', 'W', '\xF5', '\x3', '\x2', '\x2', '\x2', 'Y', '\xF9', '\x3', 
		'\x2', '\x2', '\x2', '[', '\xFC', '\x3', '\x2', '\x2', '\x2', ']', '\x100', 
		'\x3', '\x2', '\x2', '\x2', '_', '\x109', '\x3', '\x2', '\x2', '\x2', 
		'\x61', '\x112', '\x3', '\x2', '\x2', '\x2', '\x63', '\x120', '\x3', '\x2', 
		'\x2', '\x2', '\x65', '\x13D', '\x3', '\x2', '\x2', '\x2', 'g', 'h', '\a', 
		'=', '\x2', '\x2', 'h', '\x4', '\x3', '\x2', '\x2', '\x2', 'i', 'j', '\a', 
		'.', '\x2', '\x2', 'j', '\x6', '\x3', '\x2', '\x2', '\x2', 'k', 'l', '\a', 
		']', '\x2', '\x2', 'l', '\b', '\x3', '\x2', '\x2', '\x2', 'm', 'n', '\a', 
		'_', '\x2', '\x2', 'n', '\n', '\x3', '\x2', '\x2', '\x2', 'o', 'p', '\a', 
		'}', '\x2', '\x2', 'p', '\f', '\x3', '\x2', '\x2', '\x2', 'q', 'r', '\a', 
		'\x7F', '\x2', '\x2', 'r', '\xE', '\x3', '\x2', '\x2', '\x2', 's', 't', 
		'\a', '\x30', '\x2', '\x2', 't', '\x10', '\x3', '\x2', '\x2', '\x2', 'u', 
		'v', '\a', '*', '\x2', '\x2', 'v', '\x12', '\x3', '\x2', '\x2', '\x2', 
		'w', 'x', '\a', '+', '\x2', '\x2', 'x', '\x14', '\x3', '\x2', '\x2', '\x2', 
		'y', 'z', '\a', '?', '\x2', '\x2', 'z', '\x16', '\x3', '\x2', '\x2', '\x2', 
		'{', '|', '\a', '>', '\x2', '\x2', '|', '\x18', '\x3', '\x2', '\x2', '\x2', 
		'}', '~', '\a', '>', '\x2', '\x2', '~', '\x7F', '\a', '?', '\x2', '\x2', 
		'\x7F', '\x1A', '\x3', '\x2', '\x2', '\x2', '\x80', '\x81', '\a', '-', 
		'\x2', '\x2', '\x81', '\x1C', '\x3', '\x2', '\x2', '\x2', '\x82', '\x83', 
		'\a', '/', '\x2', '\x2', '\x83', '\x1E', '\x3', '\x2', '\x2', '\x2', '\x84', 
		'\x85', '\a', ',', '\x2', '\x2', '\x85', ' ', '\x3', '\x2', '\x2', '\x2', 
		'\x86', '\x87', '\a', '\x31', '\x2', '\x2', '\x87', '\"', '\x3', '\x2', 
		'\x2', '\x2', '\x88', '\x89', '\a', '\x80', '\x2', '\x2', '\x89', '$', 
		'\x3', '\x2', '\x2', '\x2', '\x8A', '\x8B', '\a', '>', '\x2', '\x2', '\x8B', 
		'\x8C', '\a', '/', '\x2', '\x2', '\x8C', '&', '\x3', '\x2', '\x2', '\x2', 
		'\x8D', '\x8E', '\a', '<', '\x2', '\x2', '\x8E', '(', '\x3', '\x2', '\x2', 
		'\x2', '\x8F', '\x90', '\a', '?', '\x2', '\x2', '\x90', '\x91', '\a', 
		'@', '\x2', '\x2', '\x91', '*', '\x3', '\x2', '\x2', '\x2', '\x92', '\x93', 
		'\a', '\x42', '\x2', '\x2', '\x93', ',', '\x3', '\x2', '\x2', '\x2', '\x94', 
		'\x95', '\a', 'k', '\x2', '\x2', '\x95', '\x96', '\a', 'h', '\x2', '\x2', 
		'\x96', '.', '\x3', '\x2', '\x2', '\x2', '\x97', '\x98', '\a', 'v', '\x2', 
		'\x2', '\x98', '\x99', '\a', 'j', '\x2', '\x2', '\x99', '\x9A', '\a', 
		'g', '\x2', '\x2', '\x9A', '\x9B', '\a', 'p', '\x2', '\x2', '\x9B', '\x30', 
		'\x3', '\x2', '\x2', '\x2', '\x9C', '\x9D', '\a', 'g', '\x2', '\x2', '\x9D', 
		'\x9E', '\a', 'n', '\x2', '\x2', '\x9E', '\x9F', '\a', 'u', '\x2', '\x2', 
		'\x9F', '\xA0', '\a', 'g', '\x2', '\x2', '\xA0', '\x32', '\x3', '\x2', 
		'\x2', '\x2', '\xA1', '\xA2', '\a', 'h', '\x2', '\x2', '\xA2', '\xA3', 
		'\a', 'k', '\x2', '\x2', '\xA3', '\x34', '\x3', '\x2', '\x2', '\x2', '\xA4', 
		'\xA5', '\a', 'y', '\x2', '\x2', '\xA5', '\xA6', '\a', 'j', '\x2', '\x2', 
		'\xA6', '\xA7', '\a', 'k', '\x2', '\x2', '\xA7', '\xA8', '\a', 'n', '\x2', 
		'\x2', '\xA8', '\xA9', '\a', 'g', '\x2', '\x2', '\xA9', '\x36', '\x3', 
		'\x2', '\x2', '\x2', '\xAA', '\xAB', '\a', 'n', '\x2', '\x2', '\xAB', 
		'\xAC', '\a', 'q', '\x2', '\x2', '\xAC', '\xAD', '\a', 'q', '\x2', '\x2', 
		'\xAD', '\xAE', '\a', 'r', '\x2', '\x2', '\xAE', '\x38', '\x3', '\x2', 
		'\x2', '\x2', '\xAF', '\xB0', '\a', 'r', '\x2', '\x2', '\xB0', '\xB1', 
		'\a', 'q', '\x2', '\x2', '\xB1', '\xB2', '\a', 'q', '\x2', '\x2', '\xB2', 
		'\xB3', '\a', 'n', '\x2', '\x2', '\xB3', ':', '\x3', '\x2', '\x2', '\x2', 
		'\xB4', '\xB5', '\a', 'n', '\x2', '\x2', '\xB5', '\xB6', '\a', 'g', '\x2', 
		'\x2', '\xB6', '\xB7', '\a', 'v', '\x2', '\x2', '\xB7', '<', '\x3', '\x2', 
		'\x2', '\x2', '\xB8', '\xB9', '\a', 'k', '\x2', '\x2', '\xB9', '\xBA', 
		'\a', 'p', '\x2', '\x2', '\xBA', '>', '\x3', '\x2', '\x2', '\x2', '\xBB', 
		'\xBC', '\a', '\x65', '\x2', '\x2', '\xBC', '\xBD', '\a', '\x63', '\x2', 
		'\x2', '\xBD', '\xBE', '\a', 'u', '\x2', '\x2', '\xBE', '\xBF', '\a', 
		'g', '\x2', '\x2', '\xBF', '@', '\x3', '\x2', '\x2', '\x2', '\xC0', '\xC1', 
		'\a', 'q', '\x2', '\x2', '\xC1', '\xC2', '\a', 'h', '\x2', '\x2', '\xC2', 
		'\x42', '\x3', '\x2', '\x2', '\x2', '\xC3', '\xC4', '\a', 'g', '\x2', 
		'\x2', '\xC4', '\xC5', '\a', 'u', '\x2', '\x2', '\xC5', '\xC6', '\a', 
		'\x63', '\x2', '\x2', '\xC6', '\xC7', '\a', '\x65', '\x2', '\x2', '\xC7', 
		'\x44', '\x3', '\x2', '\x2', '\x2', '\xC8', '\xC9', '\a', 'v', '\x2', 
		'\x2', '\xC9', '\xCA', '\a', 't', '\x2', '\x2', '\xCA', '\xCB', '\a', 
		'w', '\x2', '\x2', '\xCB', '\xCC', '\a', 'g', '\x2', '\x2', '\xCC', '\x46', 
		'\x3', '\x2', '\x2', '\x2', '\xCD', '\xCE', '\a', 'h', '\x2', '\x2', '\xCE', 
		'\xCF', '\a', '\x63', '\x2', '\x2', '\xCF', '\xD0', '\a', 'n', '\x2', 
		'\x2', '\xD0', '\xD1', '\a', 'u', '\x2', '\x2', '\xD1', '\xD2', '\a', 
		'g', '\x2', '\x2', '\xD2', 'H', '\x3', '\x2', '\x2', '\x2', '\xD3', '\xD4', 
		'\a', '\x65', '\x2', '\x2', '\xD4', '\xD5', '\a', 'n', '\x2', '\x2', '\xD5', 
		'\xD6', '\a', '\x63', '\x2', '\x2', '\xD6', '\xD7', '\a', 'u', '\x2', 
		'\x2', '\xD7', '\xD8', '\a', 'u', '\x2', '\x2', '\xD8', 'J', '\x3', '\x2', 
		'\x2', '\x2', '\xD9', '\xDA', '\a', 'k', '\x2', '\x2', '\xDA', '\xDB', 
		'\a', 'p', '\x2', '\x2', '\xDB', '\xDC', '\a', 'j', '\x2', '\x2', '\xDC', 
		'\xDD', '\a', 'g', '\x2', '\x2', '\xDD', '\xDE', '\a', 't', '\x2', '\x2', 
		'\xDE', '\xDF', '\a', 'k', '\x2', '\x2', '\xDF', '\xE0', '\a', 'v', '\x2', 
		'\x2', '\xE0', '\xE1', '\a', 'u', '\x2', '\x2', '\xE1', 'L', '\x3', '\x2', 
		'\x2', '\x2', '\xE2', '\xE3', '\a', 'k', '\x2', '\x2', '\xE3', '\xE4', 
		'\a', 'u', '\x2', '\x2', '\xE4', '\xE5', '\a', 'x', '\x2', '\x2', '\xE5', 
		'\xE6', '\a', 'q', '\x2', '\x2', '\xE6', '\xE7', '\a', 'k', '\x2', '\x2', 
		'\xE7', '\xE8', '\a', '\x66', '\x2', '\x2', '\xE8', 'N', '\x3', '\x2', 
		'\x2', '\x2', '\xE9', '\xEA', '\a', 'p', '\x2', '\x2', '\xEA', '\xEB', 
		'\a', 'g', '\x2', '\x2', '\xEB', '\xEC', '\a', 'y', '\x2', '\x2', '\xEC', 
		'P', '\x3', '\x2', '\x2', '\x2', '\xED', '\xEE', '\a', 'p', '\x2', '\x2', 
		'\xEE', '\xEF', '\a', 'q', '\x2', '\x2', '\xEF', '\xF0', '\a', 'v', '\x2', 
		'\x2', '\xF0', 'R', '\x3', '\x2', '\x2', '\x2', '\xF1', '\xF2', '\x4', 
		'\x32', ';', '\x2', '\xF2', 'T', '\x3', '\x2', '\x2', '\x2', '\xF3', '\xF4', 
		'\x4', '\x43', '\\', '\x2', '\xF4', 'V', '\x3', '\x2', '\x2', '\x2', '\xF5', 
		'\xF6', '\x4', '\x63', '|', '\x2', '\xF6', 'X', '\x3', '\x2', '\x2', '\x2', 
		'\xF7', '\xFA', '\x5', 'U', '+', '\x2', '\xF8', '\xFA', '\x5', 'W', ',', 
		'\x2', '\xF9', '\xF7', '\x3', '\x2', '\x2', '\x2', '\xF9', '\xF8', '\x3', 
		'\x2', '\x2', '\x2', '\xFA', 'Z', '\x3', '\x2', '\x2', '\x2', '\xFB', 
		'\xFD', '\x5', 'S', '*', '\x2', '\xFC', '\xFB', '\x3', '\x2', '\x2', '\x2', 
		'\xFD', '\xFE', '\x3', '\x2', '\x2', '\x2', '\xFE', '\xFC', '\x3', '\x2', 
		'\x2', '\x2', '\xFE', '\xFF', '\x3', '\x2', '\x2', '\x2', '\xFF', '\\', 
		'\x3', '\x2', '\x2', '\x2', '\x100', '\x106', '\x5', 'U', '+', '\x2', 
		'\x101', '\x105', '\x5', 'S', '*', '\x2', '\x102', '\x105', '\x5', 'Y', 
		'-', '\x2', '\x103', '\x105', '\a', '\x61', '\x2', '\x2', '\x104', '\x101', 
		'\x3', '\x2', '\x2', '\x2', '\x104', '\x102', '\x3', '\x2', '\x2', '\x2', 
		'\x104', '\x103', '\x3', '\x2', '\x2', '\x2', '\x105', '\x108', '\x3', 
		'\x2', '\x2', '\x2', '\x106', '\x104', '\x3', '\x2', '\x2', '\x2', '\x106', 
		'\x107', '\x3', '\x2', '\x2', '\x2', '\x107', '^', '\x3', '\x2', '\x2', 
		'\x2', '\x108', '\x106', '\x3', '\x2', '\x2', '\x2', '\x109', '\x10F', 
		'\x5', 'W', ',', '\x2', '\x10A', '\x10E', '\x5', 'S', '*', '\x2', '\x10B', 
		'\x10E', '\x5', 'Y', '-', '\x2', '\x10C', '\x10E', '\a', '\x61', '\x2', 
		'\x2', '\x10D', '\x10A', '\x3', '\x2', '\x2', '\x2', '\x10D', '\x10B', 
		'\x3', '\x2', '\x2', '\x2', '\x10D', '\x10C', '\x3', '\x2', '\x2', '\x2', 
		'\x10E', '\x111', '\x3', '\x2', '\x2', '\x2', '\x10F', '\x10D', '\x3', 
		'\x2', '\x2', '\x2', '\x10F', '\x110', '\x3', '\x2', '\x2', '\x2', '\x110', 
		'`', '\x3', '\x2', '\x2', '\x2', '\x111', '\x10F', '\x3', '\x2', '\x2', 
		'\x2', '\x112', '\x11A', '\a', '$', '\x2', '\x2', '\x113', '\x114', '\a', 
		'^', '\x2', '\x2', '\x114', '\x119', '\a', '$', '\x2', '\x2', '\x115', 
		'\x116', '\a', '^', '\x2', '\x2', '\x116', '\x119', '\a', '^', '\x2', 
		'\x2', '\x117', '\x119', '\v', '\x2', '\x2', '\x2', '\x118', '\x113', 
		'\x3', '\x2', '\x2', '\x2', '\x118', '\x115', '\x3', '\x2', '\x2', '\x2', 
		'\x118', '\x117', '\x3', '\x2', '\x2', '\x2', '\x119', '\x11C', '\x3', 
		'\x2', '\x2', '\x2', '\x11A', '\x11B', '\x3', '\x2', '\x2', '\x2', '\x11A', 
		'\x118', '\x3', '\x2', '\x2', '\x2', '\x11B', '\x11D', '\x3', '\x2', '\x2', 
		'\x2', '\x11C', '\x11A', '\x3', '\x2', '\x2', '\x2', '\x11D', '\x11E', 
		'\a', '$', '\x2', '\x2', '\x11E', '\x62', '\x3', '\x2', '\x2', '\x2', 
		'\x11F', '\x121', '\t', '\x2', '\x2', '\x2', '\x120', '\x11F', '\x3', 
		'\x2', '\x2', '\x2', '\x121', '\x122', '\x3', '\x2', '\x2', '\x2', '\x122', 
		'\x120', '\x3', '\x2', '\x2', '\x2', '\x122', '\x123', '\x3', '\x2', '\x2', 
		'\x2', '\x123', '\x124', '\x3', '\x2', '\x2', '\x2', '\x124', '\x125', 
		'\b', '\x32', '\x2', '\x2', '\x125', '\x64', '\x3', '\x2', '\x2', '\x2', 
		'\x126', '\x127', '\a', '/', '\x2', '\x2', '\x127', '\x128', '\a', '/', 
		'\x2', '\x2', '\x128', '\x12C', '\x3', '\x2', '\x2', '\x2', '\x129', '\x12B', 
		'\v', '\x2', '\x2', '\x2', '\x12A', '\x129', '\x3', '\x2', '\x2', '\x2', 
		'\x12B', '\x12E', '\x3', '\x2', '\x2', '\x2', '\x12C', '\x12D', '\x3', 
		'\x2', '\x2', '\x2', '\x12C', '\x12A', '\x3', '\x2', '\x2', '\x2', '\x12D', 
		'\x130', '\x3', '\x2', '\x2', '\x2', '\x12E', '\x12C', '\x3', '\x2', '\x2', 
		'\x2', '\x12F', '\x131', '\t', '\x3', '\x2', '\x2', '\x130', '\x12F', 
		'\x3', '\x2', '\x2', '\x2', '\x131', '\x13E', '\x3', '\x2', '\x2', '\x2', 
		'\x132', '\x133', '\a', '*', '\x2', '\x2', '\x133', '\x134', '\a', ',', 
		'\x2', '\x2', '\x134', '\x138', '\x3', '\x2', '\x2', '\x2', '\x135', '\x137', 
		'\v', '\x2', '\x2', '\x2', '\x136', '\x135', '\x3', '\x2', '\x2', '\x2', 
		'\x137', '\x13A', '\x3', '\x2', '\x2', '\x2', '\x138', '\x139', '\x3', 
		'\x2', '\x2', '\x2', '\x138', '\x136', '\x3', '\x2', '\x2', '\x2', '\x139', 
		'\x13B', '\x3', '\x2', '\x2', '\x2', '\x13A', '\x138', '\x3', '\x2', '\x2', 
		'\x2', '\x13B', '\x13C', '\a', ',', '\x2', '\x2', '\x13C', '\x13E', '\a', 
		'+', '\x2', '\x2', '\x13D', '\x126', '\x3', '\x2', '\x2', '\x2', '\x13D', 
		'\x132', '\x3', '\x2', '\x2', '\x2', '\x13E', '\x13F', '\x3', '\x2', '\x2', 
		'\x2', '\x13F', '\x140', '\b', '\x33', '\x2', '\x2', '\x140', '\x66', 
		'\x3', '\x2', '\x2', '\x2', '\x10', '\x2', '\xF9', '\xFE', '\x104', '\x106', 
		'\x10D', '\x10F', '\x118', '\x11A', '\x122', '\x12C', '\x130', '\x138', 
		'\x13D', '\x3', '\b', '\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
