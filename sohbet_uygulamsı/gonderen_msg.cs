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
    public partial class gonderen_msg : UserControl
    {
        public static string mesaj;
        public gonderen_msg()
        {
            InitializeComponent();
        }

        private void gonderen_msg_Load(object sender, EventArgs e)
        {
            textBox1.Text = mesaj;
            label1.Text = sohbet.kullanici_adi;
        }
    }
}
