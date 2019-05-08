using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Banka
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string hesap;

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-N9EER84;Initial Catalog=DbBanka;Integrated Security=True");

        private void Form2_Load(object sender, EventArgs e)
        {
            lblHesapNo.Text = hesap;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select AD,SOYAD,TC,TELEFON from TBLMUSTERILER where hesapno="+hesap,baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                lblAdSoyad.Text=dr[0]+" "+dr[1];
                lblTc.Text = dr[2].ToString();
                lblTelefon.Text = dr[3].ToString();
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select BAKIYE from TBLHESAP where HESAPNO=" + hesap, baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblBakiye.Text = dr2[0].ToString()+" TL";
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TBLHESAP set BAKIYE=BAKIYE+@P1 where HESAPNO=@P2", baglanti);
            komut.Parameters.AddWithValue("@P1", decimal.Parse(txtTutar.Text));
            komut.Parameters.AddWithValue("@P2", mskHesap.Text);
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("Update TBLHESAP set BAKIYE=BAKIYE-@P3 where HESAPNO=@P4", baglanti);
            komut2.Parameters.AddWithValue("@P3", decimal.Parse(txtTutar.Text));
            komut2.Parameters.AddWithValue("@P4", hesap);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Para başarıyla" + " " + mskHesap.Text.ToString() + " nolu hesaba aktarıldı.");
            mskHesap.Text = "";
            txtTutar.Text = "";
        }
    }
}
