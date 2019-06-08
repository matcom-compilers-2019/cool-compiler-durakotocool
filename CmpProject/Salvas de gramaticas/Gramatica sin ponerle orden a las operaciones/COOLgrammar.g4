grammar COOLgrammar;
options{
	language = CSharp;

	
}
//Gramatica
	program	: (class)+ ';' EOF;
	class	: 'class' TYPE ('inherits' TYPE)? '{' (feature)* '}';
	feature : ID '(' (formal (',' formal)*)? ')' ':' TYPE '{' expr '}' ';' //un metodo
			| ID ':' TYPE ';' //atributo
			| ID ':' TYPE '['  '<-' expr ']' ';' //atributo inicializado
			;
	formal  : ID ':' TYPE;

	expr    : ID '(' (expr( ',' expr)*)? ')'//LLamada a una funcion dentro de una clase
			| 'if' expr 'then' expr 'else' expr 'fi'//Expresion condicional
			| 'while' expr 'loop' expr 'pool'//Expresion while
			|'{' (expr ';')+  '}'//Bloque de expresiones
			|'let' ID ':' TYPE ( '<-' expr )? (',' ID ':' TYPE ( '<-' expr )?)* 'in' expr //expresion let
			|'case' expr 'of' (ID ':' TYPE '=>' expr)+ 'esac'
			| expr('@' TYPE)? '.' ID '(' (expr( ',' expr)*)? ')'//LLamada a una funcion
			| OP_NEG expr
			|'isvoid' expr //Para ver si la expresion retorna void
			|expr OP_MULT expr
			|expr OP_DIV expr
			|expr OP_PLUS expr
			|expr OP_MINUS expr
			|expr OP_MINOR expr
			|expr OP_MINOR_EQUAL expr
			|expr OP_EQUAL expr
		    |expr OP_NEG expr
			|NOT expr
			| ID '<-'  expr//asignacion
			|'(' expr ')'
			|ID
			|INTEGER
			|STRING
			|TRUE
			|FALSE
			
			;


//Separadores
	POINT_COMMA   : ';' ;
	COMA         : ',' ;
	CORCHETE_AB  : '[' ;
	CORCHETE_CE  : ']' ;
	LLAVE_AB     : '{' ;
	LLAVE_CE     : '}' ;
	POINT        : '.' ;
	PARENT_AB    : '(' ;
	PARENT_CE    : ')' ;
	//BARRA_VERT   : '|' ;
//Operadores
	OP_EQUAL         : '='  ; 
	OP_MINOR         : '<'  ;
	OP_MINOR_EQUAL   : '<=' ; 
	OP_PLUS          : '+'  ; 
	OP_MINUS         : '-'  ; 
	OP_MULT          : '*'  ; 
	OP_DIV           : '/'  ;
	OP_NEG           : '~'  ;
	OP_ASSIGN        : '<-' ;
	OP_TYPED         : ':'  ;
	OP_CASE          : '=>' ;
	OP_CLASS		 : '@'  ;
//KEYWORDS
//IF
	IF:'if';
	THEN:'then';
	ELSE:'else';
	FI:'fi';
//while
	WHILE:'while';
	LOOP:'loop';
	POOL:'pool';
//let
	LET:'let';
	IN:'in';
//case
	CASE:'case';
	OF:'of';
	ESAC:'esac';
//bool
	TRUE:'true';
	FALSE:'false';
//ESPECIAL
	CLASS:'class';
	INHERITS:'inherits';
	ISVOID:'isvoid';
	NEW:'new';
	NOT:'not';

fragment INT:('0'..'9');
fragment MALETTER:('A'..'Z');
fragment MILETTER:('a'..'z');
fragment LETTER:MALETTER|MILETTER;
INTEGER:INT+;
TYPE:MALETTER(INT|LETTER|'_')*;
ID:MILETTER(INT|LETTER|'_')*;
STRING : '"'('\\"'|.)*? '"' ;
//'\v'
WS: (' '|'\n'|'\f'|'\t'|'\r')+->skip;
//fragment KEYWORDS:IF|THEN|FI|WHILE|LOOP|POOL|LET|IN|CASE|OF|ESAC|INHERITS|ISVOID;

