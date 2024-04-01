
using System.Web.UI.HtmlControls;

public class HTMLTreeParentNode : HtmlGenericControl
{
    HTMLTreeNodeCollectoin m_Nodes = new HTMLTreeNodeCollectoin();

    public HTMLTreeParentNode()
    {
        this.TagName = "ul";
    }

    public HTMLTreeNodeCollectoin Nodes
    {
        get { return m_Nodes; }
        set { m_Nodes = value; }
    }

    public void AddNodes()
    {
        int iCount = m_Nodes.Count - 1;
        for (int iLoop = 0; iLoop <= iCount; iLoop++)
        {
            this.Controls.Add(m_Nodes[iLoop]);
        }
    }



}
