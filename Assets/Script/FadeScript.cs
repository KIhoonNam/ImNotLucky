using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    public void ClearFade()
    {
        StopAllCoroutines();
        StartCoroutine(Clear());
    }
    IEnumerator FadeFlow()
    {
        Panel.enabled = true;
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;
       
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;

            yield return null;

        }
     
        yield return null; 
 
    }

    IEnumerator Clear()
    {
        
        time = 0f;
        Color alpha = Panel.color;
        while (alpha.a >0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0,time * 2);
            Panel.color = alpha;
            yield return null;
        }

        Panel.gameObject.SetActive(false);
        Panel.enabled = false;
        yield return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
