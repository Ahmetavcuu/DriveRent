using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriveRent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CustomersDBContext db = new CustomersDBContext();
        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                var customers = db.Customers.ToList();
                dataGridView1.DataSource = customers;

                
                dataGridView1.Columns["CustomerID"].HeaderText = "ID";
                dataGridView1.Columns["FirstName"].HeaderText = "Ad";
                dataGridView1.Columns["LastName"].HeaderText = "Soyad";
                dataGridView1.Columns["TCNo"].HeaderText = "Kimlik NO";
                dataGridView1.Columns["Phone"].HeaderText = "Telefon NO";
                dataGridView1.Columns["Email"].HeaderText = "E-Posta";
                dataGridView1.Columns["DriverLicenseNo"].HeaderText = "Ehliyet NO";
                dataGridView1.Columns["Address"].HeaderText = "Adres";

                dataGridView1.Columns["CustomerID"].Visible = false;
                dataGridView1.Columns["Rentals"].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                Customers newCustomers = new Customers()
                {
                    FirstName = txt_isim.Text,
                    LastName = txt_soyisim.Text,
                    TCNo = txt_tc.Text,
                    Phone = masked_telno.Text,
                    Email = txt_email.Text,
                    DriverLicenseNo = txt_ehliyet.Text,
                    Address = txt_adres.Text
                };

                db.Customers.Add(newCustomers);
                db.SaveChanges();

                MessageBox.Show("Yeni Müşteri Eklendi!");

                btn_listele.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void masked_telno_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txt_isim.Text = dataGridView1.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();

                txt_soyisim.Text = dataGridView1.Rows[e.RowIndex].Cells["LastName"].Value.ToString();

                txt_tc.Text = dataGridView1.Rows[e.RowIndex].Cells["TCNo"].Value.ToString();

                masked_telno.Text = dataGridView1.Rows[e.RowIndex].Cells["Phone"].Value.ToString();

                txt_email.Text = dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString();

                txt_ehliyet.Text = dataGridView1.Rows[e.RowIndex].Cells["DriverLicenseNo"].Value.ToString();

                txt_adres.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].Value.ToString();
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int selectedId = Convert.ToInt32(
                        dataGridView1.CurrentRow.Cells["CustomerID"].Value);
                    

                    Customers customers = db.Customers.Find(selectedId);

                    if (customers != null)
                    {
                        customers.FirstName = txt_isim.Text;
                        customers.LastName = txt_soyisim.Text;
                        customers.TCNo = txt_tc.Text;
                        customers.Phone = masked_telno.Text;
                        customers.Email = txt_email.Text;
                        customers.DriverLicenseNo = txt_ehliyet.Text;
                        customers.Address = txt_adres.Text;

                        db.SaveChanges();

                        MessageBox.Show("Müşteri güncellendi!");

                        btn_listele.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc = MessageBox.Show(
                            "Silmek istediğinize emin misiniz?",
                            "Silme Onayı",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    if (dataGridView1.CurrentRow != null)
                    {
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CustomerID"].Value);
                        Customers customers = db.Customers.Find(selectedId);
                        if (customers != null)
                        {
                            db.Customers.Remove(customers);
                            db.SaveChanges();
                            MessageBox.Show("Müşteri Silindi!");
                            btn_listele.PerformClick();
                        }
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show($"Hata = {ex.Message}"); }
        }

        private void btn_form_rent_Click(object sender, EventArgs e)
        {
            FormRental form2_cp = new FormRental();
            this.Hide();
            form2_cp.ShowDialog();
            this.Close();

        }
    }
}
