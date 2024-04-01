
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System;


public class CMain
{
    public static SqlConnection Codecon = new SqlConnection(@"Data Source=Server\SERVER2017;Initial Catalog=CodeDB;Persist Security Info=True;User ID=sa;Password=AbAppsDataBasePassWord@123");

    public static DataTable g_NewCodeFiles = new DataTable();

    SqlConnection m_cn;
    public CMain(SqlConnection cn)
    {
        m_cn = cn;

       
    }

    public void FillList(string ProcName, HtmlSelect lst)
    {

        SqlCommand cmd = new SqlCommand(ProcName, m_cn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(dr);
        lst.DataTextField = "Name";
        lst.DataValueField = "ID";
        lst.DataSource = dt;
        lst.DataBind();

        dr.Close();
        dr.Dispose();
        cmd.Dispose();

    }

    public void FillList(string ProcName, ListControl lst)
    {

        SqlCommand cmd = new SqlCommand(ProcName, m_cn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(dr);
        lst.DataTextField = "Name";
        lst.DataValueField = "ID";
        lst.DataSource = dt;
        lst.DataBind();

        dr.Close();
        dr.Dispose();
        cmd.Dispose();

    }

    //Fill Any List
    public void FillList(string ProcName, HtmlSelect lst, params SqlParameter[] sqlP)
    {
        DataTable dt = SetDataCommand(ProcName, sqlP);

        lst.DataTextField = "Name";
        lst.DataValueField = "ID";
        lst.DataSource = dt;
        lst.DataBind();
        if (dt.Rows.Count > 0)
        {
            lst.SelectedIndex = 0;
        }
    }

    public int GetCount(string ProcName, params SqlParameter[] sqlp)
    {
        return SetIntCommand(ProcName, sqlp);
    }

    public byte DeleteEditPassWord()
    {
        //frmInputBox frm = new frmInputBox("Enter PassWord", "Validation", true);
        //frm.ShowDialog();
        //string stPassWord = frm.InputResult;
        //if (String.IsNullOrEmpty(stPassWord) == true) { return 0; }
        //if (stPassWord != "1234")
        //{
        //    MessageBox.Show("Wrong PassWord", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    return 0;
        //}
        //frm.Dispose();
        //frm = null;

        return 1;
    }

    //Fill DataTable With Command Parameter
    public DataTable GetData(string ProcName, params SqlParameter[] sqlp)
    {
        DataTable dt = new DataTable();

        SqlCommand cmd = new SqlCommand(ProcName, m_cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddRange(sqlp);
        SqlDataReader dr = cmd.ExecuteReader();
        cmd.Parameters.Clear();
        cmd.Dispose();
        dt.Load(dr);
        dr.Close();
        dr.Dispose();

        cmd.Parameters.Clear();
        cmd.Dispose();

        return dt;
    }

    private DataTable SetDataCommand(string ProcName, params SqlParameter[] sqlP)
    {
        SqlCommand cmd = new SqlCommand(ProcName, m_cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddRange(sqlP);
        SqlDataReader dr = cmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(dr);
        dr.Close();
        dr.Dispose();
        cmd.Parameters.Clear();
        cmd.Dispose();

        return dt;
    }

    private int SetIntCommand(string ProcName, SqlParameter[] sqlP)
    {
        SqlCommand cmd = new SqlCommand(ProcName, m_cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.AddRange(sqlP);
        cmd.ExecuteNonQuery();
        //Get return Value For Identity
        int iRet = int.Parse(cmd.Parameters["RETURN_VALUE"].Value.ToString());
        cmd.Parameters.Clear();
        cmd.Dispose();

        return iRet;
    }
}


