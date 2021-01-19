using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text Panel;
    float time = 0f;
    float F_time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Clear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Clear()
    {
        this.transform.position = new Vector3(this.transform.position.x , this.transform.position.y + 1, this.transform.position.z);
        time = 0f;
        Color alpha = Panel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time*0.5f);
            Panel.color = alpha;
            
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + Time.deltaTime, this.transform.position.z);
            yield return null;
        }

        Destroy(this.gameObject);
        yield return null;
    }
}
