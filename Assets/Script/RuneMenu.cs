using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class RuneMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    SoundScript sound;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.image.name == "Next")
        {
            this.image.sprite = Resources.Load<Sprite>("INFO_BUTTON_PUSH(RIGHT)") as Sprite;
        
        }
            else if (this.image.name == "Rune_Socket")
            {
                GameObject.Find("RuneMenu").GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("RUNE_SOCKET_PLUS") as Sprite;
            }
            else if (this.image.name == "Rune_PLUS")
                {
                    GameObject.Find("RuneMenu").GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("RUNE_PLUS") as Sprite;
                }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.image.name == "Next")
        {
            this.image.sprite = Resources.Load<Sprite>("INFO_BUTTON(RIGHT)") as Sprite;

        }
    }
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        sound = GetComponent<SoundScript>();
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.image.name == "Next")
        {
            sound.Play();
            if (GameObject.Find("RuneMenu").GetComponent<Image>().sprite.name == "RUNE_SOCKET_PLUS")
            {
                GameObject.Find("RNCanvas").GetComponent<Canvas>().enabled = true;
            }
            else if (GameObject.Find("RuneMenu").GetComponent<Image>().sprite.name == "RUNE_PLUS")
            {
                GameObject.Find("RPCanvas").GetComponent<Canvas>().enabled = true;
            }
        }
    }
}
