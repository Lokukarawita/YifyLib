using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YifyLib;

namespace ConsoleTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Yify y = new Yify(new Uri("https://yts.ag/api/v2/"));
            //var m = y.GetMovie(3105, includeCast: true, includeImages: true);

            //foreach (var item in m.Actors)
            //{
            //    this.flowLayoutPanel1.Controls.Add(new Actor(item));
            //}
            
            Yify y = new Yify(new Uri("https://yts.ag/api/v2/"));
            //var v = y.Login("YifyLib", "911119");
            //var details = y.GetUserDetails(1288583);
        }
    }
}
