using iti.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iti.screens.Product
{
    public partial class Productform : Form
    {
        string imagePath;

        companyEntities2 db = new db.companyEntities2();
        public Productform()
        {
            InitializeComponent();
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_namepro.Text == "" || txt_barcode.Text == "" || txt_price.Text == "")
            {
                MessageBox.Show("Please enter required data");
            }
         
            else
            {
                iti.db.product p = new iti.db.product();
                p.name = txt_namepro.Text;
                p.code = txt_barcode.Text;
                p.price = decimal.Parse(txt_price.Text);
                p.notes = txt_notes.Text;
                p.cat_id =int.Parse (comboBox1.SelectedValue.ToString());
                db.products.Add(p);
                db.SaveChanges();
                MessageBox.Show("saved");
                if (imagePath != "")
                {
                    string newPath = Environment.CurrentDirectory + "\\images\\products\\" + p.id + ".jpg";
                    System.IO.File.Copy(imagePath, newPath);
                    p.image = imagePath;
                    db.SaveChanges();
                }
                txt_barcode.Text = "";
                txt_namepro.Text = "";
                txt_notes.Text = "";
                txt_price.Text = "";
                txt_quantity.Text = "";


            }
        }
        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_notes_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = dialog.FileName;
                pictureBox1.ImageLocation = dialog.FileName;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Productform_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companyDataSet1.category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.companyDataSet1.category);

        }
    }
}
