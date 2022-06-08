grammar SimpleIR;

program: line* EOF;

line: statement;

statement: assignment | assign_with_type | call | functionDeclare | functionDefine | blockDec | return;

return: 'return' expression?;

blockDec: 'block' IDENTIFIER block;
 
param: IDENTIFIER ':' expression;
params: (param (',' param)*?); 
function: 'function' expression IDENTIFIER '(' params? ')';
functionDeclare: function;
functionDefine: function block;

assign_with_type: expression IDENTIFIER '=' expression;
assignment: IDENTIFIER '=' expression;

callArgs: '{' expression (',' expression)* '}';
call: 'call' '[' expression ',' expression (',' callArgs)? ']';

expression
: constant #constantExpression
| IDENTIFIER #identifierExpression
| call #callExpression
;

constant: STRING | NUMBER | BOOLEAN | TYPE;
block: '{' line* '}';

TYPE: 'string' | 'int32' | 'number' | 'bool' | 'void';
NUMBER: [0-9][0-9]*;
STRING: ('"' ~'"'* '"') | ('\'' ~'\''* '\'');
BOOLEAN: 'false' | 'true';

WHITESPACE: (' ' | '\t' | '\r' | '\n') -> skip;
COMMENT: '//' ~[\r\n]* -> skip;
IDENTIFIER: [#a-zA-Z_] [a-zA-Z_0-9.]*;