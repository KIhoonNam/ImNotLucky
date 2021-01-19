using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    [HideInInspector]
    new AudioSource audio;
    float vol;

    private void OnEnable()
    {
        audio = GetComponent<AudioSource>();

        vol = PlayerPrefs.GetFloat("Vol");
        audio.volume = vol;

        SoundOption.Sound += SetUp;
        
    }

    void SetUp()
    {
        if (audio != null)
        {
            vol = PlayerPrefs.GetFloat("Vol");
            audio.volume = vol;
        }
    }

    public void Play()
    {
        audio.Play();
    }

    public void Stop()
    {
        audio.Stop();
    }
    public void Puase()
    {
        audio.Pause();
    }

    public void UI_OPEN()
    {
        audio.clip = Resources.Load<AudioClip>("Sound/UI_OPEN") as AudioClip;
    }
    public void Button()
    {
        audio.clip = Resources.Load<AudioClip>("Sound/BUTTON") as AudioClip;
    }

    public void HEAL()
    {
        audio.clip = Resources.Load<AudioClip>("Sound/SHOP_HEAL") as AudioClip;
    }

    public void NOTG()
    {
        audio.clip = Resources.Load<AudioClip>("Sound/NOT_ENOUGHT_MONEY") as AudioClip;
    }
    public void RuneSocket()
    {
        audio.clip = Resources.Load<AudioClip>("Sound/RUNE_SOCKET_OPEN") as AudioClip;
    }

    public void TRADE()
    {
        audio.clip = Resources.Load<AudioClip>("Sound/TRADE") as AudioClip;
    }
    public void WIn()
    {
        audio.clip = Resources.Load<AudioClip>("Sound/WIN") as AudioClip;
    }
    public void BATTLE_CLEAR()
    {
        audio.clip = Resources.Load<AudioClip>("Sound/BATTLE_SCENE_CLEAR") as AudioClip;
    }

    public void SkeletonVoice(string name)
    {
        audio.clip = Resources.Load<AudioClip>("Sound/Monster/SKELETON/" + name) as AudioClip;
    }

    public void PlayerVoice(string name)
    {

        audio.clip = Resources.Load<AudioClip>("Sound/CHARACTER/" +name) as AudioClip;
    }

    public void RhinoVoice(string name)
    {
        audio.clip = Resources.Load<AudioClip>("Sound/Monster/RIHNO/" + name) as AudioClip;
    }
    public void MushVoice(string name)
    {
        audio.clip = Resources.Load<AudioClip>("Sound/Monster/MUSHEROOM/" + name) as AudioClip;
    }

    public void GolemVoice(string name)
    {
        audio.clip = Resources.Load<AudioClip>("Sound/Monster/GOLEM/" + name) as AudioClip;
    }
    public void SpiderVoice(string name)
    {
        audio.clip = Resources.Load<AudioClip>("Sound/Monster/SPIDER/" + name) as AudioClip;
    }
    public void GoblinVoice(string name)
    {
        audio.clip = Resources.Load<AudioClip>("Sound/Monster/REGION/" + name) as AudioClip;
    }
}
