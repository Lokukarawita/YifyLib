using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleTest
{
    public partial class Actor : UserControl
    {
        public Actor()
        {
            InitializeComponent();
        }

        public Actor(YifyLib.Data.Actor a)
            : this()
        {
            pictureBox1.LoadAsync(a.SmallImage);
            label1.Text = a.Name;
            label3.Text = a.CharacterName;
        }
    }
}
