using System;
using System.IO;
using System.Text.RegularExpressions;

namespace triangleTests
{
    class Program
    {
        static void Main(string[] args)
        { 
            var mc = new Program();
            mc.CheckTriangle();
        }

        public void CheckTriangle()
        { 
            var iStream = new FileStream("testInput.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new System.IO.StreamReader(iStream);

            using (StreamWriter fOutput = new StreamWriter("result.txt"))
            {
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == "")
                    {
                        continue;
                    }
                    //Regex regex = new Regex(@"^(?=.)([+-]?(?=[\d\.])(\d*)(\.\,(\d+))?)[\s](?=.)([+-]?(?=[\d\.])(\d*)(\.(\d+))?)[\s](?=.)([+-]?(?=[\d\.])(\d*)(\.(\d+))?)[\s][\'][\p{IsCyrillic} ]+[\']$");
                    Regex regex = new Regex(@"[\-\+]?[0-9]*[\.\,]?[0-9]+(?:[eE][\-\+]?[0-9]+)?[\s][\-\+]?[0-9]*[\.\,]?[0-9]+(?:[eE][\-\+]?[0-9]+)?[\s][\-\+]?[0-9]*[\.\,]?[0-9]+(?:[eE][\-\+]?[0-9]+)?[\s][\'][\p{IsCyrillic} ]+[\']");
                    Regex regex2 = new Regex(@"[\-\+]?[0-9]*[\.\,]?[0-9]+(?:[eE][\-\+]?[0-9]+)?");
                  
                    Regex regexForNumber = new Regex(@"(?=.)([+-]?(?=[\d\.])(\d*)(\.(\d+))?)");
                    Regex regexForWord2 = new Regex(@"[\'][\p{IsCyrillic} ]+[\']");

                    MatchCollection templateString = regex.Matches(line);
                    if (templateString.Count == 0)
                    {
                        Console.WriteLine("\'неизвестная ошибка\'");
                        continue;
                    }

                    MatchCollection numbersMatch = regex2.Matches(line);
                    double a = 0;
                    double b = 0;
                    double c = 0;

                    if (numbersMatch.Count > 0)
                    {
                        a = double.Parse(numbersMatch[0].Value.ToString());
                        Console.WriteLine(a);
                        b = double.Parse(numbersMatch[1].Value.ToString());
                        Console.WriteLine(b);
                        c = double.Parse(numbersMatch[2].Value.ToString());
                        Console.WriteLine(c);
                    }
                    
                    MatchCollection expectedValueMatch = regexForWord2.Matches(line);
                    string expectedResult = expectedValueMatch[0].Value;
                    
                    triangle.Triangle triangle = new triangle.Triangle();
                    string result = triangle.getTriangleType(a, b, c);
                    Console.WriteLine(result);
                    Console.WriteLine(expectedResult);
                    Console.WriteLine();
                    if (result == expectedResult)
                    {
                        fOutput.WriteLine("success");
                    }
                    else
                    {
                        fOutput.WriteLine("error");
                    }
                    
                }
            }
        }
    }
}
