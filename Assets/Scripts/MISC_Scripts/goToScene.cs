using UnityEngine;
using UnityEngine.SceneManagement;

public class goToScene : MonoBehaviour
{
    [SerializeField] private int sceneNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void goToTheScene(){
        SceneManager.LoadScene(sceneNum); 
    }
}
