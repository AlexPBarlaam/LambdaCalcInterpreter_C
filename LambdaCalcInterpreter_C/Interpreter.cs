namespace LambdaCalcInterpreter_C
{
    
    internal static class Interpreter
    {
        #region SyntacticSugar
        internal static Dictionary<string, string[]> Desugar(Dictionary<string, string[]> matches)
        {
            //LAMBDA xyz.xyz --> LAMBDA x.(LAMBDA y.(LAMBDA z.xyz))

            bool SyntacticSugar = false;

            //Checks if the expressions has syntactic sugar
            if (matches["vars"][0].Length > 1) //This IF statement needs to be edited to go through every var, not just the first one
            {
                SyntacticSugar = true;
            }

            if (SyntacticSugar == true)
            {
                Dictionary<string, string[]> newMatches = matches;

                Console.WriteLine("before:");
                Program.tokenPrint(newMatches);

                //Loops through the vars from the parser to desugar them 
                for (int i = 0; i < matches["vars"].Length; i++)
                {

                    if (i >= 1)
                    {
                        if (string.Equals(matches["vars"][i], matches["vars"][i - 1]))
                        {
                            continue;
                        }
                    }

                    if (matches["vars"][i].Length > 1)
                    {
                        Console.WriteLine("inside loop");
                        Console.WriteLine(matches["vars"][i]);
                        Program.tokenPrint(matches["functions"]);
                        Console.WriteLine("=============================================");

                        char[] singleVars = matches["vars"][i].ToCharArray();
                        string newFunc = null;
                        int bracketCount = 0;

                        foreach (char var in singleVars)
                        {
                            string varStr = var.ToString();
                            if (string.IsNullOrEmpty(varStr))
                            {
                                newFunc += "LAMBDA " + varStr + ".";
                            }
                            else
                            {
                                newFunc += "(LAMBDA " + varStr + ".";
                            }
                            bracketCount++;
                        }

                        string endBrackets = null;

                        for (int j = 0; j < bracketCount; j++)
                        {
                            endBrackets += ")";
                        }

                        newFunc += matches["vars"][i] + endBrackets;
                        Console.WriteLine(newFunc);
                    }
                }

                Console.WriteLine("after:");
                Program.tokenPrint(newMatches);

                return newMatches;
            }
            else
            {
                return matches;
            }        
        }
        #endregion
        
        #region Interpreter
        internal static void AlphaConversion()
        {
            /*
            MatchCollection newVars;

            bool Duplicates = false;
            foreach (string var in vars)
            {
                if (var == "x") { }
            }


            if (Duplicates == true)
            {


                return newVars;
            }
            else { return; }*/
        }

        internal static void BetaReduction()
        {
        
        }

        #endregion

        #region Search Algorithm
        internal static void getDuplicateVars(string[] vars) 
        {
            Program.tokenPrint(vars);            
        }
        #endregion
    }
}
