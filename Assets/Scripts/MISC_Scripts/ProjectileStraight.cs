using UnityEngine;

public class ProjectileStraight : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        //moves forward until death
    rb.linearVelocity = transform.up * speed;
    }
}
