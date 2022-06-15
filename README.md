# SimpleIR

NOTE: REWRITE IN PROGRESS.
NOTE: EVERYTHING IS AN ABSOLUTE MESS RIGHT NOW, DON'T USE AT THIS POINT IN TIME.

This is my WIP project for my programming language. 

//NuGet Package: https://www.nuget.org/packages/SimpleIR

SimpleIR allows for easy low-level code generation which can then be compiled to a PE.

I will make a documentation as I progress with this fun little project.

SimpleIR uses C#
         
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
