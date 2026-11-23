using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPersister : MonoBehaviour
{
    public static AudioPersister instance;
    public AudioSource audioSource;

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
    private void Awake()
    {
    int numMusicPlayers = FindObjectsByType<AudioPersister>(FindObjectsSortMode.None).Length;
    if (numMusicPlayers != 1)
    {
    audioSource.Stop();
    Destroy(this.gameObject);
    }
    }
}
