using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class MathExpression
    {
        public double ParsePolishNotation(string str)
        {
            Stack<double> st = new Stack<double>();

            var words = str.Split();
            double[] number = new double[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                bool isNumber = double.TryParse(words[i], out number[i]);
                if (isNumber)
                    st.Push(number[i]);
                else
                {
                    double op2;
                    switch (words[i])
                    {
                        case "+":
                            st.Push(st.Pop() + st.Pop());
                            break;
                        case "*":
                            st.Push(st.Pop() * st.Pop());
                            break;
                        case "-":
                            op2 = st.Pop();
                            st.Push(st.Pop() - op2);
                            break;
                        case "/":
                            op2 = st.Pop();
                            if (op2 != 0.0)
                                st.Push(st.Pop() / op2);
                            else
                                throw new DivideByZeroException("Деление на ноль");  
                            break;
                        default:
                            throw new InvalidOperationException("Неизвестная команда");
                    }
                }
            }
            return st.Pop();
        }
    }
}