using System;
using System.IO;
using ToyLang.Lexer;
using ToyLang.Parser;
using ToyLang.Interpreter;

string source = File.ReadAllText("program.toy");

var lexer = new Lexer(source);
var tokens = lexer.ScanTokens();

var parser = new Parser(tokens);
var statements = parser.Parse();

var interpreter = new Interpreter();
interpreter.Interpret(statements);
