using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //access komutlarına ulasmak icin kullandığımız kutuphane

namespace Kitaplik_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\accessDatabase\kitaplık.mdb");

        void Listele() //listele komutu
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter("Select * From kitaplar",baglanti);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void Ara()
        {
            OleDbCommand komut = new OleDbCommand("Select * From kitaplar where KitapAd like '%" + txtAra.Text + "%'", baglanti);
            komut.Parameters.AddWithValue("@p1", txtAra.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(komut);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }
        //Durum icin
        string durum;

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into kitaplar (KitapAd, Yazar, Tur,SayfaSayisi,Durum) values (@p1,@p2,@p3,@p4,@p5)",baglanti);
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtYazar.Text);
            komut.Parameters.AddWithValue("@p3",cmbTur.Text);
            komut.Parameters.AddWithValue("@p4",txtSayfa.Text);
            komut.Parameters.AddWithValue("@p5",durum);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Kayıt Edildi.");
            Listele();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            cmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Delete from kitaplar where KitapId = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Silindi");
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("update kitaplar set KitapAd = @p1, Yazar = @p2, Tur = @p3, SayfaSayisi = @p4,Durum = @p5 where KitapId = @p6", baglanti);
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtYazar.Text);
            komut.Parameters.AddWithValue("@p3",cmbTur.Text);
            komut.Parameters.AddWithValue("@p4",txtSayfa.Text);
            if (radioButton1.Checked == true)
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
            if (radioButton2.Checked == true)
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
            komut.Parameters.AddWithValue("@p6", txtId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Güncellendi");
            Listele();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("Select * From kitaplar where KitapAd = @p1",baglanti);
            komut.Parameters.AddWithValue("@p1",txtAra.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(komut);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            Ara();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            Ara();
        }
    }
}
