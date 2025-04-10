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
```

## 🛠️ Requirements

To run MiniSharp, you need to have the .NET SDK installed.

✅ Windows

Download and install the .NET SDK from the official Microsoft website:
👉 https://dotnet.microsoft.com/en-us/download

Make sure to restart your terminal (Command Prompt or PowerShell) after installation.

✅ Linux (Ubuntu / Debian)

Open your terminal and run:

```bash
sudo apt update
sudo apt install dotnet-sdk-8.0
```

## 🚀 Running MiniSharp

After cloning or downloading the project, follow these steps:

```bash
cd MiniSharp             # go to the project folder
dotnet build             # (optional) builds the project
dotnet run               # runs the interpreter
```