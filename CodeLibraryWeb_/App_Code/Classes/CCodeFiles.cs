
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System;
public class CCodeFiles
{

    CMain ClsMain = new CMain(CMain.Codecon);

    public CCodeFiles()
    {

    }

    //Return Identity
    private string ExecuteNewCommand(byte[] FileData, string Name, int CodeID, long FileSize)
    {
        string stRet = string.Empty;

        if (ValidateNameNew(Name) > 0)
        {
            stRet = "File Already Exists To Another Code";
            return stRet;
        }
        ////if (ValidateEXFilesNameNew(Name) > 0)
        ////{
        ////    stRet = "Record Already Exists In External Files";
        ////    return stRet;
        ////}

        SqlCommand cmd = new SqlCommand("CodeFilesInsert", CMain.Codecon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = Name;
        cmd.Parameters.Add("@CodeFile", SqlDbType.Image).Value = FileData;
        cmd.Parameters.Add("@CodeID", SqlDbType.Int).Value = CodeID;
        cmd.Parameters.Add("@FileSize", SqlDbType.BigInt).Value = FileSize;
        cmd.Parameters.Add("@SaveDate", SqlDbType.DateTime).Value = DateTime.Now.Date;
        cmd.Parameters.Add("@modifyDate", SqlDbType.DateTime).Value = DateTime.Now.Date;
        cmd.ExecuteNonQuery();

        //Get return Value For Identity
        stRet = cmd.Parameters["RETURN_VALUE"].Value.ToString();

        //Clear Memory
        cmd.Parameters.Clear();
        cmd.Dispose();

        return stRet;
    }

    private int ValidateNameNew(string Name)
    {
        int iRet = 0;

        SqlCommand cmd = new SqlCommand("CodeFilesCheckNameNew", CMain.Codecon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = Name;
        cmd.ExecuteNonQuery();

        iRet = int.Parse(cmd.Parameters["RETURN_VALUE"].Value.ToString());

        //Clear Memory
        cmd.Parameters.Clear();
        cmd.Dispose();

        return iRet;
    }

    private void ExecuteDeleteCommand(int FileID)
    {
        
            SqlCommand cmd = new SqlCommand("CodeFilesDelete", CMain.Codecon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = FileID;
            cmd.ExecuteNonQuery();

            //Clear memory
            cmd.Parameters.Clear();
            cmd.Dispose();
    }

    public string ExecuteCommand(ExecuteType executetype, int ID, byte[] FileData, string Name, int CodeID, long FileSize)
    {
        string stRet = string.Empty;

        switch (executetype)
        {
            case ExecuteType.New:
                {
                    stRet = ExecuteNewCommand(FileData, Name, CodeID, FileSize);
                    break;
                }
            case ExecuteType.Edit:
                {
                   // iRet = ExecuteEditCommand(ID, filePath, Name, FileSize);
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
