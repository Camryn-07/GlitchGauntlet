using UnityEngine;

public class BGM_Switcher : MonoBehaviour
{
    [SerializeField] private GameObject BGM;
    [SerializeField] private GameObject VT;

    [SerializeField] private GameObject menuCheck;
    
    void Update()
    {
        if (menuCheck.activeSelf == true)
        {
            BGM.SetActive(false);
            VT.SetActive(true);
        }
    }
}
