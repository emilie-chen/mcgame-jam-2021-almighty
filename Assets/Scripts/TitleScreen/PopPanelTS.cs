using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopPanelTS : MonoBehaviour
{

    TitlescreenManager tsManager;
    public GameObject childPannel;
    public Text[] childOptions;
    public int childSelected;
    public bool activated;
    public Color colorInactive;
    public Color colorActive;
    public PlaySounds soundModule;
    

    // Start is called before the first frame update
    void Start()
    {
        childPannel = transform.GetChild(0).gameObject;
        this.tsManager = GameObject.Find("Canvas").GetComponent<TitlescreenManager>();
        this.soundModule = GameObject.Find("Canvas").GetComponent<PlaySounds>();
        colorInactive = tsManager.colorInactive;
        colorActive = tsManager.colorActive;
        UpdateChildButtons();
        Desactivate();
    }

    // Update is called once per frame
    void Update()
    {
          if (activated) {
            float verticalAxis = Input.GetAxisRaw("Vertical");
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float movement = 0;

            if (verticalAxis != 0) {
                movement = verticalAxis;
            }
            else if (horizontalAxis != 0) { 
                movement -= horizontalAxis;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Desactivate();
            }

            if (tsManager.canButtonDown)
                {

                 if (movement != 0)
                    {
                    tsManager.canButtonDown = false;
                    childSelected -= Mathf.RoundToInt(movement);
                    if (childSelected >= 0 && childSelected < childOptions.Length)
                    {
                        soundModule.playSound(0);
                    }
                    childSelected = Mathf.Clamp(childSelected, 0, childOptions.Length - 1);
                    UpdateChildButtons();
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                    {
                        tsManager.canButtonDown = false;
                        soundModule.playSound(0);
                        ExecuteButton(childSelected);
                    }
                }
            }  
    }

    public void Activate() {
        this.tsManager.popUpOpen = true;
        activated = true;
        GetComponent<Image>().enabled = true;
        childPannel.SetActive(true);
    }

    public void Desactivate() {
        this.tsManager.popUpOpen = false;
        activated = false;
        GetComponent<Image>().enabled = false;
        childPannel.SetActive(false);
    }

    void UpdateChildButtons() {
        int i = 0;
        foreach (Text txt in childOptions)
        {
            if (i == childSelected)
            {
                txt.color = colorActive;
            }
            else
            {
                txt.color = colorInactive;
            }

            i++;
        }
    }

    void ExecuteButton(int childSelected) {
        switch (childOptions[childSelected].tag)
        {
            case "ClosePannelButton":
                Desactivate();
                break;
            case "FullscreenButton":
                ToggleFullScreen(childOptions[childSelected].gameObject);
                break;
            case "NewGameButton":
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LoadHub();
                break;
            case "ExitButton":
                Application.Quit();
                break;
        }
    }

    private void ToggleFullScreen(GameObject panneau)
    {
        GameManager.ToggleFullscreen(!Screen.fullScreen);
        GameObject.Find("CheckboxFullscreen").GetComponent<Image>().enabled = Screen.fullScreen;
    }
}
