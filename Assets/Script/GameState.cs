using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public Image[] Panel;
    float time = 0f;
    float F_time = 1f;
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
       
        StartCoroutine(GameOver());

        SaveManager.Delete(" Item");
        SaveManager.Delete(" Manager");

        SaveManager.Delete(" Player");
        Time.timeScale = 1;
    }

    public static void Over()
    {
        SceneManager.LoadScene("GameOver");
    }

  public void Exit()
    {
      
        Application.Quit();
    }
    public void Restrt()
    {


        SceneManager.LoadScene("MainScene");
    }
    IEnumerator GameClear()
    {
        yield return null;
    }
    // Update is called once per frame
    IEnumerator GameOver()
    {
       
        time = 0f;
        Color alpha = Panel[0].color;

        while (alpha.a < 1f)
        {

            alpha.a += 0.1f;
            Panel[0].color = alpha;

            yield return new WaitForSeconds(0.1f);

        }
        Panel[1].enabled = true;
        Panel[1].gameObject.SetActive(true);
        Panel[2].enabled = true;
        Panel[2].gameObject.SetActive(true);
    }
}
