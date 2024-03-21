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

namespace iti.screens.customar
{
    public partial class addcustomar : Form
    {
        companyEntities2 db = new companyEntities2();
        int id;
        iti.db.customar result;
        string imagePath = "";
        public addcustomar()
        {
            InitializeComponent();
           dataGridView1.DataSource= db.customars.ToList();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void addcustomar_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void refresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.customars.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_clname.Text == "")
                dataGridView1.DataSource = db.customars.Where(x => x.phone.Contains( txt_clnum.Text)).ToList();
            else dataGridView1.DataSource = db.customars.Where(x => x.phone.Contains( txt_clnum.Text) || x.name.Contains(txt_clname.Text)).ToList();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txt_namec.Text == "" || txt_phonec.Text=="")
            {
                MessageBox.Show("Please enter required data");
            }
            else
            {
                iti.db.customar c = new iti.db.customar();
                c.name = txt_namec.Text;
                c.phone = txt_phonec.Text;
                c.address =txt_addressc.Text;
                c.notes = txt_notesc.Text;
                c.email = txt_emailc.Text;
                c.company = txt_companyc.Text;
                c.isactive = checkactive.Checked;
                db.customars.Add(c);
                db.SaveChanges();
                MessageBox.Show("saved");
                if (imagePath != "")
                {
                    string newPath = Environment.CurrentDirectory + "\\images\\cust\\" + c.id + ".jpg";
                    System.IO.File.Copy(imagePath, newPath);
                    c.image = imagePath;
                    db.SaveChanges();
                }
                txt_namec.Text = "";
                txt_phonec.Text= "";
                txt_addressc.Text = "";
                txt_notesc.Text ="";
                txt_emailc.Text ="";
                txt_companyc.Text ="";
                checkactive.Checked = false;

            }
            dataGridView1.DataSource = db.customars.ToList();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var result = db.customars.SingleOrDefault(x => x.id == id);
            
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
                string newPath = Environment.CurrentDirectory + "\\images\\cust\\" + this.result.id + ".jpg";
                System.IO.File.Copy(imagePath, newPath, true);
                this.result.image = imagePath;
                db.SaveChanges();
            }
            MessageBox.Show("Changes saved");
            dataGridView1.DataSource = db.customars.ToList();
        }

        private void txt_notesc_TextChanged(object sender, EventArgs e)
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                result = db.customars.SingleOrDefault(x => x.id == id);

                txt_namec.Text = result.name;
                txt_phonec.Text= result.phone;
                txt_addressc.Text = result.address;
                txt_notesc.Text = result.notes;
                txt_emailc.Text = result.email;
                txt_companyc.Text = result.company;
                if(result.isactive == null)
                {
                    result.isactive=false;
                }
                checkactive.Checked =(bool) result.isactive;
                pictureBox1.ImageLocation = result.image;

              
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("sure for deleting?", "delete", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                var result = db.customars.Find(id);
                db.customars.Remove(result);
                db.SaveChanges();
                MessageBox.Show("deleted");
                dataGridView1.DataSource = db.customars.ToList();
            }
        }
    }
}
