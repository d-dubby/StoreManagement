using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystem
{
    internal class DBconnection
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();

        public DataTable getTable(String query)
        {
            cn = new SqlConnection(Properties.Settings.Default.Connection);
            cm = new SqlCommand(query, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;

        }
    }
}
