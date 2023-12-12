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
    public partial class BrandDetails : Form

    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        Brand brand;
        public BrandDetails(Brand br)
        {
            InitializeComponent();
            cn = new SqlConnection(Properties.Settings.Default.Connection);
            brand = br;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try 
            { 
                if(MessageBox.Show("Are you sure to save this brand?","POS",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String str = "INSERT INTO tbBrand(brand) VALUES(@brand)";
                    cm = new SqlCommand(str, cn);
                    cm.Parameters.AddWithValue("@brand", txtbrand.Text);
                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Record has been successful saved", "POS");
                    Clear();
                    brand.LoadBrand();
                    this.Dispose();
                    
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtbrand.Clear();
            btnupdate.Enabled = false;
            btnsave.Enabled = true;
            txtbrand.Focus();
        }

        //Update Brand name
        private void btnupdate_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to update this brand?","Update Record!", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //string str = "UPDATE tbBrand SET brand=@brand WHERE id LIKE'" + lblid.Text + "'";
                cn.Open();
                cm = new SqlCommand("UPDATE tbBrand SET brand = @brand WHERE id LIKE '"+ lblid.Text+"'", cn);
                cm.Parameters.AddWithValue("@brand", txtbrand.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Successfuly Updated","POS");
                Clear();
                this.Dispose();
            }
        }
    }
}
