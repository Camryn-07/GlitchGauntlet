using UnityEngine;
using UnityEngine.InputSystem;

public class MaceMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] macePoints;
    [SerializeField] private float speed;
    private int currentIndex;

    void Start()
    {
        currentIndex = 0;
    }

    void Update()
    {
        // Check if there are actually points assigned to prevent errors
        //if (macePoints == len || macePoints.Length == 0) return;

        if (Vector3.Distance(transform.position, macePoints[currentIndex].transform.position) < 0.1f)
        {
            currentIndex++;

            // FIX: Reset to 0 ONLY when we have reached the end of the array
            if (currentIndex >= macePoints.Length)
            {
                currentIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, macePoints[currentIndex].transform.position,
            speed * Time.deltaTime);
    }
}
