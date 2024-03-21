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
    public partial class productlist : Form

    {
        companyEntities2 db = new companyEntities2();
        int id;
        iti.db.product result;
        string imagepath="";

        public productlist()
        {
            InitializeComponent();
            comboBox2.DataSource = db.categories.ToList();
            dataGridView1.DataSource= db.products.OrderBy(x=>x.price).ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productlist_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companyDataSet2.category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.companyDataSet2.category);
            // TODO: This line of code loads data into the 'companyDataSet.product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.companyDataSet.product);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_plname.Text=="")
            dataGridView1.DataSource = db.products.Where(x => x.code == txt_plcode.Text).ToList();
            else dataGridView1.DataSource = db.products.Where(x => x.code == txt_plcode.Text || x.name.Contains(txt_plname.Text)).ToList();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void refresh_Click(object sender, EventArgs e)
        {
           dataGridView1.DataSource= db.products.ToList();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                result = db.products.SingleOrDefault(x => x.id == id);

                txt_barcodeu.Text = result.code;
                txt_nameprou.Text = result.name;
                txt_notesu.Text = result.notes;
                txt_priceu.Text = result.price.ToString();
                comboBox1.SelectedValue = result.cat_id;

                pictureBox1.ImageLocation = result.image;
            }
            catch 
            {

            }

        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {
          
            
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            var result = db.products.SingleOrDefault(x => x.id == id);
            result.code = txt_barcodeu.Text;
            result.name = txt_nameprou.Text;
            result.notes = txt_notesu.Text;
            result.price = (decimal.Parse(txt_priceu.Text));
            result.image = pictureBox1.ImageLocation;
            result.cat_id =int.Parse( comboBox1.SelectedValue.ToString());
            db.SaveChanges();
            if (imagepath != "")
            {
                string newPath = Environment.CurrentDirectory + "\\images\\products\\" + result.id + ".jpg";
                System.IO.File.Copy(imagepath, newPath,true);
                result.image = imagepath;
                db.SaveChanges();
            }
            MessageBox.Show("Changes saved");
            dataGridView1.DataSource = db.products.ToList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagepath = dialog.FileName;
                pictureBox1.ImageLocation = dialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          var r=  MessageBox.Show("sure for deleting?","delete", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                var result = db.products.Find(id);
                db.products.Remove(result);
                db.SaveChanges();
                MessageBox.Show("deleted");
                dataGridView1.DataSource = db.products.ToList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Productform p = new screens.Product.Productform();
            p.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cid = int.Parse(comboBox2.SelectedValue.ToString());
            dataGridView1.DataSource = db.products.Where(x => x.cat_id == cid).ToList();
        }
    }
}
