# UserInput

A library for parsing various types of user input and other untrusted text input.

## Casting
A set of function for casting unknown objects (usualy strings) to number types (int, decimal, datetime). Functions return a nullable type, and null is returned for unkown or un-parseable objects.

## Console Arguments
A object designed for parsing string args[] from dotnet console applications and ease access to named and unnamed items. Can automatically parse int, long, decimal and DateTime.

Supports arguments a variety of formats including but not limited to:
```
program.exe /Name:value
program.exe -name value
program.exe unnamed1 unnamed2 -name value1 value2
program.exe --name:value1:value2

```
Example usage:
```
> program.exe a1 a2 /Name:Value /Multi:1 2 /Date:01-02-2030 /Int:123
```

```c#
using JeremyOne.UserInput;
namespace Program{
    static void Main(string[] args) {

        var Arguments = new ConsoleArguments(args);

        //Checking if argument exists:
        bool nameExists = Arguments["name"].Exists; //Returns: true
        bool wxyzExists = Arguments["wzyx"].Exists; //Returns: false

        //Getting string argument values
        string nameValue = Arguments["name"].Value; //Returns: "Value"
        string wxyzValue = Arguments["wxyz"].Value; //Returns: ""
 
        //ValueOrDefault() returns a value, or a default if it does not exist
        string nameDefault = Arguments["name"].ValueOrDefault("Default"); //Returns: "Value"
        string wxyzDefault = Arguments["wzyz"].ValueOrDefault("Default"); //Returns: "Default"

        //Getting int and DateTime types
        DateTime dateValue = Arguments["date"].ValueDateTime; //Returns: 01/02/2016 00:00:00
        int intValue  = Arguments["int"].ValueInt; //Returns: 123
        int intWxyzValue  = Arguments["wxyz"].ValueInt; //Returns: 0
        int intWxyzDefault = Arguments["wxyz"].ValueIntOrDefault(456); //Returns 456

        //Getting multi-value (array) arguments
        bool isArray = Arguments["multi"].IsArray(); //Returns: true
        string arrayItem1 = Arguments["multi"].GetItem(0).Value; //Returns: "a1"
        string arrayItem2 = Arguments["multi"].GetItem(1).Value; //Returns: "a2"
        string outOfRangeItem3 = Arguments["multi"].GetItem(2).Value; //Returns: ""

        //Getting multi-value UNNAMED (array) arguments
        bool uIsArray = Arguments.Default.IsArray(); //Returns: true
        string uArrayItem1 = Arguments.Default.GetItem(0).Value; //Returns: "a1"
        string uArrayItem2 = Arguments.Default.GetItem(1).Value; //Returns: "a2"
        string uOutOfRangeItem3 = Arguments.Default.GetItem(2).Value; //Returns: ""

    }
}
```

## Console Input
Several functions to help with common user prompts at the console

#### UerInput.Console.PromptUser
Prompts for user input, result can be automatically parced to a strong type 

## String Extentions

### string.SmartSplit()
Splits a string while respecting escape charcters, useful for splitting CSV's that may also have commas in their values

Example useage:
```c#
string example = "test,'$1,234.56'";

string[] standardSplit = example.Split(',');
//Returns: ["test", "$1", "234.56"]

string[] smartSplit = example.SmartSplit(',', '\'');
//Returns: ["test", "$1,234.56"]
```

## Filtering
Soon

## Encoding
Soon

## Formatting
Soon