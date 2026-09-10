using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D myRb;
    private InputAction moveAction;
    private Vector2 moveData;
    //the 'regular' speed
    public float baseSpeed;
    //the current speed
    private float speed;
    private Vector2 lastPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPosition = transform.position;
        //making the distinction between Speed and baseSpeed for the purpose of coding speed increasing/decreasing effects later
        speed = baseSpeed;
        moveAction = InputSystem.actions.FindAction("Move");
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveData = moveAction.ReadValue<Vector2>();
        speed = baseSpeed;
        //calculate angle of movement
        Vector2 moveDirection = (Vector2)transform.position - lastPosition;
        //checks if movement has happened (avoids rotation resetting when standing still)
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            //calculates angle of movement
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            //rotates based on calculated angle
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
        lastPosition = transform.position;
    }
    private void FixedUpdate()
    {
        myRb.linearVelocity = new Vector2(moveData.x * speed, moveData.y * speed);
    }
}
