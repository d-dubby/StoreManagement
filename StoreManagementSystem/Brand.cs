using StoreManagementSystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagementSystem
{
    public partial class Brand : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
       
        
        public Brand()
        {
            InitializeComponent();
            cn = new SqlConnection(Properties.Settings.Default.Connection);
            LoadBrand();
            
        }

        //Data retrieve from tbBrand to dgBrand on Brand Form

        public void LoadBrand()
        {
            int i = 0;
            dgbrand.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT *FROM tbBrand ORDER BY brand",cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                dgbrand.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString());
            }
            dr.Close();
            cn.Close();

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            BrandDetails brandDetails = new BrandDetails(this);
            brandDetails.ShowDialog();
        }

        private void dgbrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //For update and delete brand by cellclick from tbBrand
            string colName = dgbrand.Columns[e.ColumnIndex].Name;
            if(colName == "Delete")
            {
                if(MessageBox.Show("Are you sure to delete this record?","POS",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbBrand WHERE id LIKE '" + dgbrand[1, e.RowIndex].Value.ToString()+ "'",cn);
                    cm.ExecuteNonQuery();
                    cn.Close() ;
                    MessageBox.Show("Brand has been sucessful deleted.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if(colName == "Edit")
            {
                BrandDetails brandDetails = new BrandDetails(this);
                brandDetails.lblid.Text = dgbrand[1, e.RowIndex].Value.ToString();
                brandDetails.txtbrand.Text = dgbrand[2,e.RowIndex].Value.ToString();
                brandDetails.btnsave.Enabled = false;
                brandDetails.btnupdate.Enabled = true;
                brandDetails.ShowDialog();
            }
            LoadBrand();
        }

        private void Brand_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
