using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoApp
{
    public partial class Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connString;
            SqlConnection conn;

            connString = "@Data Source=WIN-50GP30FGO75;Initial Catalog=Demodb ;User ID=sa;Password=demol23";

            conn = new SqlConnection(connString);

            conn.Open();

            Response.Write("Connection Made");
            conn.Close();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Response.Write(txtName.Text + "</br>");
            Session["Name"] = txtName.Text;//Alt

            //Response.Write(lstLocation.SelectedItem.Text + "</br>");
            Response.Write(Session["Name"]);//Alt

            lblName.Visible = false;
            txtName.Visible = false;
            lstLocation.Visible = false;
            chkC.Visible = false;
            chkASP.Visible = false;
            radMale.Visible = false;
            radFemale.Visible = false;
            btnSubmit.Visible = false;
        }
    }
}