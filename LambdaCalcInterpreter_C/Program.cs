using System;
using System.Text.RegularExpressions;
using static LambdaCalcInterpreter.Parser;  

namespace LambdaCalcInterpreter 
{ 
    class Program
    {
        static void Main(string[] args)
        {
            //test strings to be parsed, eventually will parse a string through command line
            string test = "(LAMBDA xy.xy)ab";

            MatchCollection[] testTokens = Parse(test);
            tokenPrint(testTokens);
        }

        //Used to print all matches from a MatchCollection array for testing
        static void tokenPrint(MatchCollection[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                foreach (Match match in tokens[i])
                {
                    Console.WriteLine(match.ToString());
                }
            }
        }    
    }

    /*TO DO:
     * Pattern for individual variables so they can be interpreted and expressions can be de-suagred if needed - DONE
     * Maybe have a dictionary instead of an array for ease of recognition in the parser
     * Write Interpreter
    */
}