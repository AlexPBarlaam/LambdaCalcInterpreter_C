using static LambdaCalcInterpreter_C.Parser;
using static LambdaCalcInterpreter_C.Interpreter;

namespace LambdaCalcInterpreter_C
{ 
    class Program
    {
        static void Main(string[] args)
        {
            //test strings to be parsed, eventually will parse a string through command line
            string identityApplication = "((Lx .x)y)";
            string test = "((Lx.(Ly.(Lz.xyz)))abc)";
            
            /*
            Dictionary<string, string[]> matches = Parse(identityApplication);
            BetaReduction(identityApplication, matches);*/

            List<KeyValuePair<string,char>> tokens = AltParse(test);
            foreach(KeyValuePair<string,char> token in tokens)
            {
                Console.WriteLine("Key: {0}, Value: {1}",token.Key,token.Value);
            }
        }

        static string SyntacticCheck(string expression) 
        { 
            Console.WriteLine(expression);
            string[] vars = getVarsArray(expression);
            string? desugared = null;
            
            foreach (string var in vars) 
            { 
                if(var.Length > 1) 
                { 
                    desugared = Desugar(expression);
                    break;
                }
            }
            return desugared;
        }

        #region DictionaryPrinting        
        public static void dictPrint(Dictionary<string, string[]> tokens)
        {
            foreach(KeyValuePair<string, string[]> pair in tokens)
            {
                Console.WriteLine("Key: {0}", pair.Key);
                
                foreach(string value in pair.Value) 
                {
                    Console.WriteLine("Value: {0}", value); 
                }
            }
        }
        #endregion
    }

    /*TO DO:
     * DONE - Complete AltParser. May work better than the current parser. See Lexical Analysis for a model.
     * 
     * STARTED - Write Synthatic Check to only modify certain region if the expression for long expressions - this is what synctatic check is for
     * Finish Writing alphaConvertion
     * STARTED - Write betaReduction
    */

    /*KNOWN BUGS:
     * Curried functions, ie ((L x.(L y.(L z.xyz)))x) won't get entirely parsed. The parser will only recognise L z.xyz as a function
     * ^ Might be fixed with the new parser. EDIT: Is Fixed with new token parser
    */
}