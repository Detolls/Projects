using System.Text.RegularExpressions;

namespace MarketDeskApp.Domain.Checkers
{
    /// <summary>
    /// Static class that stores input validation logic for pattern matching.
    /// </summary>
    public static class Checker
    {
        /// <summary>
        /// Method to validate user line input.
        /// </summary>
        /// <param name="line">User line input.</param>
        public static bool CheckLine(string line)
        {
            Regex pattern = new Regex(@"[A-zА-я]+,-*[0-9]+.*,[0-9]+");

            return pattern.IsMatch(line);
        }
    }
}
