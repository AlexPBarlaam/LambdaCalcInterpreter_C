using static LambdaCalcInterpreter_C.Parser;
using static LambdaCalcInterpreter_C.Interpreter;

namespace LambdaCalcInterpreter_C
{ 
    class Program
    {
        static void Main(string[] args)
        {
            //test strings to be parsed, eventually will parse a string through command line
            string test = "(LAMBDA xyz.xyz)";
            string desugaredTest = "LAMBDA x.(LAMBDA y.(LAMBDA z.xyz))";

            Dictionary<string, string[]> testTokens = Parse(test);
            Dictionary<string, string[]> desugaredTestTokens = Parse(desugaredTest);
            //tokenPrint(testTokens);
            //tokenPrint(desugaredTestTokens);
            Dictionary<string, string[]> desugaredTokens = Desugar(testTokens);
        }

        #region MatchCollection Printing
        //Used to print all matches from a MatchCollection array for testing
        public static void tokenPrint(string[] tokens)
        {
            foreach (string match in tokens)
            {
                Console.WriteLine(match.ToString());
            }
        }
        
        public static void tokenPrint(Dictionary<string, string[]> tokens)
        {
            foreach(KeyValuePair<string, string[]> pair in tokens)
            {
                foreach(string match in pair.Value)
                {
                    Console.WriteLine(match);
                }
            }
        }
        #endregion
    }

    /*TO DO:
     * DONE - Pattern for individual variables so they can be interpreted and expressions can be de-suagred if needed
     * DONE - Maybe have a dictionary instead of an array for ease of recognition in the parser
     * 
     * IN PROGRESS - Write Desugarer
     * 
     * Finish Writing alphaConvertion
     * Write betaReduction
    */
}