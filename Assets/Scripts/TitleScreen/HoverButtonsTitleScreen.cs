using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButtonsTitleScreen : MonoBehaviour, 
     IPointerClickHandler,
     IPointerEnterHandler
    {

    public TitlescreenManager titleScreenManager;
    public PopPanelTitleScreen titleScreenPanel;
    public int position;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData){
        if (titleScreenManager != null) {
            titleScreenManager.ChangeChild(position);
        } else if (titleScreenPanel != null) {
            titleScreenPanel.ChangeChild(position);
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        if (titleScreenManager != null)
        {
            titleScreenManager.ExecuteButtonByClick();
        }
        else if (titleScreenPanel != null)
        {
            titleScreenPanel.ExecuteButtonByClick();
        }
    }
}
