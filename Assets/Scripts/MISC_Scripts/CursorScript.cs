using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class CursorScript : MonoBehaviour
{
    private InputAction move;
    private InputAction clickAction;
    [SerializeField] private Vector2 moveData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject buttonSelected;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("MoveCursor");
        clickAction = InputSystem.actions.FindAction("ClickCursor");
        clickAction.performed += ClickActionPerformed;
    }
    void Update()
    {
        moveData = move.ReadValue<Vector2>();
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + (moveData.x * speed), gameObject.transform.position.y + (moveData.y * speed));
        isPointerOverUI();
    }

    // Update is called once per frame
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Button"))
    //    {
    //        Debug.Log("buttonRegistered");
    //        buttonSelected = collision.gameObject;
    //    }
    //}
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Button"))
    //    {
    //        Debug.Log("buttonDeregistered");
    //        buttonSelected = null;
    //    }
    //}
    private void ClickActionPerformed(InputAction.CallbackContext obj){
        if(buttonSelected != null){
        buttonSelected.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
    }
    public bool isPointerOverUI()
    {
        PointerEventData Data = new PointerEventData(EventSystem.current)
        {
            position = transform.position
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(Data, results);
        Debug.Log(results.Count);
        if(results.Count < 3)
        {
            buttonSelected = null;
        }
        for(int i = 0; i < results.Count; i++)
        {
            GameObject aObject = results[i].gameObject;
            if (aObject.CompareTag("Button") == true)
            {
                buttonSelected = results[i].gameObject;
            }
        }
        return true;
    }
}
