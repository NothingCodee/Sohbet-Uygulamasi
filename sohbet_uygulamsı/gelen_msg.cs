using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sohbet_uygulamsı
{
    public partial class gelen_msg : UserControl
    {
        public static string mesaj;
        public static string secili_kim;
        public gelen_msg()
        {
            InitializeComponent();
        }

        private void gelen_msg_Load(object sender, EventArgs e)
        {
            textBox1.Text = mesaj;
            label1.Text = secili_kim;
        }
    }
}
