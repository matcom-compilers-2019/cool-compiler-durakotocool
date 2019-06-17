grammar COOLgrammar;
options{
	language = CSharp;

	
}
@header {
using CmpProject;
}
//Gramatica
	program	: (classes+=class)+ EOF ;
	//program	: (class)+ ;
	class
	locals[ClassContext father]
			: 'class' type= TYPE ('inherits' inherits=TYPE)? '{' (features+=feature)* '}'';';
			//un metodo
	//En los features y en las declaraciones voy a tirar unos codigos para guardar en un atributo el string del nombre y el atributo que es lo unico que hace falta
	feature 
	locals[ string idText, string typeText]
			:methodName=ID '(' (formals+=formal (',' formals+=formal)*)? ')' ':' TypeReturn=TYPE '{'exprBody=expr '}' ';'
			{ 
				$idText=($methodName).Text; 
				$typeText=($TypeReturn).Text;
			} #method
	        //atributo 
			| decl=declaration ';'
			{ 
			  $idText=((AttributeContext)_localctx).decl.idText;
			  $typeText=((AttributeContext)_localctx).decl.typeText; 
			} #attribute //es una declaracion pero en el scope de la clase puede que se cambie en un futuro
			;
	formal  
	locals[ string idText, string typeText]
			: id=ID ':' type=TYPE { $idText=($id).Text; $typeText=($type).Text;} ;	
			//LLamada a una funcion dentro de una clase 
expr   locals[IType computedType] :
			//Expresion condicional
		     'if' ifExpr=expr 'then'thenExpr=expr 'else'elseExpr=expr 'fi'								 #condExpr
		    //Expresion while
			| 'while' whileExpr= expr 'loop' loopExpr= expr 'pool'										 #whileExpr
			//Bloque de expresiones
			| '{' (expresions+=expr ';')* finalExpresion=expr';'  '}'									 #blockExpr
			//expresion let
			| 'let' let=letRule																			 #letExpr
			| 'case' expresion=expr 'of' firstBranch=branch (branches+= branch)* 'esac'					 #caseExpr
			|'new' type=TYPE																			 #newTypeExpr
			| id=ID																						 #idExpr
			| integer= INTEGER															                 #integerExpr
 			| string= STRING																			 #stringExpr
  			| bool=(TRUE|FALSE)																			 #boolExpr
			|  '('expresion=expr')'																		 #inParenthesisExpr
            |  id=ID '(' (expresions+=expr( ',' expresions+= expr)*)? ')'								 #selfDispatch
			|  expresion=expr('@'type=TYPE)?'.'id=ID '('(expresions+=expr( ',' expresions+= expr)*)? ')' #dispatch
			|  '~'expresion=expr																	     #negExpr
			| 'isvoid'	expresion=expr																	 #isvoidExpr
			| left=expr op=(OP_MULT|OP_DIV)   right= expr											     #arith
			| left=expr op=(OP_PLUS|OP_MINUS) right= expr												 #arith
			| left=expr op=( OP_MINOR|OP_MINOR_EQUAL |OP_EQUAL) right= expr								 #compaExpr
			| 'not' expresion=expr   															         #notExpr
			| id=ID '<-' expresion=expr																	 #assignExpr																		 
			;
letRule locals[IType computedType]: declaretion= declaration ','let= letRule							 #letDecl
			|  declaretion= declaration'in' body=expr													 #letBody
			;
      params: declaretions+= declaration (',' declaretions+= declaration)*;
  declaration
	  locals[string idText,string typeText]
			: id= ID ':' type= TYPE ( '<-' expression=expr )? {$idText=($id).Text; $typeText=($type).Text;};
      branch
	  locals[string idText,string typeText,IType computedType]
			: id= ID ':' type= TYPE '=>'  expression=expr     {$idText=($id).Text; $typeText=($type).Text;};
   expr_list
			: (expresions+=expr( ',' expressions+= expr)*)?;


//Separadores
	POINT_COMMA  : ';' ;
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
	IF		:'if';
	THEN	:'then';
	ELSE	:'else';
	FI		:'fi';
//while
	WHILE	:'while';
	LOOP	:'loop';
	POOL	:'pool';
//let
	LET		:'let';
	IN		:'in';
//case
	CASE	:'case';
	OF		:'of';
	ESAC	:'esac';
//bool
	TRUE	:'true';
	FALSE	:'false';
//ESPECIAL
	CLASS	:'class';
	INHERITS:'inherits';
	ISVOID	:'isvoid';
	NEW		:'new';
	NOT		:'not';
fragment INT		:('0'..'9');
fragment MALETTER	:('A'..'Z');
fragment MILETTER	:('a'..'z');
fragment LETTER		:MALETTER|MILETTER;
INTEGER				:INT+;
TYPE				:MALETTER(INT|LETTER|'_')*;
ID					:MILETTER(INT|LETTER|'_')*;
STRING				: '"'('\\"'|.)*? '"' ;
//'\v'
WS					: (' '|'\n'|'\fr')+'|'\t'|'\->skip;
COMMENTS			:(('--' (.)*?('\n'|EOF))|'(*'(.)*?'*)');
//fragment KEYWORDS:IF|THEN|FI|WHILE|LOOP|POOL|LET|IN|CASE|OF|ESAC|INHERITS|ISVOID;

