
using System.Collections.Generic;
using System;
using System.Collections;


public class HTMLTreeParentNodeCollection
{
    public HTMLTreeParentNodeCollection()
    {

    }

    private List<HTMLTreeParentNode> m_ParentNodes = new List<HTMLTreeParentNode>();

    #region IList Members

    public void Add(HTMLTreeParentNode Node)
    {
        m_ParentNodes.Add(Node);
    }
    public void Add()
    {
        HTMLTreeParentNode ParentNode = new HTMLTreeParentNode();
        m_ParentNodes.Add(ParentNode);


    }




    public void Clear()
    {
        m_ParentNodes.Clear();
    }

    public bool Contains(HTMLTreeParentNode Node)
    {
        return m_ParentNodes.Contains(Node);
    }

    public int IndexOf(HTMLTreeParentNode Node)
    {
        int iCount = m_ParentNodes.Count - 1;
        for (int iLoop = 0; iLoop < iCount; iLoop++)
        {
            if (this[iLoop] == Node)
            {
                return iLoop;
            }
        }
        return -1;

    }

    public void Insert(int index, HTMLTreeParentNode Node)
    {
        m_ParentNodes.Insert(index, Node);
    }

    public void Remove(HTMLTreeParentNode Node)
    {
        m_ParentNodes.Remove(Node);
    }

    public void Remove(string NodeID)
    {
        HTMLTreeParentNode Node = FindNode(NodeID);
        if (Node != null)
        {
            m_ParentNodes.Remove(Node);
        }
    }

    public void RemoveAt(int index)
    {
        m_ParentNodes.RemoveAt(index);
    }

    public HTMLTreeParentNode this[int index]
    {
        get
        {

            return m_ParentNodes[index];
        }
        set
        {
            m_ParentNodes[index] = value;
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
        get { return m_ParentNodes.Count; }
    }

    public bool IsSynchronized
    {
        get { return false; }
    }

    public object SyncRoot
    {
        get { return m_ParentNodes; }
    }

    #endregion

    #region IEnumerable Members

    public IEnumerator GetEnumerator()
    {
        int iCount = m_ParentNodes.Count - 1;
        for (int iLoop = 0; iLoop < iCount; iLoop++)
        {
            yield return m_ParentNodes[iLoop];
        }
    }

    #endregion

    private HTMLTreeParentNode FindNode(string NodeID)
    {
        foreach (HTMLTreeParentNode node in m_ParentNodes)
        {
            if (node.ID == NodeID)
            {
                return node;
            }
        }
        return null;
    }
}
