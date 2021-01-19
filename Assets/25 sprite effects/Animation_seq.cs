using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Animation_seq : MonoBehaviour
{
    public float fps = .5f;
    public Sprite[] frames;

    private int frameIndex;
    private Image rendererMy;

    void Start()
    {
        rendererMy = GetComponent<Image>();
        NextFrame();
        InvokeRepeating("NextFrame", 1 / fps, 1 / fps);
    }

    void NextFrame()
    {
        if (frameIndex != frames.Length - 1)
        {
            rendererMy.sprite = frames[frameIndex];
            frameIndex = (frameIndex + 0001) % frames.Length;



        }
        else
            Destroy(this.gameObject);


    }
}