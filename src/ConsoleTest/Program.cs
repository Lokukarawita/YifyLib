using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using YifyLib;
using YifyLib.Data;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {


            Yify y = new Yify(new Uri("https://yts.ag/api/v2/"));
            //y.Proxy = new System.Net.WebProxy("localhost", 8888);
            y.ApplicationKey = "aaaaaaa";
            var details = y.ListMovies(queryTerm:"Martian", quality: "3D", sortBy: SearchResultSort.Year, orderBy: YifyLib.SortOrder.Desc);
            var movie = y.GetMovie(details.First().ID, true, true);
            var suggestins = y.GetMovieSuggestion(details.First().ID);
            
            Console.ReadLine();

            //Application.Run(new Form1());
            //JavaScriptSerializer sr = new JavaScriptSerializer();
            //var data = sr.Deserialize<dynamic>("{'aa' : 'bb', 'obj' : {'objp' : '1'}, 'ary' :[1, 2, 3]}");
            //r val = data.aa;

            
            /*Console.ReadLine();*/

            //var movie = y.RegisterUser("");

           // Console.ReadLine();

            //XmlDocument d = new XmlDocument();
            //d.Load(@"D:\a.txt");
        }
    }
}
