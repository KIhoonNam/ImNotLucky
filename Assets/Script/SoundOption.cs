using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    public static event SoundHandler Sound;
    private float Vol;

    public Slider backVol;
    // Start is called before the first frame update
    void Start()
    {
        Vol = PlayerPrefs.GetFloat("Vol", 1f);
        backVol.value = Vol;
        
    }

    // Update is called once per frame
    void Update()
    {
        Slider();
        
    }

    public void Slider()
    {
        Sound();
        Vol = backVol.value;
        PlayerPrefs.SetFloat("Vol", Vol);
    }
}
