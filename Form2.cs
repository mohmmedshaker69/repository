using iti.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iti
{
    public partial class Form2 : Form
    {
        companyEntities2 db = new companyEntities2();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var result = db.users.Where(y => y.user_name == txt_user2.Text && y.password== txt_password2.Text).ToList();
            if (result.Count() > 0)
            { 
                this.Close();
                Thread th = new Thread(openform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else
            {
                MessageBox.Show("UserName or password invalid");
            }
        }
        void openform()
        {
            Application.Run(new MainForm());

        }
    }
}
