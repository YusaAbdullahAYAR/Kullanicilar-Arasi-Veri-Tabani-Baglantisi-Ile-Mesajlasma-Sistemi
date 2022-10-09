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
namespace MesajTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string numara;
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-RBCL1FM\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True");

        void gelenkutusu()
        {
             SqlDataAdapter da1 = new SqlDataAdapter("Select * from TBLMESAJLAR WHERE ALICI="+numara,baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource=dt1;

        }
        void gidenkutusu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from TBLMESAJLAR WHERE GONDEREN=" + numara, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;
            gelenkutusu();
            gidenkutusu();

            // Ad Soyad çekme

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select AD,SOYAD FROM TBLKISILER WHERE NUMARA="+numara,baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[0]+ " " + dr[1];
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLMESAJLAR (GONDEREN,ALICI,BASLIK,ICERIK) VALUES (@P1,@P2,@P3,@P4)", baglanti);
            komut.Parameters.AddWithValue("@P1", numara);
            komut.Parameters.AddWithValue("@P2", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@P3", textBox1.Text);
            komut.Parameters.AddWithValue("@P4", richTextBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesajınız İletildi");
            gidenkutusu();


        }
    }
}
