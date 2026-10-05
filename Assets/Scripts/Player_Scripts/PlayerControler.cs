using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private string playerMove;
    //[SerializeField] private string playerAttack;
    //[SerializeField] private string playerDash;
    //[SerializeField] private string playerShoot;
    private InputAction move;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 playerMovement;
    public float baseSpeed;
    public float moveSpeed;

    [SerializeField] private float walkSoundTimer;
    [SerializeField] private AudioClip walk;

    void Start()
    {
        move = InputSystem.actions.FindAction(playerMove);
        move.performed += movePerformed;
        move.canceled += moveCanceled;
        moveSpeed = baseSpeed;
    }

    private void movePerformed(InputAction.CallbackContext obj)
    {
        playerMovement = obj.ReadValue<Vector2>() * moveSpeed;
        Debug.Log("start moving");
    }

    private void moveCanceled(InputAction.CallbackContext obj)
    {
        playerMovement = Vector2.zero;
        Debug.Log("stop moving");
    }

    private void Update()
    {
        rb.linearVelocity = playerMovement;
    }

    private void OnDestroy()
    {
        move.performed -= movePerformed;
        move.canceled -= moveCanceled;
    }
}
