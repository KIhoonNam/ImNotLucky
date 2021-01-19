using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfomaitionScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public string Name;
    Image Info;
    bool Stop = false;
    SpriteRenderer StInfo;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        if(this.tag == "Rune" || this.tag == "Dice" || this.tag == "Skill")
        Info = transform.GetChild(1).GetComponent<Image>();

        if(this.tag== "Stage")
        StInfo = transform.GetChild(1).GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        
         RayCast_HIT();
    }


    void RayCast_HIT()
    {
        var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hitInfo))
        {
            
            if (hitInfo.transform.gameObject.tag == "Stage")
            {
                if (!Stop)
                {
                    if (this.name == hitInfo.transform.gameObject.name)
                    {
                        Debug.Log(hitInfo.transform.gameObject.name);
                        Name = hitInfo.transform.gameObject.name;


                        this.StInfo.sprite = Resources.Load<Sprite>("StageInfo/" + Name) as Sprite;

                        this.StInfo.enabled = true;
                        Stop = true;
                    }
                }
                
            }
            else if(hitInfo.transform.gameObject.tag != "Stage")
            {
                if (StInfo != null)
                {
                    StInfo.enabled = false;
                    Stop = false;
                    
                }
            }
        }
    }
    public void Up()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (this.tag == "Rune")
        {
            Name = this.name;

            Info.sprite = Resources.Load<Sprite>("RuneInfo/" + Name) as Sprite;

            Info.enabled = true;
        }
        if(this.tag == "Dice")
        {
            Name = this.name;

            Info.sprite = Resources.Load<Sprite>("DiceInfo/" + Name) as Sprite;

            Info.enabled = true;
        }

        if(this.tag == "Skill")
        {
            Name = this.name;

            Info.sprite = Resources.Load<Sprite>("SkillInfo/" + Name + "_INFO") as Sprite;

            Info.enabled = true;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.tag == "Rune" || this.tag == "Dice" || this.tag == "Skill")
            Info.enabled = false;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Info.enabled = false;
    }
}
