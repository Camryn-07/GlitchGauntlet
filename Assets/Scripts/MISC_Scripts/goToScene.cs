using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class goToScene : MonoBehaviour
{
    [SerializeField] private int sceneNum;
    [SerializeField] private Canvas canvas;
    private InputAction pause;
    private void Start()
    {
        pause = InputSystem.actions.FindAction("Pause");
        pause.performed += Pause_performed;
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        canvas.enabled = true;
        Time.timeScale = 0;
    }

    public void goToTheScene(){
        SceneManager.LoadScene(sceneNum); 
    }
    public void restartScene()
    {
        SceneManager.LoadScene(sceneNum);
        Time.timeScale = 1;
    }
    public void Unppause()
    {
        canvas.enabled = false;
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
