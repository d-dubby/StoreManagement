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
    public partial class Category : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Category()
        {
            InitializeComponent();
            cn = new SqlConnection(Properties.Settings.Default.Connection);
            LoadCategory();
        }

        //Data retrieve from tbCategory to dgcategory on Category Form

        public void LoadCategory()
        {
            int i = 0;
            dgCategory.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT *FROM tbCategory ORDER BY category", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgCategory.Rows.Add(i, dr["id"].ToString(), dr["category"].ToString());
            }
            dr.Close();
            cn.Close();

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            CategoryDetails categoryDetails = new CategoryDetails(this);
            categoryDetails.ShowDialog();
        }

        private void dgCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //For update and delete brand by cellclick from tbCategory
            string colName = dgCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure to delete this record?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbCategory WHERE id LIKE '" + dgCategory[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Category has been sucessful deleted.", "Point of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (colName == "Edit")
            {
                CategoryDetails categoryDetails = new CategoryDetails(this);
                categoryDetails.lblid.Text = dgCategory[1, e.RowIndex].Value.ToString();
                categoryDetails.txtcategory.Text = dgCategory[2, e.RowIndex].Value.ToString();
                categoryDetails.btnsave.Enabled = false;
                categoryDetails.btnupdate.Enabled = true;
                categoryDetails.ShowDialog();
            }
            LoadCategory();
        }
    }
}
