using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPersister : MonoBehaviour
{
    public static AudioPersister instance;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Startmenu")
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "HowToPlay")
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
