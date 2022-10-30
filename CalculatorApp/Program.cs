using System;

namespace CalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string equation = Console.ReadLine();
            var result = Calculator.Calculations(equation);

            var test = 23-(29.3-12.5);
            Console.WriteLine(test);

            Console.WriteLine (result);
        }
    }
 }
