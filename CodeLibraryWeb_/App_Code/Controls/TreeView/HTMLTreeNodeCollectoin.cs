
using System.Collections.Generic;
using System;
using System.Collections;
using System.Web.UI.HtmlControls;


public class HTMLTreeNodeCollectoin
{ 
   //private HtmlAnchor m_ancLink = new HtmlAnchor();
   
    public HTMLTreeNodeCollectoin()
    {

    }
   
    private List<HTMLTreeNode> m_Nodes = new List<HTMLTreeNode>();

    #region IList Members

    public void Add(HTMLTreeNode Node)
    {
        m_Nodes.Add(Node);
        
    }

    public void Add(string Text, string ID,string NodeID ,string OnClickMethodName)
    {
        HTMLTreeNode node = new HTMLTreeNode();
        node.AddLink(Text, ID, NodeID, OnClickMethodName);
        m_Nodes.Add(node);
    }

    public void Clear()
    {
        m_Nodes.Clear();
    }

    public bool Contains(HTMLTreeNode Node)
    {
        return m_Nodes.Contains(Node);
    }

    public int IndexOf(HTMLTreeNode Node)
    {
        int iCount = m_Nodes.Count - 1;
        for (int iLoop = 0; iLoop < iCount; iLoop++)
        {
            if (this[iLoop] == Node)
            {
                return iLoop;
            }
        }
        return -1;

    }

    public void Insert(int index, HTMLTreeNode Node)
    {
        m_Nodes.Insert(index, Node);
    }

    public void Remove(HTMLTreeNode Node)
    {
        m_Nodes.Remove(Node);
    }

    public void Remove(string NodeID)
    {
        HTMLTreeNode Node = FindNode(NodeID);
        if (Node != null)
        {
            m_Nodes.Remove(Node);
        }
    }

    public void RemoveAt(int index)
    {
        m_Nodes.RemoveAt(index);
    }

    public HTMLTreeNode this[int index]
    {
        get
        {

            return m_Nodes[index];
        }
        set
        {
            m_Nodes[index] = value;
        }
    }

    #endregion

    #region ICollection Members

    public void CopyTo(Array array, int index)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public int Count
    {
        get { return m_Nodes.Count; }
    }

    public bool IsSynchronized
    {
        get { return false; }
    }

    public object SyncRoot
    {
        get { return m_Nodes; }
    }

    #endregion

    #region IEnumerable Members

    public IEnumerator GetEnumerator()
    {
        int iCount = m_Nodes.Count - 1;
        for (int iLoop = 0; iLoop < iCount; iLoop++)
        {
            yield return m_Nodes[iLoop];
        }
    }

    #endregion

    private HTMLTreeNode FindNode(string NodeID)
    {
        foreach (HTMLTreeNode node in m_Nodes)
        {
            if (node.ID == NodeID)
            {
                return node;
            }
        }
        return null;
    }
}
