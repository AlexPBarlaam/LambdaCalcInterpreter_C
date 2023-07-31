using System.Text.RegularExpressions;

namespace LambdaCalcInterpreter_C
{
    internal static class Parser
    {
        #region RegexPatterns        
        private static Regex function = new Regex(@"LAMBDA [a-z]+\.[a-z]+");
        private static Regex application = new Regex(@"\(LAMBDA [a-z]+\.[a-z]+\)[a-z]+");
        private static Regex boundVar = new Regex(@"LAMBDA [a-z]+\.");
        private static Regex var = new Regex(@"[a-z]+");
        #endregion

        #region Parser
        
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

            var boundVarMatches = boundVar.Matches(str)
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
                { "boundVars", boundVarMatches},
                { "vars", varMatches}
            };
            return matches;
        }
        
        //Secondary Parser used when desugaring expression
        internal static string[] getVarsArray(string str) 
        { 
            var matches = var.Matches(str)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();
            return matches;
        }
        #endregion
    }    
}
