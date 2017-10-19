using System;
using System.Linq;
using WCFOdataClient.Netflix;

namespace WCFOdataClient
{
    class Program
    {
        static void Main()
        {
            // make a LINQ provider for the Netflix odata endpoint
            var netflix = new NetflixCatalog(
                new Uri("http://odata.netflix.com/Catalog"));
            // make a query that looks up all the Netflix moview
            var titleQuery = from title in netflix.Titles select title;
            // keep track of number of titles returned by the query
            var titleCount = 0;
            // execute the query and write the results to the screen
            foreach (var title in titleQuery)
            {
                titleCount++;
                Console.WriteLine(title.Name);
            }
            // show how many titles were returned
            Console.WriteLine("==============\n{0}", titleCount);

        }
    }
}
