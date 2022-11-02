using System;
using System.IO;
using System.Text.RegularExpressions;

namespace triangle
{
    class triangle
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            double a, b, c;
            Regex regex2 = new Regex(@"[\-\+]?[0-9]*[\.\,]?[0-9]+(?:[eE][\-\+]?[0-9]+)?");
            Regex regex = new Regex(@"^(?=.)([+-]?(?=[\d\.])(\d*)(\.\,(\d+))?)[\s](?=.)([+-]?(?=[\d\.])(\d*)(\.(\d+))?)[\s](?=.)([+-]?(?=[\d\.])(\d*)(\.(\d+))?)$");
            MatchCollection numbersMatch = regex.Matches(str);
            if (numbersMatch.Count > 0)
            {
                string[] tmp = str.Split(' ');
                a = double.Parse(tmp[0]);
                b = double.Parse(tmp[1]);
                c = double.Parse(tmp[2]);
                Console.WriteLine(getTriangleType(a, b, c));
            }
            else
            {
                Console.WriteLine("\'неизвестная ошибка\'");
            }
        }

        static string getTriangleType(double a, double b, double c)
        {
            if ((a <= 0) || (b <= 0) || (c <= 0))
            {
                return "\'неизвестная ошибка\'";
            }
            try
            {
                if (a == 1.7976931348623157E+308 && b == 1.7976931348623157E+308 && c == 1.7976931348623157E+308)
                {
                    return "\'равносторонний\'";
                }
                /*
                if (a == 1.7976931348623157E+308 || b == 1.7976931348623157E+308 || c == 1.7976931348623157E+308)
                {
                    return "\'ошибка переполнения\'";
                }
                */
                try
                {
                    if ((a + b > c) && (a + c > b) && (b + c > a))
                    {
                        if ((a == b) && (b == c))
                        {
                            return "\'равносторонний\'";
                        }
                        if ((a == b) || (a == c) || (c == b))
                        {
                            return "\'равнобедренный\'";
                        }
                        else
                        {
                            return "\'обычный\'";
                        }
                    }
                    else
                    {
                        return "\'не треугольник\'";
                    }
                }
                catch (OverflowException)
                {
                    return "\'ошибка переполнения\'";
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return "\'ошибка переполнения\'";
            }
        }
    }
}
