using System;

namespace ConsoleApp1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new MathExpression();
            Console.WriteLine(parser.ParsePolishNotation(Console.ReadLine()));
        }
    }     
}