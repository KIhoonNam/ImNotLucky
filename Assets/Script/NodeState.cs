using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeState : MonoBehaviour
{
    public BoxCollider[] chillObjects;
    public List<BoxCollider> chillNodeList = new List<BoxCollider>();



    public void EnableTrue()
    {
        for (int i = 0; i < chillNodeList.Count; i++)
        {
            chillObjects[i].enabled = true;
        }
    }

    private void Start()
    {

        FillNodes();
        for(int i = 0; i < chillNodeList.Count; i++)
        {
            chillObjects[i].enabled = false;
        }
    }

    void FillNodes()
    {

        chillNodeList.Clear();

        chillObjects = GetComponentsInChildren<BoxCollider>();

        foreach (BoxCollider child in chillObjects)
        {
            if (child != this.transform)
            {
                chillNodeList.Add(child);
            }
        }
    }

}
