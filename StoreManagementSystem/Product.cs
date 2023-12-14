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
    public partial class Product : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        public Product()
        {
            InitializeComponent();
            cn = new SqlConnection(Properties.Settings.Default.Connection);
            LoadProduct();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgProduct.Rows.Clear();
            string str = "SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.reOrder FROM tbProduct AS p INNER JOIN tbBrand AS b ON b.id = p.brandId INNER JOIN tbCategory AS c ON c.id = p.categoryId WHERE CONCAT(p.pdesc, b.brand, c.category) LIKE '%" + txtsearch.Text+ "%'";
            cm = new SqlCommand(str,cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();
            
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            ProductDetails productDetails = new ProductDetails(this);
            productDetails.ShowDialog();
        }

        private void dgProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgProduct.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                ProductDetails product = new ProductDetails(this);
                product.txtPcode.Text = dgProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                product.txtbarcode.Text = dgProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                product.txtPdes.Text = dgProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                product.cbBrand.Text = dgProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                product.cbCategory.Text = dgProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                product.txtprice.Text = dgProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
                product.updownReOrder.Value = int.Parse(dgProduct.Rows[e.RowIndex].Cells[7].Value.ToString());

                product.txtPcode.Enabled = false;
                product.btnsave.Enabled = false;
                product.btnupdate.Enabled = true;
                product.ShowDialog();

            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure to delete this Product?", "Delete record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE pcode LIKE '" + dgProduct[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been sucessful deleted.", "Point of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                
            }
            LoadProduct();
        }

        private void txtsearch_TextChange(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
