using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitlescreenManager : MonoBehaviour
{
    public Text[] boutonMenus;
    public int buttonPosition;
    public Color colorInactive;
    public Color colorActive;
    public GameObject fadeOut;
    public bool popUpOpen;
    public PlaySounds soundModule;
    public bool canButtonDown;

    // Start is called before the first frame update
    void Start()
    {       
        fadeOut = GameObject.Find("FadeIn");
        UpdateButtons();
        soundModule = GetComponent<PlaySounds>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");

            if (!popUpOpen)
            {
                if (canButtonDown)
                {
                    if (verticalAxis != 0)
                    {
                        canButtonDown = false;
                        buttonPosition -= Mathf.RoundToInt(verticalAxis);
                        if (buttonPosition >= 0 && buttonPosition < boutonMenus.Length)
                        {
                            soundModule.playSound(0);
                        }
                        buttonPosition = Mathf.Clamp(buttonPosition, 0, boutonMenus.Length - 1);
                        UpdateButtons();
                    }
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                    {
                        canButtonDown = false;
                        ExecuteButton(buttonPosition);
                        soundModule.playSound(0);
                }
            }
            }


        if (verticalAxis == 0 && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Return))
        {
            canButtonDown = true;
        }
       
    }

    void UpdateButtons(){
        int i = 0;
        foreach (Text txt in boutonMenus) {
            if (i == buttonPosition) {
                txt.color = colorActive; 
            } else {
                txt.color = colorInactive;
            }         
            i++;
        }
    }

    void ExecuteButton(int buttonPosition) {
        print(boutonMenus[buttonPosition].tag);
        switch (boutonMenus[buttonPosition].tag) {
            case "NewGameButton" :
                NewGame();
                break;
            case "OptionsButton":
                Settings();
                break;
            case "CreditsButton":
                Credits();
                break;
            case "ExitButton":
                ExitGame();
                break;
        }
    }

    void NewGame() {
        if (SceneManager.GetActiveScene().name == "Title Screen")
        {
            fadeOut.GetComponent<Animator>().SetTrigger("FadeOut");
            SceneManager.LoadScene("Game");
        }
    }

    void Settings() {
        GameObject.FindGameObjectWithTag("OptionsButton").transform.GetChild(0).gameObject.GetComponent<PopPanelTS>().Activate();
        popUpOpen = true;
    }

    void Credits()
    {
        GameObject.FindGameObjectWithTag("CreditsButton").transform.GetChild(0).gameObject.GetComponent<PopPanelTS>().Activate();
        popUpOpen = true;
    }

    void ExitGame()
    {
        GameObject.FindGameObjectWithTag("ExitButton").transform.GetChild(0).gameObject.GetComponent<PopPanelTS>().Activate();
        popUpOpen = true;
    }

    public void ChangeChild(int childSelected)
    {
        if (!popUpOpen) { 
            this.buttonPosition = childSelected;
            UpdateButtons();
        }
    }

    public void ExecuteButtonByClick()
    {
        if (!popUpOpen && canButtonDown) { 
            canButtonDown = false;
            ExecuteButton(buttonPosition);
            soundModule.playSound(0);
        }
    }

}
