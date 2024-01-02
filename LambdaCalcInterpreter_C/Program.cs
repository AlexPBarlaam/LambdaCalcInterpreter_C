using static LambdaCalcInterpreter_C.Parser;
using static LambdaCalcInterpreter_C.Interpreter;
using System.Text.RegularExpressions;

namespace LambdaCalcInterpreter_C
{ 
    class Program
    {
        static void Main(string[] args)
        {
            //test strings to be parsed, eventually will parse a string through command line
            
            //string identityApplication = "((Lx .x)y)";
            //string CurriedExp = "((Lx.(Ly.(Lz.xyz)))abc)";
            string SugaredExp = "((Lxyz.xyz)abc)";

            string DesugaredExp = SyntacticCheck(SugaredExp);

            Console.WriteLine(DesugaredExp);

            List<KeyValuePair<string,char>> tokens = Parse(DesugaredExp);
            
            foreach(KeyValuePair<string,char> token in tokens)
            {
                Console.WriteLine("Key: {0}, Value: {1}",token.Key,token.Value);
            }
            Console.WriteLine("====================================================");
            BetaReduction(tokens);
        }
        static string SyntacticCheck(string expression)
        {
            Regex singleExp = new(@"\(L[a-z]+\.[a-z]+\)");
            
            Console.WriteLine(expression);
            string? desugared = null;
            
            //var matches = singleExp.Matches(expression).OfType<Match>().Select(m => m.Groups[0].Value).ToArray(); 
            string match = singleExp.Match(expression).ToString();

            string newExp = Desugar(match);

            desugared = expression.Replace(match , newExp);

            return desugared ?? expression; //return desugared, if it is empty return expression instead
        }
    }

    /*TO DO:
     * DONE - Complete AltParser. May work better than the current parser. See Lexical Analysis for a model.
     * DONE - Write Synthatic Check to only modify certain region if the expression for long expressions - this is what synctatic check is for
     * 
     * Finish Writing alphaConvertion
     * Write betaReduction
    */

    /*KNOWN BUGS:
     * FIXED - Curried functions, ie ((L x.(L y.(L z.xyz)))x) won't get entirely parsed. The parser will only recognise L z.xyz as a function
     * FIXED - Desugaring algorithm will desugar anything with multiples vars including unbound variables used in applications
    */

    /*NOTES 
     * Algorithms Flow should be:
     *      Desugar - Parse - Resolve(with A Convertion & B Reduction)
     *      
     * Syntactic check does work however bugs may occur when an expression has multiple region to desugar
     */

}