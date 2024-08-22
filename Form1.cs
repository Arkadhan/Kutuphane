using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class Form1 : Form
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        public Form1()
        {
            InitializeComponent();
        }
        void Vericek()
        {
            var liste = db.KitapTable.ToList();
            listBox1.DataSource = liste;
            listBox1.DisplayMember = "KitapAdi";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Vericek();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            KitapTable kitap = new KitapTable();
            kitap.KitapAdi = tbEkle.Text;

            db.KitapTable.Add(kitap);
            db.SaveChanges();
            Vericek();
            MessageBox.Show("Veri tabanına kitap eklendi", "Ekleme", MessageBoxButtons.OK);
            tbEkle.Text = "";
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var kitap = listBox1.SelectedItem as KitapTable;

            tbSil.Text = kitap.KitapAdi.ToString();

            if (kitap != null)
            {
                DialogResult cevap = MessageBox.Show(kitap.KitapAdi + " Kitabı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);

                if (cevap == DialogResult.Yes)
                {
                    db.KitapTable.Remove(kitap);
                    db.SaveChanges();
                    Vericek();
                    MessageBox.Show("Veri tabanından kitap başarıyla silindi", "Silindi", MessageBoxButtons.OK);
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            var kitap = listBox1.SelectedItem as KitapTable; 
            
            if (kitap != null)
            {
                kitap.KitapAdi=tbGuncelle.Text;
                db.SaveChanges();
                Vericek();
                MessageBox.Show("Veri tabanından kitap başarıyla güncellendi", "Güncelleme", MessageBoxButtons.OK);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var kitap=listBox1.SelectedItem as KitapTable;

            if (kitap != null)
            {
                tbGuncelle.Text=kitap.KitapAdi;
                tbSil.Text = kitap.KitapAdi;                   
            }
            else {
                tbGuncelle.Text = "";
                tbSil.Text = "";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Hide();

        }
    }
}
