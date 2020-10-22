grammar PoorGrammar;

/*
* Parser Rules
*/

compileUnit : expression EOF;
expression:
	LPAREN expression RPAREN #ParenthesizedExpr
	|expression EXPONENT expression #ExponentialExpr
	|expression operatorToken=(MULTIPLY | DIVIDE) expression #MultiplicativeExpr
	|expression operatorToken=(ADD | SUBTRACT) expression #AdditiveExpr
	|MIN LPAREN expression COMA expression RPAREN #MinExpr
	|MAX LPAREN expression COMA expression RPAREN #MaxExpr
	|MOD LPAREN expression COMA expression RPAREN #ModExpr
	|DIV LPAREN expression COMA expression RPAREN #DivExpr
	|INC LPAREN expression RPAREN #IncExpr
	|DEC LPAREN expression RPAREN #DecExpr
	|NOT LPAREN expression RPAREN #NotExpr
	|expression ISMORE expression #IsMoreExpr
	|expression ISLESS expression #IsLessExpr
	|expression ISEQUAL expression #IsEqualExpr
	|expression ISMOREOREQUAL expression #IsMoreOrEqualExpr
	|expression ISLESSOREQUAL expression #IsLessOrEqualExpr
	|NUMBER #NumberExpr
	|IDENTIFIER #IdentifierExpr;

/*
* Lexer Rules
*/

NUMBER : INT ('.' INT)?;
IDENTIFIER : [A-Z]+[0-9]+('=')?;

INT:('0'..'9')+;

EXPONENT : '^';
MULTIPLY : '*';
DIVIDE : '/';
SUBTRACT : '-';
ADD : '+';
LPAREN : '(';
RPAREN : ')';
MIN : 'min';
MAX : 'max';
MOD : 'mod';
DIV : 'div';
INC : 'inc';
DEC : 'dec';
NOT : 'not';
COMA : ',';

ISMORE : '>';
ISLESS : '<';
ISEQUAL : '==';
ISMOREOREQUAL : '>=';
ISLESSOREQUAL : '<=';

WS : [ \t\r\n] -> channel(HIDDEN);