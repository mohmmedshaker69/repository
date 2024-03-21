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

namespace iti.screens.Recipt
{
    public partial class Recipt : Form
    {
        companyEntities2 db = new db.companyEntities2();
        List<iti.db.product> products;

        public Recipt()
        {
            InitializeComponent();
            products = db.products.ToList();
            imageList1.ImageSize = new Size(70, 70);

        }

        private void Recipt_Load(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'companyDataSet3.customar' table. You can move, or remove it, as needed.
            //this.customarTableAdapter.Fill(this.companyDataSet3.customar);
            this.customarTableAdapter1.FillBy(this.companyDataSet4.customar);
            this.customarTableAdapter1.FillBy1(this.companyDataSet4.customar);
            for (int i = 0; i < products.Count(); i++)
            {
               if(products[i].image != null)
                {
                    imageList1.Images.Add(Image.FromFile(products[i].image));
                 
                }
                else
                {
                    Bitmap btm =new Bitmap(70, 70);
                    imageList1.Images.Add(btm);
                }
                ListViewItem item = new ListViewItem();
                item.Text = products[i].name;
                item.ImageIndex = i;

                listView1.Items.Add(item);
                item.Tag = products[i];
            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
           

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void fillBy2ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.customarTableAdapter1.FillBy2(this.companyDataSet4.customar);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }   void calctotal()
        {
            try
            {
                decimal total = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    total += decimal.Parse(dataGridView1.Rows[i].Cells["Totalprice"].Value.ToString());
                }
                lblTotal.Text = total.ToString();
                decimal disc = decimal.Parse(txt_disc.Text);
                lblDiscount.Text = (total - disc).ToString();
            }
            catch { }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)

            {
               var pro=(product) listView1.SelectedItems[0].Tag;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Id"].Value.ToString()==pro.id.ToString())
                    {
                        dataGridView1.Rows[i].Cells["Quantity"].Value 
                            = int.Parse(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString())+1;
                       
                        dataGridView1.Rows[i].Cells["Totalprice"].Value = 
                            int.Parse(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString())
                           *
                           decimal.Parse(dataGridView1.Rows[i].Cells["Price"].Value.ToString());
                        calctotal();
                        return;
                    }
                }
            dataGridView1.Rows.Add(pro.id, pro.name, pro.price, 1, pro.price * 1,pro.image==null?
                new Bitmap(40,40): 
                Image.FromFile(pro.image));
                calctotal();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_disc_TextChanged(object sender, EventArgs e)
        {
            calctotal();
        }

        private void txt_disc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            iti.db.salesbill result = new iti.db.salesbill()
            {
                date = dateTimePicker1.Value.Date,
                discount = decimal.Parse(txt_disc.Text),
                total = decimal.Parse(lblTotal.Text),
                totalAdesc = decimal.Parse(lblDiscount.Text),
                notes = txt_notes.Text,
                customerid = int.Parse(comboBox1.SelectedValue.ToString()),

            };
            List<salesbilldetail> list = new List<salesbilldetail>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                list.Add(new salesbilldetail
                {
                    productid = int.Parse(dataGridView1.Rows[i].Cells["id"].Value.ToString()),
                    quantity = int.Parse(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString()),
                    price = decimal.Parse(dataGridView1.Rows[i].Cells["Price"].Value.ToString()),
                    totalprice = decimal.Parse(dataGridView1.Rows[i].Cells["Price"].Value.ToString())
                    * int.Parse(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString()),
                });
            }
            result.salesbilldetails = list;
            db.salesbills.Add(result);
            db.SaveChanges();
            MessageBox.Show("saved"+result.id);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_notes_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
