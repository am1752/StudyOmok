using System;

namespace CalculatorApp
{
    public class Calculator
    {
        public Calculator()
        {
        }

        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Subtract(double a, double b)
        {
            return (a > b)? a - b : b - a;
        }

        public double Multiple(double a, double b)
        {
            return a * b;
        }

        public double Divide(double a, double b)
        {
            return (a > b)? a/b : b/a;
        }
    }
}