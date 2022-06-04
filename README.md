# SimpleIR
This is my WIP project for my programming language. 

SimpleIR allows for easy low-level code generation which can then be compiled to a PE.

I will make a documentation as I progress with this fun little project.
            
Hello world example:

```csharp
Module module = new Module("HelloWorld");
var IR = module.IR;

var main = IR.CreateFunction("main", IR.GetDataType(DataTypeKind.Void));
var printf = IR.CreateFunction("printf", IR.GetDataType(DataTypeKind.Number), IR.GetDataType(DataTypeKind.String));

var invoke = IR.CreateBlock("invoke");
main.InsertBlock(invoke);

invoke.CreateCall(printf, new List<SimpleType>()
{
    IR.CreateValue("Hello, world\n", DataTypeKind.String)
});

invoke.CreateReturn();

module.Finish();
```

SimpleIR uses C# and is built upon LLVM.
