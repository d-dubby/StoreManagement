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
    public partial class CategoryDetails : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        Category category;
        public CategoryDetails(Category ct)
        {
            InitializeComponent();
            cn = new SqlConnection(Properties.Settings.Default.Connection);
            category = ct;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void clear()
        {
            txtcategory.Clear();
            txtcategory.Focus();
            btnsave.Enabled = true;
            btnupdate.Enabled = false;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to save this Category?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String str = "INSERT INTO tbCategory(category) VALUES(@category)";
                    cm = new SqlCommand(str, cn);
                    cm.Parameters.AddWithValue("@category", txtcategory.Text);
                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Record has been successful saved", "Point of Sales");
                    clear();


                }
                category.LoadCategory();

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

        //Update Category 
        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to update this Category?", "Update Record!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Open();
                cm = new SqlCommand("UPDATE tbCategory SET category = @category WHERE id LIKE '" + lblid.Text + "'", cn);
                cm.Parameters.AddWithValue("@category", txtcategory.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Successfuly Updated", "Point of Sales");
                clear();
                this.Dispose();
            }
        }

        
    }
}
