

using System.Web;
using System;
using System.IO;
public class TFiles : IDisposable
{
    public TFiles()
    {

    }


    public byte[] UploadFile(HttpPostedFile postFile, string FilePath)
    {


        int nFileLen = postFile.ContentLength;
        byte[] FileData = new byte[nFileLen];
        postFile.InputStream.Read(FileData, 0, nFileLen);

        //write File To Desk
        File.WriteAllBytes(FilePath, FileData);

        return FileData;
    }

    public string GetFileSize(long FileSize)
    {
        string stRet = string.Empty;

        //Get File Size in (Bytes , KB , MB)
        if (FileSize < 1024)
        {
            stRet = Math.Round(double.Parse(FileSize.ToString()), 2) + " (Bytes)";
        }
        else
        {

            if ((FileSize / 1024) > 1024)
            {
                stRet = Math.Round(((double.Parse(FileSize.ToString()) / 1024) / 1024), 2) + " (MB)";
            }
            else
            {
                stRet = Math.Round((double.Parse(FileSize.ToString()) / 1024), 2) + " (KB)";
            }
        }

        return stRet;
    }

    //private string RenderControl(Control ctrl)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    StringWriter tw = new StringWriter(sb);
    //    HtmlTextWriter hw = new HtmlTextWriter(tw);

    //    ctrl.RenderControl(hw);
    //    return sb.ToString();
    //}

    #region IDisposable Members

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    #endregion
}
