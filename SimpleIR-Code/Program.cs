namespace SimpleIR_Code
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}


/*

//SimpleIR Code

//headers
module_name = "HelloWorld"
target = "i686-pc-windows-gnu"

//constants
string SimpleIR_String = "Hello, world\n"

//declarations
function int32 printf(string)
function string gets()

//functions
function void main() {
    on_invoke {
        call [printf, int32, {SimpleIR_String}]
        call [gets, string]
        return
    }
}

 */
