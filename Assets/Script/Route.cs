using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{

    Transform[] chillObjects;
    public List<Transform> chillNodeList = new List<Transform>();
  


   
    private void OnEnable()
    {
        
        FillNodes();
    }

    void FillNodes()
    {

        chillNodeList.Clear();

        chillObjects = GetComponentsInChildren<Transform>();

        foreach (Transform child in chillObjects)
        {
            if (child != this.transform)
            {
                chillNodeList.Add(child);
            }
        }
    }

  
}
