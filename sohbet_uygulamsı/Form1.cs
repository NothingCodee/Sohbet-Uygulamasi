using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace sohbet_uygulamsı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "bT1yHdnZrncAsW4ielWJkvWjyWCcPxL1ZXaK4CVe",
            BasePath = "https://sohbet-uygulamasi-6bda2-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        private void button1_Click(object sender, EventArgs e)
        {
            Giris_yap();
        }
        private void Giris_yap()
        {
            var uyeler = client.Get($"Uyeler/{textBox1.Text}/");
            uye_sinifi uye = uyeler.ResultAs<uye_sinifi>();
            if(uye != null)
            {
                if (textBox2.Text == uye.Parola)
                {
                    sohbet.kullanici_adi = textBox1.Text;
                    this.Hide();
                    sohbet fr2 = new sohbet();
                    fr2.Show();
                }
                else
                {
                    MessageBox.Show("Parola / Kullanıcı Adı yanlış.");
                }
            }
            else
            {
                MessageBox.Show("Parola / Kullanıcı Adı yanlış.");
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch
            {
                MessageBox.Show("Sunucuya bağlanırken bir sorun oluştu! Lütfen daha sonra tekrar deneyiniz.");
            }
        }
    }
}
