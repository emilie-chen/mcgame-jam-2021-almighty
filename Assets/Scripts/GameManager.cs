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

    public GameObject characterDialogBox;
    public bool characterDialogBoxShown;
    private List<string> characterDialogues;

    public Color baseSkyColor;
    public Color darkerSkyColor;
    public Color redSkyColor;

    // Start is called before the first frame update
    void Start()
    {
        characterDialogues = new List<string>();
        baseResolution = Screen.currentResolution;

        fadeOut = GameObject.Find("FadeIn").GetComponent<Animator>();
        characterDialogBox = GameObject.Find("CharacterDialoguePanel");
        populationCircle = GameObject.Find("PopulationCircle");
        bossCircle = GameObject.Find("BossCircle");

        Invoke(nameof(FirstDialoguesSequence), 3);

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "TommyTestZone") { 
            FillBossCircle(bossLifePercentage);
            FillPopulationCircle(populationPercentage);
        }

        if (characterDialogBox != null)
        {
            if (characterDialogBoxShown == false)
            {
                characterDialogBox.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(characterDialogBox.GetComponent<RectTransform>().anchoredPosition,
                     new Vector3(0, 200, 0), 0.03f);

                if (characterDialogues.Count != 0) {
                    CharacterDialogue(characterDialogues[0]);
                    characterDialogues.RemoveAt(0);
                }
            }
            else {
                characterDialogBox.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(characterDialogBox.GetComponent<RectTransform>().anchoredPosition,
                     new Vector3(0, -20, 0), 0.03f);
            }
        }


        if (populationPercentage < 60 && populationPercentage > 20) {

            if (RenderSettings.skybox.HasProperty("_Tint"))
            {
                RenderSettings.skybox.SetColor("_Tint", Color.Lerp(RenderSettings.skybox.GetColor("_Tint"), darkerSkyColor, 0.03f));
            }
            else if (RenderSettings.skybox.HasProperty("_SkyTint")) {
                RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(RenderSettings.skybox.GetColor("_SkyTint"), darkerSkyColor, 0.03f));
            }
        }
        else if (populationPercentage < 20)
        {
            if (RenderSettings.skybox.HasProperty("_Tint"))
            {
                RenderSettings.skybox.SetColor("_Tint", Color.Lerp(RenderSettings.skybox.GetColor("_Tint"), redSkyColor, 0.03f));
            }
            else if (RenderSettings.skybox.HasProperty("_SkyTint"))
            {
                RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(RenderSettings.skybox.GetColor("_SkyTint"), redSkyColor, 0.03f));
            }
        } else 
        {
            if (RenderSettings.skybox.HasProperty("_Tint"))
            {
                RenderSettings.skybox.SetColor("_Tint", Color.Lerp(RenderSettings.skybox.GetColor("_Tint"), baseSkyColor, 0.03f));
            }
            else if (RenderSettings.skybox.HasProperty("_SkyTint"))
            {
                RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(RenderSettings.skybox.GetColor("_SkyTint"), baseSkyColor, 0.03f));
            }
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

    private void CharacterDialogue(string message) 
    {
        if (characterDialogBox != null && characterDialogBoxShown == false) {
            characterDialogBoxShown = true;
            characterDialogBox.transform.Find("DialogueTxt").GetComponent<Text>().text = message;
            Invoke(nameof(HideCharacterDialogue), 5f);
        }
    }

    public void enqueueCharacterDialogue(string message) {
        characterDialogues.Add(message);
    }

    public void HideCharacterDialogue()
    {
        characterDialogBoxShown = false;
    }

    public void FirstDialoguesSequence() {
        enqueueCharacterDialogue("Oh no... I need to save the city !");
        enqueueCharacterDialogue("I can walk with [W][A][S][D]\nI can jump with [Spacebar]");
        enqueueCharacterDialogue("I can sprint with [Ctrl]\nAnd finally, I can send fireballs with [Left click button]");
    }

}
