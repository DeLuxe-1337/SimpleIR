# SimpleIR

NOTE: EVERYTHING IS AN ABSOLUTE MESS RIGHT NOW, DON'T USE AT THIS POINT IN TIME.

This is my WIP project for my programming language. 

//NuGet Package: https://www.nuget.org/packages/SimpleIR

SimpleIR allows for easy low-level code generation which can then be compiled to a PE.

I will make a documentation as I progress with this fun little project.

SimpleIR uses C# and is built upon LLVM.
            
<details>
<summary>Hello world example</summary>
<br>

```csharp
var module = new Module("HelloWorld");
var IR = module.IR;

var main = IR.CreateFunction("main", IR.GetDataType(DataTypeKind.Void));
var printf = IR.CreateFunction("printf", IR.GetDataType(DataTypeKind.Number),
    IR.GetDataType(DataTypeKind.String));

var invoke = IR.CreateBlock("invoke");
main.InsertBlock(invoke);

invoke.CreateCall(printf, new List<SimpleType>
{
    IR.CreateValue("Hello, world\n", DataTypeKind.String)
});

//prevent console from closing
var gets = IR.CreateFunction("gets", IR.GetDataType(DataTypeKind.String));
invoke.CreateCall(gets, new List<SimpleType>());

invoke.CreateReturn();

module.Finish();
```
	
<details>
<summary>Result from "Hello world" example</summary>
<br>

![image](https://user-images.githubusercontent.com/74394136/172027172-646800dc-1388-4eca-9abc-375a805c4058.png)

Compiled PE "main" function dissasembled:

![image](https://user-images.githubusercontent.com/74394136/172027215-c8a329ec-5145-4d73-9171-14f2abfccbbf.png)

Assembly Result: (Much more that you can target the IR to.)
```assembly
	.text
	.def	 @feat.00;
	.scl	3;
	.type	0;
	.endef
	.globl	@feat.00
.set @feat.00, 1
	.file	"HelloWorld"
	.def	 _main;
	.scl	2;
	.type	32;
	.endef
	.globl	_main                   # -- Begin function main
	.p2align	4, 0x90
_main:                                  # @main
	.cfi_startproc
# %bb.0:                                # %invoke
	pushl	%ebp
	.cfi_def_cfa_offset 8
	.cfi_offset %ebp, -8
	movl	%esp, %ebp
	.cfi_def_cfa_register %ebp
	calll	___main
	pushl	$L_SimpleIR_String
	calll	_printf
	addl	$4, %esp
	calll	_gets
	popl	%ebp
	retl
	.cfi_endproc
                                        # -- End function
	.section	.rdata,"dr"
L_SimpleIR_String:                      # @SimpleIR_String
	.asciz	"Hello, world\n"

```
</details>
</details>

<details>
<summary>SimpleIR Code progress!</summary>
<br>
Example IR code looks look like
	
```

//SimpleIR Code

//headers
module = "HelloWorld"
target = "i686-pc-windows-gnu"

//constants
string SimpleIR_String = "Hello, world\n"

//declarations
function int32 printf(string)
function string gets()

//functions
function void main() {
    block on_invoke {
        call [printf, int32, {SimpleIR_String}]
        call [gets, string]
        return
    }
}
```
		
The compiler has a cool error handler (not complete) also it is indeed rust inspired.
![image](https://user-images.githubusercontent.com/74394136/172590316-d39b2697-60d6-45ff-99d4-4b89df205109.png)

</details>
