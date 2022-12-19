using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    internal class MathExpression
    {
        public decimal ParsePolishNotation(string str)
        {
            Stack<decimal> st = new Stack<decimal>();

            var words = str.Replace('.', ',').Split();
            decimal[] number = new decimal[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                bool isNumber = decimal.TryParse(words[i], out number[i]);
                if (isNumber)
                    st.Push(number[i]);
                else
                {
                    decimal op2;
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
                            if (op2 != 0)
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