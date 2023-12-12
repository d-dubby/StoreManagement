using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagementSystem
{
    public partial class ProductDetails : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection db = new DBconnection();
        Product product;
        public ProductDetails(Product p)
        {
            InitializeComponent();
            cn = new SqlConnection(Properties.Settings.Default.Connection);
            product = p;
            LoadBrand();
            LoadCategory();
            
        }

        //To close the form 
        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void clear()
        {
            txtPcode.Clear();
            txtbarcode.Clear();
            txtPdes.Clear();
            txtprice.Clear();
            cbBrand.SelectedIndex = 0;
            cbCategory.SelectedIndex = 0;
            updownReOrder.Value = 1;

            txtPcode.Enabled = true;
            txtPcode.Focus();
            btnsave.Enabled = true;
            btnupdate.Enabled = false;
        }


        public void LoadCategory()
        {
            cbCategory.Items.Clear();
            cbCategory.DataSource = db.getTable("SELECT * FROM tbCategory");
            cbCategory.DisplayMember = "category";
            cbCategory.ValueMember = "id";
        }

        public void LoadBrand()
        {
            cbBrand.Items.Clear();
            cbBrand.DataSource = db.getTable("SELECT * FROM tbBrand");
            cbBrand.DisplayMember = "brand";
            cbBrand.ValueMember = "id";
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to save this Product?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String str = "INSERT INTO tbProduct(pcode,barcode,pdesc,brandId,categoryId,price,reOrder) VALUES(@pcode,@barcode,@pdesc,@brandId,@categoryId,@price,@reOrder)";
                    cm = new SqlCommand(str, cn);
                    cm.Parameters.AddWithValue("@pcode", txtPcode.Text);
                    cm.Parameters.AddWithValue("@barcode", txtbarcode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtPdes.Text);
                    cm.Parameters.AddWithValue("@brandId", cbBrand.SelectedValue);
                    cm.Parameters.AddWithValue("@categoryId", cbCategory.SelectedValue);
                    cm.Parameters.AddWithValue("@price", double.Parse(txtprice.Text));
                    cm.Parameters.AddWithValue("@reOrder", updownReOrder.Value);
                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Product has been successful saved", "Point of Sales");
                    clear();
                    product.LoadProduct();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
