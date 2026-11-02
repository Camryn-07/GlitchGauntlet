using UnityEngine;
using UnityEngine.InputSystem;
public class ChangeControls : MonoBehaviour
{
    [SerializeField] private int controlSchemeNum;
    private bool buttonActive;
    void Update(){
        if(ControlSchemeUsed.controlSchemeToUse == controlSchemeNum){
            buttonActive = false;
        } else {
            buttonActive = true;
        }
        if(buttonActive == false){
        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.gray;
        } else {
        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }

    public void setControlScheme(){
        ControlSchemeUsed.controlSchemeToUse = controlSchemeNum;
    }
}
