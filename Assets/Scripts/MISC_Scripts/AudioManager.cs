using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
      //if (SceneManager.GetActiveScene().name == "Startmenu") || (SceneManager.GetActiveScene().name == "HowToPlay")
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
