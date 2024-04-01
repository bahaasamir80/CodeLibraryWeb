using System;
using System.Web.UI.HtmlControls;



public class HTMLTreeNode :HtmlGenericControl, IDisposable
{
    HTMLTreeParentNodeCollection m_Nodes = new HTMLTreeParentNodeCollection();

    public HTMLTreeNode()
    {
        this.TagName = "li";
    }

    public HTMLTreeParentNodeCollection ParentNodes
    {
        get { return m_Nodes; }
        set { m_Nodes = value; }
    }

    public void AddParentNodes()
    {
        int iCount = m_Nodes.Count - 1;
        for (int iLoop = 0; iLoop <= iCount; iLoop++)
        {
            this.Controls.Add(m_Nodes[iLoop]);
        }
    }

    public void AddLink(string Text, string ID, string NodeID, string OnClickMethodName)
    {
        HtmlAnchor ancLink = new HtmlAnchor();
        ancLink.ID = ID;
        ancLink.InnerText = Text;
        ancLink.Attributes.Add("OnClick", OnClickMethodName);
        this.ID = NodeID;
        this.Controls.Add(ancLink);
    }

    

    #region IDisposable Members

    void IDisposable.Dispose()
    {
        this.Dispose();
    }

    #endregion
}
