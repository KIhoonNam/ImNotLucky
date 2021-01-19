using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonScript : MonoBehaviour
{

    Image[] Main;
    
    public List<Image> MainList = new List<Image>();
    // Start is called before the first frame update
    void Awake()
    {
        FillNodes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillNodes()
    {

        MainList.Clear();

        Main = GetComponentsInChildren<Image>();

        foreach (Image child in Main)
        {
            if (child != this.transform)
            {
                MainList.Add(child);
            }
        }
    }

}
