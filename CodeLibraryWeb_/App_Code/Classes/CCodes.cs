

using System.Data;
using System.Data.SqlClient;
using System;
using System.Text;
public class CCodes
{
    //Save Data In DataBase With Parameter
    private string ExecuteEditCommand(int ID, string Name, string CodeText, string CodeEX, byte CategoryID, short ProgLangID)
    {
        string stRet = string.Empty;

        if (int.Parse(ValidateNameEdit(ID, Name, ProgLangID)) > 0)
        {
            stRet = "This Name Has Been Sets To Another Record";
            return stRet;
        }
        SqlCommand cmd = new SqlCommand("CodesUpdate", CMain.Codecon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = Name;
        cmd.Parameters.Add("@CodeText", SqlDbType.NText).Value = CodeText;
        cmd.Parameters.Add("@CodeEX", SqlDbType.NText).Value = CodeEX;
        cmd.Parameters.Add("@CategoryID", SqlDbType.TinyInt).Value = CategoryID;
        cmd.Parameters.Add("@ProgLangID", SqlDbType.SmallInt).Value = ProgLangID;
        cmd.ExecuteNonQuery();

        //Clear Memory
        cmd.Parameters.Clear();
        cmd.Dispose();
        stRet = "0";
        return stRet;
    }

    //Return Identity
    private string ExecuteNewCommand(string Name, string CodeText, string CodeEX, byte CategoryID, short ProgLangID)
    {
        string stRet = string.Empty;
       
            if (ValidateNameNew(Name, ProgLangID) > 0)
            {
                stRet = "Record Already Exists";
                return  stRet ;
            }

            SqlCommand cmd = new SqlCommand("CodesInsert", CMain.Codecon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = Name;
            cmd.Parameters.Add("@CodeText", SqlDbType.NText).Value = CodeText;
            cmd.Parameters.Add("@CodeEX", SqlDbType.NText).Value = CodeEX;
            cmd.Parameters.Add("@CategoryID", SqlDbType.TinyInt).Value = CategoryID;
            cmd.Parameters.Add("@ProgLangID", SqlDbType.SmallInt).Value = ProgLangID;
            cmd.ExecuteNonQuery();

            //Get return Value For Identity
            stRet = cmd.Parameters["RETURN_VALUE"].Value.ToString();

            //Clear Memory
            cmd.Parameters.Clear();
            cmd.Dispose();

        return stRet;
    }

    private int ValidateNameNew(string Name, short ProgLangID)
    {
        int iRet = 0;
        
            SqlCommand cmd = new SqlCommand("CodesCheckNameNew", CMain.Codecon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = Name;
            cmd.Parameters.Add("@ProgLangID", SqlDbType.SmallInt).Value = ProgLangID;
            cmd.ExecuteNonQuery();

            iRet = int.Parse(cmd.Parameters["RETURN_VALUE"].Value.ToString());

            //Clear Memory
            cmd.Parameters.Clear();
            cmd.Dispose();
       
        return iRet;
    }

    private string ValidateNameEdit(int ID, string Name, short ProgLangID)
    {
        string stRet = string.Empty ;
       
            SqlCommand cmd = new SqlCommand("CodesCheckNameEdit", CMain.Codecon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = Name;
            cmd.Parameters.Add("@ProgLangID", SqlDbType.SmallInt).Value = ProgLangID;
            cmd.ExecuteNonQuery();

            stRet = cmd.Parameters["RETURN_VALUE"].Value.ToString();

            //Clear Memory
            cmd.Parameters.Clear();
            cmd.Dispose();


       
        return stRet;
    }

    private void ExecuteDeleteCommand(int CodeID)
    {
        string stRet = string.Empty;
       
            StringBuilder stbMessege = new StringBuilder();
            DataTable dt = new DataTable();
            dt = ValidDelete(CodeID);
            if (dt.Rows.Count > 0)
            {
                int iRowCount = dt.Rows.Count - 1;
                stbMessege.Append("There Are Files Belong To Code :");
                stbMessege.AppendLine();

                for (int iLoop = 0; iLoop <= iRowCount; iLoop++)
                {
                    stbMessege.Append((iLoop + 1) + " : ");
                    stbMessege.Append(dt.Rows[iLoop][0].ToString());
                    stbMessege.AppendLine();
                }
                stbMessege.Append("Files Will Be Deleted !!");
                stbMessege.AppendLine();
                stbMessege.Append("Are You Sure You Want To Delete Code ?");

                //if (System.Windows.Forms.MessageBox.Show(stbMessege.ToString(), "Validate Code's Files", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                //{
                //    dt.Dispose();
                //    stbMessege = null;
                //    return;
                //}
            }
            SqlCommand cmd = new SqlCommand("CodesDelete", CMain.Codecon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = CodeID;
            cmd.ExecuteNonQuery();

            stRet = "Code Deleted";
            //Clear memory
            cmd.Parameters.Clear();
            cmd.Dispose();
            dt.Dispose();
            stbMessege = null;
        

    }

    private DataTable ValidDelete(int CodeID)
    {
        string stRet = string.Empty;
        DataTable dt = new DataTable();
        

            SqlCommand cmd = new SqlCommand("CodesValidateDelete", CMain.Codecon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CodeID", SqlDbType.Int).Value = CodeID;
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);

            //Clear Memory
            dr.Close();
            cmd.Parameters.Clear();
            dr.Dispose();
            cmd.Dispose();


      
        return dt;
    }

    public string ExecuteCommand(ExecuteType executetype, int ID, string Name, string CodeText, string CodeEX, byte CategoryID, short ProgLangID)
    {
        string stRet = string.Empty;

        switch (executetype)
        {
            case ExecuteType.New:
                {
                    stRet = ExecuteNewCommand(Name, CodeText, CodeEX, CategoryID, ProgLangID);
                    break;
                }
            case ExecuteType.Edit:
                {
                    stRet = ExecuteEditCommand(ID, Name, CodeText, CodeEX, CategoryID, ProgLangID);
                    break;
                }
            case ExecuteType.Delete:
                {
                    ExecuteDeleteCommand(ID);
                    break;
                }
        }
        return stRet;
    }

}
