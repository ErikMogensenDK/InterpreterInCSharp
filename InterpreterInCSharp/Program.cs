namespace InterpreterInCSharp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the monkey language!!");
        Console.WriteLine();
        Console.WriteLine("Feel free to type in any code below:");
        Repl.Start();
    }
}
