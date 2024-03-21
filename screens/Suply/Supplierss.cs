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

namespace iti.screens.Suply
{
    public partial class Supplierss : Form
    {
        companyEntities2 db = new companyEntities2();
        int id;
        iti.db.supplier result;
        string imagePath = "";
        public Supplierss()
        {
            InitializeComponent();
            dataGridView1.DataSource = db.suppliers.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                result = db.suppliers.SingleOrDefault(x => x.id == id);

                txt_namec.Text = result.name;
                txt_phonec.Text = result.phone;
                txt_addressc.Text = result.address;
                txt_notesc.Text = result.notes;
                txt_emailc.Text = result.email;
                txt_companyc.Text = result.company;
                if (result.isactive == null)
                {
                    result.isactive = false;
                }
                checkactive.Checked = (bool)result.isactive;
                pictureBox1.ImageLocation = result.image;


            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txt_namec.Text == "" || txt_phonec.Text == "")
            {
                MessageBox.Show("Please enter required data");
            } 
            else if(db.suppliers.Where(x=>x.phone==txt_phonec.Text).Count()>0)
            {
                MessageBox.Show("user existed");

            }
            else
            {
                iti.db.supplier s = new iti.db.supplier();
                s.name = txt_namec.Text;
                s.phone = txt_phonec.Text;
                s.address = txt_addressc.Text;
                s.notes = txt_notesc.Text;
                s.email = txt_emailc.Text;
                s.company = txt_companyc.Text;
                s.isactive = checkactive.Checked;
                db.suppliers.Add(s);
                db.SaveChanges();
                MessageBox.Show("saved");
                if (imagePath != "")
                {
                    string newPath = Environment.CurrentDirectory + "\\images\\sup\\" + s.id + ".jpg";
                    System.IO.File.Copy(imagePath, newPath);
                    s.image = imagePath;
                    db.SaveChanges();
                }
                txt_namec.Text = "";
                txt_phonec.Text = "";
                txt_addressc.Text = "";
                txt_notesc.Text = "";
                txt_emailc.Text = "";
                txt_companyc.Text = "";
                checkactive.Checked = false;

            }
            dataGridView1.DataSource = db.suppliers.ToList();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var result = db.suppliers.SingleOrDefault(x => x.id == id);

            result.name = txt_namec.Text;
            result.phone = txt_phonec.Text;
            result.address = txt_addressc.Text;
            result.notes = txt_notesc.Text;
            result.email = txt_emailc.Text;
            result.company = txt_companyc.Text;
            result.isactive = checkactive.Checked;

            this.result.image = pictureBox1.ImageLocation;
            db.SaveChanges();
            if (imagePath != "")
            {
                string newPath = Environment.CurrentDirectory + "\\images\\sup\\" + this.result.id + ".jpg";
                System.IO.File.Copy(imagePath, newPath, true);
                this.result.image = imagePath;
                db.SaveChanges();
            }
            MessageBox.Show("Changes saved");
            dataGridView1.DataSource = db.suppliers.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("sure for deleting?", "delete", MessageBoxButtons.YesNo);
         
            if (r == DialogResult.Yes)
            {
                var result = db.suppliers.Find(id);
                db.suppliers.Remove(result);
                db.SaveChanges();
                MessageBox.Show("deleted");
                dataGridView1.DataSource = db.customars.ToList();
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.suppliers.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             if (txt_clname.Text == "")
                dataGridView1.DataSource = db.suppliers.Where(x => x.phone.Contains( txt_spnum.Text)).ToList();
            else dataGridView1.DataSource = db.suppliers.Where(x => x.phone.Contains( txt_spnum.Text) || x.name.Contains(txt_clname.Text)).ToList();

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
    }
}
