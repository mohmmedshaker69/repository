using iti.screens.customar;
using iti.screens.Product;
using iti.screens.Recipt;
using iti.screens.Suply;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void listProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            productlist p = new screens.Product.productlist();
            p.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            productlist p = new screens.Product.productlist();
            p.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Thread th = new Thread(openform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        void openform()
        {
            Application.Run(new Form1());

        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Productform p = new screens.Product.Productform();
            p.Show();
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addcustomar p = new screens.customar.addcustomar();
            p.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addcustomar p = new screens.customar.addcustomar();
            p.Show();
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Supplierss p = new screens.Suply.Supplierss();
            p.Show();
        }

        private void reciptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recipt p = new screens.Recipt.Recipt();
            p.Show();
        }
    }
}
