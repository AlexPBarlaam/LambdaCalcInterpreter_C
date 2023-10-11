using static LambdaCalcInterpreter_C.Program;
using static LambdaCalcInterpreter_C.Parser;

namespace LambdaCalcInterpreter_C
{

    internal static class Interpreter
    {
        #region SyntacticSugar
        internal static string Desugar(string expression)
        {
            //i.e. LAMBDA xyz.xyz --> LAMBDA x.(LAMBDA y.(LAMBDA z.xyz))

            bool SyntacticSugar = false;
            string[] vars = getVarsArray(expression);
            foreach (string var in vars)
            {
                //Checks if the expression has syntactic sugar
                if (var.Length > 1)
                {
                    SyntacticSugar = true;
                    break;
                }
            }

            if (SyntacticSugar == true)
            {
                string newFunc = "";

                //Loops through the vars from the parser to desugar them 
                for (int i = 0; i < vars.Length; i++)
                {

                    //Compares the current and previous var to avoid desugaring the same thing twice
                    if (i >= 1)
                    {
                        if (string.Equals(vars[i], vars[i - 1]))
                        {
                            continue;
                        }
                    }

                    //Checks the lenght of the vars to see if they need desugaring
                    if (vars[i].Length > 1)
                    {
                        char[] singleVars = vars[i].ToCharArray(); //splits the sugared vars into individual vars
                        int bracketCount = 0;

                        //desugars the vars into the currents equivalent expression
                        foreach (char var in singleVars)
                        {
                            string varStr = var.ToString();
                            if (string.IsNullOrEmpty(varStr))
                            {
                                newFunc += "L " + varStr + ".";
                            }
                            else
                            {
                                newFunc += "(L " + varStr + ".";
                            }
                            bracketCount++;
                        }

                        string endBrackets = "";

                        //adds the correct number of brackets at the end of the desuraged expression
                        for (int j = 0; j < bracketCount; j++)
                        {
                            endBrackets += ")";
                        }

                        newFunc += vars[i] + endBrackets;

                        Console.WriteLine("New Expression:" + newFunc);
                    }
                }

                return newFunc;
            }

            else
            {
                return expression;
            }
        }
        #endregion

        #region Interpreter
        internal static void AlphaConversion(string[] vars)
        {

            string[] newVars;

            bool Duplicates = false;
            foreach (string var in vars)
            {
                if (var == "x") { }
            }

            if (Duplicates == true)
            {
                return;
            }
            else { return; }
        }

        internal static void BetaReduction(string exp, Dictionary<string, string[]> parseMatches)
        {
            bool isBound = false;
            dictPrint(parseMatches);
            //Checks for bound variables in the input variables
            foreach (string input in parseMatches["inputVar"])
            {
                //string[] retVars = findReturnVars();


            }
        }
        #endregion
        /*private static string[] findReturnVars(string inputVar, string exp) 
        {            
            //Helper function to find vars that are identical to our input var
            return ;
        }*/
    }
}
