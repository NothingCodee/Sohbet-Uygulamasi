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
using FireSharp.Response;
using FireSharp.Interfaces;

namespace sohbet_uygulamsı
{
    public partial class sohbet : Form
    {
        public static string kullanici_adi;
        public static string msg_goster_id;
        public static string arkadas_ismi;
        public static string arkadas_secili_ismi = "yok";
        public sohbet()
        {
            InitializeComponent();
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "bT1yHdnZrncAsW4ielWJkvWjyWCcPxL1ZXaK4CVe",
            BasePath = "https://sohbet-uygulamasi-6bda2-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        private void sohbet_Load(object sender, EventArgs e)
        {
            timer1.Start();
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch
            {
                MessageBox.Show("Sunucuya bağlanırken bir sorun oluştu! Lütfen daha sonra tekrar deneyiniz.");
            }
            arkadas_yukle();
        }
        int ark_sayisi;
        private void arkadas_yukle()
        {
            flowLayoutPanel1.Controls.Clear();
            var arkadas = client.Get($"Uyeler/{kullanici_adi}/Arkadaslar/");
            ark_sinifi ark = arkadas.ResultAs<ark_sinifi>();
            ark_sayisi = Convert.ToInt32(ark.ark_sayisi);

            for(int i = 0; i != ark_sayisi; i++)
            {
                var arkadass = client.Get($"Uyeler/{kullanici_adi}/Arkadaslar/arkadas/ark{i.ToString()}");
                ark_sinifi arkk = arkadass.ResultAs<ark_sinifi>();

                arkadaslar.arkadas_ismi = arkk.ismi;
                arkadaslar.arkadas_id = i.ToString();

                arkadaslar a = new arkadaslar();
                flowLayoutPanel1.Controls.Add(a);
            }
        }
        int msg_sayisi;
        public static string secili_id;
        int sec = 0;
        private void msggg()
        {

            
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(arkadas_secili_ismi != "yok")
            {
                flowLayoutPanel2.Controls.Clear();
                var arkadas = client.Get($"Uyeler/{kullanici_adi}/Arkadaslar/arkadas/ark{arkadas_secili_ismi}/");
                ark_msg_sinifi ark = arkadas.ResultAs<ark_msg_sinifi>();
                msg_sayisi = Convert.ToInt32(ark.msg_sayisi);
                mesaj_yukleme_tick = Convert.ToInt32(ark.msg_sayisi) - 1;
                for (int i = 0; i != msg_sayisi; i++)
                {
                    var ark_msg = client.Get($"Uyeler/{kullanici_adi}/Arkadaslar/arkadas/ark{arkadas_secili_ismi}/msgler/msg{i.ToString()}");
                    ark_msg_sinifi ark_msgg = ark_msg.ResultAs<ark_msg_sinifi>();

                    if(ark_msgg.kimden != kullanici_adi)
                    {
                        gelen_msg a = new gelen_msg();
                        gelen_msg.secili_kim = ark_msgg.kimden;
                        gelen_msg.mesaj = ark_msgg.msg;


                        flowLayoutPanel2.Controls.Add(a);
                    }
                    else if(ark_msgg.kimden == kullanici_adi)
                    {
                        gonderen_msg g = new gonderen_msg();
                        gonderen_msg.mesaj = ark_msgg.msg;

                        flowLayoutPanel2.Controls.Add(g);
                    }
                }
                secili_arkadas = arkadas_secili_ismi;
                
                arkadas_secili_ismi = "yok";
            }

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mesaj_gonder();
        }
        int ark_msg_sayisi;
        string secili_arkadas;
        private void mesaj_gonder()
        {
            var msg_sayisii = client.Get($"Uyeler/{kullanici_adi}/Arkadaslar/arkadas/ark{secili_arkadas}/");
            ark_msg_sinifi ark_msgg = msg_sayisii.ResultAs<ark_msg_sinifi>();
            ark_msg_sayisi = Convert.ToInt32(ark_msgg.msg_sayisi);

            ark_msg_sinifi msg_gonder = new ark_msg_sinifi
            {
                kimden=kullanici_adi,
                msg=textBox2.Text
            };
            var msg_olustur = client.Set($"Uyeler/{kullanici_adi}/Arkadaslar/arkadas/ark{secili_arkadas}/msgler/msg{msg_sayisi.ToString()}/", msg_gonder);
            msg_sayisi++;
            msg_sayisi_arttir();
        }

        private void msg_sayisi_arttir()
        {
            ark_msg_sinifi msg_gonder = new ark_msg_sinifi
            {
                msg_sayisi=msg_sayisi.ToString()
            };
            var msg_olustur = client.Update($"Uyeler/{kullanici_adi}/Arkadaslar/arkadas/ark{secili_arkadas}/", msg_gonder);
            karsi_taraf_id_al();
        }
        private void karsi_taraf_id_al()
        {
            var msg_sayisi = client.Get($"Uyeler/{arkadas_ismi}/Arkadaslar/");
            ark_sinifi ark = msg_sayisi.ResultAs<ark_sinifi>();
            ark_sayisi = Convert.ToInt32(ark.ark_sayisi);

            for (int i = 0; i != ark_sayisi; i++)
            {
                if(ark.ismi == kullanici_adi)
                {
                    bizim_arkadas_id = i;
                }
            }
            mesaj_gonder_karsi();
        }
        int bizim_arkadas_id;
        private void mesaj_gonder_karsi()
        {
            var msg_sayisi = client.Get($"Uyeler/{arkadas_ismi}/Arkadaslar/arkadas/ark{bizim_arkadas_id.ToString()}/");
            ark_msg_sinifi ark_msgg = msg_sayisi.ResultAs<ark_msg_sinifi>();
            ark_msg_sayisi_karsi = Convert.ToInt32(ark_msgg.msg_sayisi);

            ark_msg_sinifi msg_gonder = new ark_msg_sinifi
            {
                kimden = kullanici_adi,
                msg = textBox2.Text
            };
            var msg_olustur = client.Set($"Uyeler/{arkadas_ismi}/Arkadaslar/arkadas/ark{bizim_arkadas_id.ToString()}/msgler/msg{ark_msg_sayisi.ToString()}/", msg_gonder);
            ark_msg_sayisi_karsi++;
            msg_sayisi_arttir_karsi();
        }
        int ark_msg_sayisi_karsi;
        private void msg_sayisi_arttir_karsi()
        {
            ark_msg_sinifi msg_gonder = new ark_msg_sinifi
            {
                msg_sayisi = ark_msg_sayisi_karsi.ToString()
            };
            var msg_olustur = client.Update($"Uyeler/{arkadas_ismi}/Arkadaslar/arkadas/ark{bizim_arkadas_id.ToString()}/", msg_gonder);
            textBox2.Clear();
        }
        int mesaj_yukleme_tick;
        private void yukleme()
        {
  
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            var msg_sayisii = client.Get($"Uyeler/{kullanici_adi}/Arkadaslar/arkadas/ark{secili_arkadas}/");
            ark_msg_sinifi ark_msgg = msg_sayisii.ResultAs<ark_msg_sinifi>();
            

            if(mesaj_yukleme_tick != Convert.ToInt32(ark_msgg.msg_sayisi) - 1)
            {

            }
        }
    }


}
