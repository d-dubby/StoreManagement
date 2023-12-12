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
    public partial class Form1 : Form

    {
  

        public Form1()
        {
            InitializeComponent();
            CustomizeDesign();
        }

        #region panel slide

        private void CustomizeDesign()
        {
            panelsubproduct.Visible = false;
            panelsubrecord.Visible = false;
            panelinstock.Visible = false;
            panelsetting.Visible = false;
        }

        private void hideSubmenu()
        {
            if(panelsubproduct.Visible == true) 
            {
                panelsubproduct.Visible = true;
            }
            if(panelsubrecord.Visible == true)
            {
                panelsubrecord.Visible = false;
            }
            if(panelinstock.Visible == true)
            {
                panelinstock.Visible = false;
            }
            if(panelsetting.Visible == true)
            {
                panelsetting.Visible = false;
            }
        }

        private void showSubmenu(Panel submenu) 
        {
            if(submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }

        #endregion panel slide

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            lbltitle.Text = childForm.Text;
            panelmain.Controls.Add(childForm);
            panelmain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btndashboard_Click(object sender, EventArgs e)
        {

        }

        private void btnproduct_Click(object sender, EventArgs e)
        {
            showSubmenu(panelsubproduct);
        }

        private void btnproductlist_Click(object sender, EventArgs e)
        {
            openChildForm(new Product());
            hideSubmenu();
        }

        private void btncategory_Click(object sender, EventArgs e)
        {
            openChildForm(new Category());
            hideSubmenu();
        }

        private void btnbrand_Click(object sender, EventArgs e)
        {
            openChildForm(new Brand());
            hideSubmenu();
        }

        private void btbstock_Click(object sender, EventArgs e)
        {
            showSubmenu(panelinstock);
        }

        private void btnstockentity_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnstockadjust_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnsupplier_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnrecord_Click(object sender, EventArgs e)
        {
            showSubmenu(panelsubrecord);
        }

        private void btnsalehist_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnposrecord_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnsetting_Click(object sender, EventArgs e)
        {
            showSubmenu(panelsetting);
        }

        private void btnuser_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnstore_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }
    }
}
