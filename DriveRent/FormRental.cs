using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace DriveRent
{
    public partial class FormRental : Form
    {
        public FormRental()
        {
            InitializeComponent();
        }
        CustomersDBContext db = new CustomersDBContext();

        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                var list = db.Rentals
                    .Include("Customer")
                    .Include("Car.CarCategory")
                    .Select(r => new
                    {
                        r.RentalID,

                        MüşteriAdı = r.Customer.FirstName,
                        MüşteriSoyadı = r.Customer.LastName,

                        Araç = r.Car.Brand + " " + r.Car.Model,

                        Kategori = r.Car.CarCategory.CategoryName,

                        BaşlangicTarihi = r.RentDate,
                        BitisTarihi = r.ReturnDate,
                        ToplamFiyat = r.TotalPrice,

                        Durum = r.Status
                    })
                    .ToList();

                dataGridView1.DataSource = list;

                dataGridView1.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["RentalID"].Visible = false;

               
                dataGridView1.Columns["Durum"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }
        }

        private void FormRental_Load(object sender, EventArgs e)
        {
            try
            {
                
                combo_customer.DataSource = db.Customers
                    .OrderBy(c => c.FirstName)
                    .Select(c => new
                    {
                        c.CustomerID,
                        FullName = c.FirstName + " " + c.LastName
                    })
                    .ToList();

                combo_customer.DisplayMember = "FullName";
                combo_customer.ValueMember = "CustomerID";


                
                combo_car.DataSource = db.Cars
                    .Include("c.CarCategory")
                    .OrderBy(c => c.Brand)
                    .Select(c => new
                    {
                        c.CarID,
                        CarInfo = c.Brand + " " + c.Model + " (" + c.CarCategory.CategoryName + ")"
                    })
                    .ToList();

                combo_car.DisplayMember = "CarInfo";
                combo_car.ValueMember = "CarID";


              


                
                btn_listele.PerformClick();
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
                int selectedCustomerId = (int)combo_customer.SelectedValue;
                int selectedCarId = (int)combo_car.SelectedValue;

               
                var car = db.Cars
                    .Include("CarCategory")
                    .FirstOrDefault(x => x.CarID == selectedCarId);

                if (car == null)
                {
                    MessageBox.Show("Araç bulunamadı!");
                    return;
                }

                if (car.CarCategory == null)
                {
                    MessageBox.Show("Araç kategorisi yok!");
                    return;
                }

                int days = (dt_returndate.Value - dt_rentdate.Value).Days;

                if (days <= 0)
                {
                    MessageBox.Show("Tarih hatalı!");
                    return;
                }

                decimal totalPrice = days * car.CarCategory.DailyPrice;

                var newRental = new Rentals
                {
                    CustomerID = selectedCustomerId,
                    CarID = selectedCarId,
                    RentDate = dt_rentdate.Value,
                    ReturnDate = dt_returndate.Value,
                    
                    TotalPrice = totalPrice
                };

                db.Rentals.Add(newRental);
                db.SaveChanges();

                MessageBox.Show("Kiralama başarıyla eklendi.");

                btn_listele.PerformClick();
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
                        
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["RentalID"].Value);

                        
                        var recordToDelete = db.Rentals.Find(selectedId);

                        if (recordToDelete != null)
                        {
                           
                            db.Rentals.Remove(recordToDelete);
                            db.SaveChanges();

                            MessageBox.Show("Kiralama kaydı başarıyla silindi.");

                            
                            btn_listele.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen silmek için bir kayıt seçin.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    var result = db.Rentals
                        .GroupBy(r => new
                        {
                            r.Car.CarID,
                            r.Car.Brand,
                            r.Car.Model
                        })
                        .Select(g => new
                        {
                            CarName = g.Key.Brand + " " + g.Key.Model,
                            TotalCount = g.Count()
                        })
                        .OrderByDescending(x => x.TotalCount)
                        .FirstOrDefault();

                    if (result != null)
                    {
                        label_sonuc.Text = $"{result.CarName} {result.TotalCount} kez kiralanmış";
                    }
                    else
                    {
                        label_sonuc.Text = "Kayıt bulunamadı.";
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton2.Checked)
                {
                    
                    var result = db.Rentals
                        .GroupBy(r => new
                        {
                            r.Customer.CustomerID,
                            r.Customer.FirstName,
                            r.Customer.LastName
                        })
                        
                        .Select(g => new
                        {
                            FullName = g.Key.FirstName + " " + g.Key.LastName,
                            TotalCount = g.Count()
                        })
                        
                        .OrderByDescending(x => x.TotalCount)
                        .FirstOrDefault();

                    if (result != null)
                    {
                        label8.Text = $"{result.FullName} {result.TotalCount} kez araç kiralamış";
                    }
                    else
                    {
                        label8.Text = "Kayıt bulunamadı.";
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        private void btn_anaform_Click(object sender, EventArgs e)
        {
           
        }
        private void btn_form_customers_Click(object sender, EventArgs e)
        {
            Form1 form1_cp = new Form1();
            this.Hide();
            form1_cp.ShowDialog();
            this.Close();
        }
    }
    }
    

