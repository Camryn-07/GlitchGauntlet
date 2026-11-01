using UnityEngine;
using TMPro;
public class ControlTextChanger : MonoBehaviour
{
    public TMP_Text p1ControlText;
    public TMP_Text p2ControlText;
    [SerializeField] private string p1ControlTextKeyboard;
    [SerializeField] private string p2ControlTextKeyboard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p1ControlTextKeyboard = p1ControlTextKeyboard.Replace("\\n", "\n");
        p2ControlTextKeyboard = p2ControlTextKeyboard.Replace("\\n", "\n");
        if(ControlSchemeUsed.controlSchemeToUse == 1){
            p1ControlText.text = p1ControlTextKeyboard;
            p2ControlText.text = p2ControlTextKeyboard;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
