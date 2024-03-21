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

namespace iti
{
    public partial class Form1 : Form
    {
        companyEntities2 db = new companyEntities2();
        string imagePath;
        public Form1()
        {
            InitializeComponent();


        }

     

        private void btn_save_Click(object sender, EventArgs e)
        {
            
                user obj = new user();
                obj.user_name = txt_username.Text;
                obj.password = txt_password.Text;
                obj.image = imagePath; 
                db.users.Add(obj);
                db.SaveChanges();
            string newPath = Environment.CurrentDirectory + "\\images\\users\\" + obj.id + ".jpg";
                System.IO.File.Copy(imagePath,newPath);
                MessageBox.Show("Saved");

            obj.image = imagePath;
            db.SaveChanges();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = dialog.FileName;
                pictureBox1.ImageLocation = dialog.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

