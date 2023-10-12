using System.Text.RegularExpressions;

namespace LambdaCalcInterpreter_C
{        
    internal static class Parser
    {
        #region RegexPatterns        
        private static readonly Regex function = new Regex(@"L [a-z]+\.[a-z]+");
        private static readonly Regex application = new Regex(@"\(L [a-z]+\.[a-z]+\)[a-z]+");
        private static readonly Regex inputVar = new Regex(@"L [a-z]+\.");
        private static readonly Regex var = new Regex(@"[a-z]+");
        #endregion

        #region ParserRegex
        
        //Main Parser
        internal static Dictionary<string, string[]> Parse(string str)
        {
            //searches for regex matches and converts them to string[]
            var funcMatches = function.Matches(str)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();

            var appMatches = application.Matches(str)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();

            var boundVarMatches = inputVar.Matches(str)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();

            var varMatches = var.Matches(str)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();


            //compiles all matches in a dictionary and returns
            var matches = new Dictionary<string, string[]>()
            {
                { "functions", funcMatches },
                { "applications", appMatches},
                { "inputVar", boundVarMatches},
                { "vars", varMatches}
            };
            return matches;
        }
        
        //Secondary Parser used when desugaring expression
        //Parses just the expression's variable
        internal static string[] getVarsArray(string str) 
        { 
            var matches = var.Matches(str)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();
            return matches;
        }
        #endregion



        #region ParserTokens

        internal static readonly Regex singleVar = new Regex(@"[a-z]");

        internal static List<KeyValuePair<string, char>> AltParse(string expression)
        {

            /*
            *   L is the function keyword
            *   ( and ) are expression delimiters
            *   . is the return operator
            *   [a-z] are identifiers used as arguments and returns and variables 
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
    }
}
