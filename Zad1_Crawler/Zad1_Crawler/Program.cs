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

            using HttpClient httpclient = new ();

            HttpResponseMessage result = await httpclient.GetAsync(url);




        }
    }
}