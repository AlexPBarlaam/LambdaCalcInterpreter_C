using System.Text.RegularExpressions;

namespace LambdaCalcInterpreter_C
{
    internal static class Parser
    {
        #region RegexPatterns
        /*regex patterns to generate tokens
            Function Syntax: "LAMBDA x.x"
            Application Syntax "(LAMBDA x.x)y"
        */
        private static Regex function = new Regex(@"LAMBDA [a-z]+\.[a-z]+");
        private static Regex application = new Regex(@"\(LAMBDA [a-z]+\.[a-z]+\)[a-z]+");
        private static Regex boundVar = new Regex(@"LAMBDA [a-z]+\.");
        private static Regex var = new Regex(@"[a-z]+");
        #endregion

        #region Parser
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
        #endregion
    }    
}
