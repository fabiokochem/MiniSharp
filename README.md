# MiniSharp – Toy Language Interpreter in C#

**MiniSharp** is a toy programming language interpreter written in C#.  
It's a small, clean, and educational project that demonstrates how a programming language works under the hood — from source code to execution.

---

## Features

- ✅ Lexical analysis (tokenizer / lexer)
- ✅ Recursive descent parser
- ✅ Abstract Syntax Tree (AST)
- ✅ Interpreter using the Visitor pattern
- ✅ Support for:
  - Variables (`let`)
  - Integer arithmetic
  - `print` statement
  - `if` / `else` conditionals
  - `while` loops
  - Block scope with `{}`

---

## Example Program

```plaintext
let x = 0;
while x < 3 {
    print x;
    let x = x + 1;
}
