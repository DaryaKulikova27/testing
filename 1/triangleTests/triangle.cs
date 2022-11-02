using System;
using System.IO;

namespace triangle
{
    public class Triangle
    {
        void EmptyMain(string[] args)
        {
        }

        public string getTriangleType(double a, double b, double c)
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
