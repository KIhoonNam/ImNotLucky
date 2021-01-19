using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneControl : MonoBehaviour
{
    GameManager gg;
    static string netScene;
    
    [SerializeField]
    Slider Bar;
    // Start is called before the first frame update
    public static void LoadingScene(string name)
    {
        netScene = name;
        SceneManager.LoadScene("LoadingScene");
    }


    private void Start()
    {
        gg = FindObjectOfType<GameManager>();
        StartCoroutine(LoadSc());

    }
    // Update is called once per frame
    public IEnumerator LoadSc()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(netScene);

        operation.allowSceneActivation = false;
        gg.Fade.ClearFade();
        float timer = 0f;
        while (!operation.isDone)
        {
            


            if (operation.progress < 0.9f)
            {
                Bar.value = operation.progress;
              
               
            }
            else
            {
                yield return null;
                timer += Time.deltaTime;
                Bar.value = Mathf.Lerp(0.9f, 1f, timer);
                if(Bar.value >=1f)
                {
                   
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
