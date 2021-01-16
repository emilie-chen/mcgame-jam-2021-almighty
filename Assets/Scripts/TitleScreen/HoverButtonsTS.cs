using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButtonsTS : MonoBehaviour, 
     IPointerClickHandler,
     IPointerEnterHandler
    {

    public TitlescreenManager tsManager;
    public PopPanelTS tsPanel;
    public int position;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData){
        if (tsManager != null) {
            tsManager.ChangeChild(position);
        } else if (tsPanel != null) {
            tsPanel.ChangeChild(position);
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        if (tsManager != null)
        {
            tsManager.ExecuteButtonByClick();
        }
        else if (tsPanel != null)
        {
            tsPanel.ExecuteButtonByClick();
        }
    }
}
