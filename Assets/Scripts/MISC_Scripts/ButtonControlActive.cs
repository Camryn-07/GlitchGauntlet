using UnityEngine;

public class ButtonControlActive : MonoBehaviour
{
    public GameObject keyboardControlIndicator;
    public GameObject arcadeControlIndicator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyboardControlIndicator.SetActive(false);
        arcadeControlIndicator.SetActive(false);
        if(ControlSchemeUsed.controlSchemeToUse == 1){
            keyboardControlIndicator.SetActive(true);
        } else {
            arcadeControlIndicator.SetActive(true);
        }
    }
}
