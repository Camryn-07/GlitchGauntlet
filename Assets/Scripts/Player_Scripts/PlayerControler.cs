using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private string playerMove;
    //[SerializeField] private string playerAttack;
    //[SerializeField] private string playerShoot;
    private InputAction move;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 playerMovement;
    private Vector2 moveDirection;
    public float baseSpeed;
    public float moveSpeed;

    private Vector2 lastPosition;
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
        moveDirection = obj.ReadValue<Vector2>();
        moving();
    }
    public void moving()
    {
        playerMovement = moveDirection * moveSpeed;
    }

    private void moveCanceled(InputAction.CallbackContext obj)
    {
        playerMovement = Vector2.zero;
    }

    private void Update()
    {
        //moving
        rb.linearVelocity = playerMovement;
        //walk sfx timer
        if (walkSoundTimer > 0)
        {
            walkSoundTimer -= Time.deltaTime;
            if (walkSoundTimer < 0)
            {
                walkSoundTimer = 0;
            }
        }

        //following code copied from Cameron Chrones
        //calculate angle of movement
        Vector2 moveDirection = (Vector2)transform.position - lastPosition;
        //checks if movement has happened (avoids rotation resetting when standing still) (also used for the audio clip)
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            if (walkSoundTimer == 0)
            {
                AudioSource.PlayClipAtPoint(walk, transform.position);
                walkSoundTimer = 0.5f;
            }
            //calculates angle of movement
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            //rotates based on calculated angle
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
        lastPosition = transform.position;
    }

    private void OnDestroy()
    {
        move.performed -= movePerformed;
        move.canceled -= moveCanceled;
    }
}
