using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static Resolution baseResolution;
    public static Animator fadeOut;

    private GameObject populationCircle;
    private GameObject bossCircle;

    public float populationPercentage;
    public float bossLifePercentage;

    // Start is called before the first frame update
    void Start()
    {
        baseResolution = Screen.currentResolution;
        fadeOut = GameObject.Find("FadeIn").GetComponent<Animator>();

        populationCircle = GameObject.Find("PopulationCircle");
        bossCircle = GameObject.Find("BossCircle");

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "TommyTestZone") { 
            FillBossCircle(bossLifePercentage);
            FillPopulationCircle(populationPercentage);
        }
    }

    public GameObject getInstance() {
        return gameObject;
    }

    public static void ToggleFullscreen(bool full) {

        if (full) {
            Screen.SetResolution(baseResolution.width, baseResolution.height, true, baseResolution.refreshRate);
        } else {
            Screen.SetResolution(Mathf.RoundToInt(baseResolution.width / 1.2f), Mathf.RoundToInt(baseResolution.height / 1.2f), false, baseResolution.refreshRate);
        }

    }


    /**
     Method that fill the circle of the UI for the boss
     @param percent the float (ex : 100)
     **/
    public void FillBossCircle(float percent) {
        bossCircle.GetComponent<Image>().fillAmount = Mathf.Lerp(bossCircle.GetComponent<Image>().fillAmount, percent / 100, 0.3f);

    }

    /**
     Method that fill the circle of the UI for the boss
     @param percent the float (ex : 100)
     **/
    public void FillPopulationCircle(float percent)
    {
        populationCircle.GetComponent<Image>().fillAmount = Mathf.Lerp(populationCircle.GetComponent<Image>().fillAmount, percent / 100, 0.3f);
    }

    public void EndGame()
    {
        fadeOut.Play("FadeOut");
        Invoke(nameof(GoEndGame), 1f);
    }

    private void GoEndGame()
    {
        SceneManager.LoadScene("End Screen");
    }

    public void LoadMenu()
    {
        fadeOut.Play("FadeOut");
        Invoke(nameof(GoMenu), 1f);
    }

    private void GoMenu()
    {
        SceneManager.LoadScene("Title Screen");
    }

}
