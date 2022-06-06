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
  #tempcall = call [printf, int32, {SimpleIR_String}]
  #tempcall.1 = call [gets, string]
  return
}

 */