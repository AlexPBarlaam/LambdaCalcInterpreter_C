using System.Collections;
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
            string[] vars = GetVarsArray(expression);
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

                    //Checks the length of the vars to see if they need desugaring
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
                                newFunc += "L" + varStr + ".";
                            }
                            else
                            {
                                newFunc += "(L" + varStr + ".";
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
        internal static void AlphaConversion()
        {

          
        }

        internal static void BetaReduction(List<KeyValuePair<string, char>> Tokens)
        {
            bool IsBound = false;
            Dictionary<int,char> vars = BuildHash(Tokens);
            
            foreach(KeyValuePair<int,char> var in vars)
            {
                Console.WriteLine("{0}:{1}",var.Key,var.Value);
                //Console.WriteLine(var.Value.GetHashCode);
            }

            /* 
             *
             */

        }
        
        
        internal static Dictionary<int, char> BuildHash(List<KeyValuePair<string,char>> Tokens)
        {
            Dictionary<int, char> hash = new Dictionary<int, char>();
            int i  = 0;
            foreach(KeyValuePair<string, char> kvp in Tokens)
            {
                if (kvp.Key == "identifier")
                {
                    hash.Add(i,kvp.Value);
                }
                i++;
            }            
            return hash;
        }
        #endregion
    }
}
