using System.Text.RegularExpressions;

namespace LambdaCalcInterpreter_C
{        
    internal static class Parser
    {
        #region RegexPatterns        
        private static readonly Regex vars = new (@"[a-z]+");
        internal static readonly Regex singleVar = new (@"[a-z]");
        #endregion

        #region Parser
        internal static List<KeyValuePair<string, char>> Parse(string expression)
        {

            /*
            *   L is the function keyword
            *   ( and ) are expression delimiters
            *   . is the return operator
            *   [a-z] are identifiers used as arguments, returns and variables 
            */


            var tokens = new List<KeyValuePair<string, char>>();
            int charIndex = 0;
            foreach(char c in expression)
            {
                switch (c)
                {
                    case 'L':
                        tokens.Add(new KeyValuePair<string, char>("keyword", c));
                        break;
                    case '(':
                        tokens.Add(new KeyValuePair<string, char>("separator", c));
                        break;
                    case ')':
                        tokens.Add(new KeyValuePair<string, char>("separator", c));
                        break;
                    case '.':
                        tokens.Add(new KeyValuePair<string, char>("operator", c));
                        break;
                    default:
                        Match var = singleVar.Match(c.ToString());
                        if (var.Success)
                        {
                            tokens.Add(new KeyValuePair<string, char>("identifier", c));
                            break;
                        }
                        else
                        {
                            Console.Error.WriteLine("Parser Error: Unexpected Character at Character {0}", charIndex.ToString());
                            Environment.Exit(1);
                            break;
                        }                        
                }                
                charIndex++;
            }
            return tokens;
        }

        #endregion
        
        #region SecondaryParser
        //Secondary Parser used when desugaring expression
        //Parses just the expression's variables
        internal static string[] GetVarsArray(string str)
        {
            var matches = vars.Matches(str)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();
            return matches;
        }
        #endregion


    }
}
