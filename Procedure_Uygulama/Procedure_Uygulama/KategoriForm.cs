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
    public partial class KategoriForm : Form
    {
        public KategoriForm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Server=localhost;Database=Northwind;Integrated Security=true");
        private void KategoriForm_Load(object sender, EventArgs e)
        {
            Kategoriler_Listele();
        }

        private void Kategoriler_Listele()
        {
            SqlDataAdapter adp = new SqlDataAdapter("Kategoriler_Listele", baglanti);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dgvKategoriler.DataSource = dt;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("KategorilerYeni",baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kategoriAdi", txtKategoriAdi.Text);
            cmd.Parameters.AddWithValue("@tanimi", txtTanimi.Text);
            baglanti.Open();
            int ekl = cmd.ExecuteNonQuery();
            baglanti.Close();
            if (ekl>0)
            {
                MessageBox.Show("Urun Eklendi");
                Kategoriler_Listele();
            }
            else
            {
                MessageBox.Show("Islem basarisiz");
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvKategoriler.CurrentRow.Cells!=null)
            {
                SqlCommand cmd = new SqlCommand("Kategori_Sil", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;
                int id = Convert.ToInt32(dgvKategoriler.CurrentRow.Cells["KategoriID"].Value);
                cmd.Parameters.AddWithValue("@kategoriID", id );
                baglanti.Open();
                int ekl = cmd.ExecuteNonQuery();
                baglanti.Close();
                if (ekl > 0)
                {
                    MessageBox.Show("Veri Silinmisdir");
                    Kategoriler_Listele();

                }
                else
                {
                    MessageBox.Show("Islem basarisiz!");
                }

            }
            

        }

        private void dgvKategoriler_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Kategoriler_Update", baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", dgvKategoriler.CurrentRow.Cells["KategoriID"].Value);
            cmd.Parameters.AddWithValue("@adi", dgvKategoriler.CurrentRow.Cells["KategoriAdi"].Value);
            cmd.Parameters.AddWithValue("@tanimi", dgvKategoriler.CurrentRow.Cells["Tanimi"].Value);
            baglanti.Open();
            int ekl = cmd.ExecuteNonQuery();
            baglanti.Close();
            if (ekl>0)
            {
                MessageBox.Show("Guncelledin");
                Kategoriler_Listele();
            }
            else
            {
                MessageBox.Show("Islem basarisiz");
            }
        }
    }
}
