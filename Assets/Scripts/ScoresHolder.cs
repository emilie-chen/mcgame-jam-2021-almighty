using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresHolder : MonoBehaviour
{

    public float populationPercentage;
    public AudioSource audioSourceGoodEnding;
    public AudioSource audioSourceBadEnding;

    public Sprite badEndScreenImage;
    public Sprite goodEndScreenImage;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "TommyTestZone") { 
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "TommyTestZone")
        {
            populationPercentage = GameObject.Find("Game UI").GetComponent<GameManager>().populationPercentage;
            audioSourceGoodEnding.volume = 0;
            audioSourceBadEnding.volume = 0;
        } else if (SceneManager.GetActiveScene().name == "End Screen")
        {
            GameObject.Find("PopulationCircle").GetComponent<Image>().fillAmount = Mathf.Lerp(GameObject.Find("PopulationCircle").GetComponent<Image>().fillAmount, populationPercentage / 100, 0.03f);

            if (populationPercentage > 20f)
            {

                GameObject.Find("FinalTitleText").GetComponent<Text>().text = "You saved the day\n...with minimal casualties";
                GameObject.Find("EndingImage").GetComponent<Image>().sprite = goodEndScreenImage;
                audioSourceGoodEnding.enabled = true;
                audioSourceGoodEnding.volume = Mathf.Lerp(audioSourceGoodEnding.volume, 0.6f, 0.03f);
                audioSourceBadEnding.volume = Mathf.Lerp(audioSourceBadEnding.volume, 0, 0.03f);

            }
            else {
                GameObject.Find("FinalTitleText").GetComponent<Text>().text = "Uhhhh you were supposed\nto save everybody...";
                GameObject.Find("EndingImage").GetComponent<Image>().sprite = badEndScreenImage;
                audioSourceBadEnding.enabled = true;
                audioSourceBadEnding.volume =  Mathf.Lerp(audioSourceBadEnding.volume, 0.6f, 0.03f);
                audioSourceGoodEnding.volume = Mathf.Lerp(audioSourceGoodEnding.volume, 0, 0.03f);
            }
        }
    }
}
