grammar COOLgrammar;
options{
	language = CSharp;

	
}
//Gramatica
	program	: (class)+ POINT_COMMA EOF;
	class	: CLASS TYPE (INHERITS TYPE)? LLAVE_AB (feature)*  LLAVE_CE;
	feature : ID PARENT_AB (formal (COMA formal)*)? PARENT_CE OP_TYPED TYPE LLAVE_AB expr  LLAVE_CE POINT_COMMA //un metodo
			| ID OP_TYPED TYPE POINT_COMMA //atributo
			| ID OP_TYPED TYPE CORCHETE_AB  OP_ASSIGN expr CORCHETE_CE POINT_COMMA //atributo inicializado
			;
	formal  : ID OP_TYPED TYPE;

	expr    : ID OP_ASSIGN  expr//asignacion
			| expr(OP_CLASS TYPE)? POINT ID PARENT_AB (expr( COMA expr)*)? PARENT_CE//LLamada a una funcion
			| ID PARENT_AB (expr( COMA expr)*)? PARENT_CE//LLamada a una funcion dentro de una clase
			| IF expr THEN expr ELSE expr FI//Expresion condicional
			| WHILE expr LOOP expr POOL//Expresion while
			|LLAVE_AB (expr POINT_COMMA)+  LLAVE_CE//Bloque de expresiones
			|LET ID OP_TYPED TYPE ( OP_ASSIGN expr )? (COMA ID OP_TYPED TYPE ( OP_ASSIGN expr )?)* IN expr //expresion let
			|CASE expr OF (ID OP_TYPED TYPE OP_CASE expr)+ ESAC
			|ISVOID expr //Para ver si la expresion retorna void
			|expr OP_PLUS expr
			|expr OP_MINUS expr
			|expr OP_MULT expr
			|expr OP_DIV expr
			|expr OP_NEG expr
			|expr OP_MINOR expr
			|expr OP_MINOR_EQUAL expr
			|expr OP_EQUAL expr
			|NOT expr
			|PARENT_AB expr PARENT_CE
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

