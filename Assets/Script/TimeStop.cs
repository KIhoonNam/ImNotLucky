using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    float timeScale;
    bool stop;
    // Start is called before the first frame update

    private void Start()
    {
        stop = false;
    }

    public void TimeScale()
    {
        if (Time.timeScale >= 1f)
        {
            Time.timeScale = 0f;
          
        }
        else if(Time.timeScale < 1f)
        {
           
            Time.timeScale += timeScale;
        }

        
    }
    void Update()
    {
        if(stop)
        {
            if(Time.timeScale < 1f)
            {
                Time.timeScale += Time.deltaTime * timeScale;
            }
            else
            {
                Time.timeScale = 1f;
                stop = false;
            }
        }
        
    }

    public void StopTime(float changetime,int RestorSpeed,float Delay)
    {
        timeScale = RestorSpeed;

        if(Delay>0)
        {
            StopCoroutine(StartAgain(Delay));
            StartCoroutine(StartAgain(Delay));
        }
        else
        {
            stop = true;
        }
        Time.timeScale = changetime;
    }
    IEnumerator StartAgain(float amt)
    {
        stop = true;
        yield return new WaitForSeconds(amt);
    }
}
