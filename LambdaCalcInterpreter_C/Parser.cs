using System.Text.RegularExpressions;

namespace LambdaCalcInterpreter
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
        internal static MatchCollection[] Parse(string str) // put all regex matchcollections in an array for the interpreter?
        {
            //searches for regex matches
            MatchCollection funcMatches = application.Matches(str);
            MatchCollection appMatches = function.Matches(str);
            MatchCollection boundVarMatches = boundVar.Matches(str);
            MatchCollection varMatches = var.Matches(str);

            MatchCollection[] matches = { funcMatches, appMatches, boundVarMatches, varMatches };
            return matches;
        }
        #endregion
    }
}
