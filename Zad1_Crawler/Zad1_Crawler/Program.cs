using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Zad1_Crawler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if(args.Length == 0)
            {
                throw new ArgumentNullException();
            }
            string url = args[0];

            string urlPattern = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&/=]*)";

            Regex urlRegex = new(urlPattern);

            if (!urlRegex.IsMatch(url))
            {
                throw new ArgumentException();
            }

            using HttpClient httpclient = new ();

            HttpResponseMessage result = await httpclient.GetAsync(url);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Błąd w czasie pobierania strony");
            }

            string htmlContent = await result.Content.ReadAsStringAsync();

            String emailPattern = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";

            Regex emailRegex = new(emailPattern, RegexOptions.IgnoreCase);

            MatchCollection matchedEmails = emailRegex.Matches(htmlContent);

            if(matchedEmails.Count == 0 )
            {
            throw new Exception("Nie znaleziono adresow Email");
            }

            HashSet<String> uniqueEmails = new();
            foreach (Match match in matchedEmails) 
            {
                uniqueEmails.Add(match.ToString());
            }

            foreach(string email in uniqueEmails) 
            {
            Console.WriteLine(email);
            }


        }
    }
}