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
	|operatorToken=(MIN | MAX) LPAREN expression COMA expression RPAREN #MinExpr
	|operatorToken=(MOD | DIV) LPAREN expression COMA expression RPAREN #ModExpr
	|operatorToken=(INC | DEC) LPAREN expression RPAREN #IncExpr
	|NOT LPAREN expression RPAREN #NotExpr
	|expression operatorToken=(ISMORE | ISLESS | ISMOREOREQUAL | ISLESSOREQUAL) expression #CompareExpr
	|expression ISEQUAL expression #IsEqualExpr
	|NUMBER #NumberExpr
	|IDENTIFIER #IdentifierExpr;

/*
* Lexer Rules
*/

NUMBER : INT;
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