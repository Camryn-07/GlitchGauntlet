using UnityEngine;
using UnityEngine.InputSystem;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        moveData = move.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveData.x * speed, moveData.y * speed);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Button"))
        {
            Debug.Log("buttonRegistered");
            buttonSelected = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Button"))
        {
            Debug.Log("buttonDeregistered");
            buttonSelected = null;
        }
    }
    private void ClickActionPerformed(InputAction.CallbackContext obj){
        if(buttonSelected != null){
        buttonSelected.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
    }
}
