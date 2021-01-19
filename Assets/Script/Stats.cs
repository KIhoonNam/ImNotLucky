using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{

    private Image content;
    string val;
   

    private float currentFill;
    [SerializeField]
    public Text statValue =null;
    [SerializeField]
    public Text GoldValue = null;


    private float Gold;

    public float MaxFill { get; set; }
    private float CurrentValue;
    public float MyCurrentValue
    {
        get
        {
            return CurrentValue;
        }
        set
        {
            if(value > MaxFill)
            {
                CurrentValue = MaxFill;
            }
            else if (value <0)
            {
                CurrentValue = 0;
            }
            else
                CurrentValue = value;

            

            currentFill = CurrentValue / MaxFill;

            val = CurrentValue + "/" + MaxFill;
            

            if(GoldValue != null)
                GoldValue.text = Gold + "";
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        MaxFill = 100;
        content = GetComponent<Image>();

        if(statValue == null)
        {
            this.gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
            content.fillAmount = currentFill;
        if(statValue!= null)
            statValue.text = val;
    }

    public void Initialize(float currentValue, float MaxValue,float GoldValue)
    {
        MaxFill = MaxValue;
        Gold = GoldValue;
        MyCurrentValue = currentValue;

       
    }
    
}
