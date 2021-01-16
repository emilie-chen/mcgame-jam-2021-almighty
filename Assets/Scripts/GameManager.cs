using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static Resolution baseResolution;
    public static Animator fadeOut;
    public GameObject instance;


    // Start is called before the first frame update
    void Start()
    {
        baseResolution = Screen.currentResolution;
        fadeOut = GameObject.Find("FadeIn").GetComponent<Animator>();
        instance = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
 
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

    public void LoadHub() {
        fadeOut.Play("FadeOut");
        Invoke("GoHub", 1f);
    }

    public void GoHub() {
        SceneManager.LoadScene("Hub");
    }

}
