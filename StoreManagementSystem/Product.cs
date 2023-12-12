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
            string str = "SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.reOrder FROM tbProduct AS p INNER JOIN tbBrand AS b ON b.id = p.brandId INNER JOIN tbCategory AS c ON c.id = p.categoryId";
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
    }
}
