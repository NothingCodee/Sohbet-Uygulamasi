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
    public partial class arkadaslar : UserControl
    {
        public static string arkadas_ismi;
        public static string arkadas_id;
        public arkadaslar()
        {
            InitializeComponent();
        }

        private void arkadaslar_Load(object sender, EventArgs e)
        {
            button1.Text = arkadas_ismi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sohbet.arkadas_secili_ismi = arkadas_id;
            sohbet.arkadas_ismi = arkadas_ismi;
        }
    }
}
