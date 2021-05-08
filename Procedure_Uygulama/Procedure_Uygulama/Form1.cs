using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Procedure_Uygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Server=localhost;Database=NorthWind;Integrated Security=true");
        private void Form1_Load(object sender, EventArgs e)
        {
            UrunListele();

        }

        private void UrunListele()
        {
            SqlDataAdapter adp = new SqlDataAdapter("select*from Urunler where Sonlandi=0", baglanti);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UrunEkle",baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@urunAdi", txtUrunAdi.Text);
            cmd.Parameters.AddWithValue("@birimFiyati", nudFiyat.Value);
            cmd.Parameters.AddWithValue("@hedefStokDuzeyi", nudStok.Value);
            baglanti.Open();
            int ekl = cmd.ExecuteNonQuery();
            baglanti.Close();   
            if (ekl>0)
            {
                MessageBox.Show("Islem Basarili");
                UrunListele();
            }
            else
            {
                MessageBox.Show("Islem Basarisiz");
            }
           

        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow!=null)
            {
                SqlCommand cmd = new SqlCommand("UrunSil",baglanti);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@urunAdi", dataGridView1.CurrentRow.Cells["UrunAdi"].Value);
                baglanti.Open();
                int ekl = cmd.ExecuteNonQuery();
                baglanti.Close();
                if (ekl>0)
                {
                    MessageBox.Show("Veri Silinmisdir");
                    UrunListele();
                }
                else
                {
                    MessageBox.Show("Islem Basarisiz");
                }
            }
        }

        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            KategoriForm kf = new KategoriForm();
            kf.ShowDialog();
        }
    }
}
