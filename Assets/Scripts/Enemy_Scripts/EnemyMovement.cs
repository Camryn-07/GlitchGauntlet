using UnityEngine;
using UnityEngine.AI;

public class EnemyMovemnt : MonoBehaviour
{
    public GameObject[] players;
    private NavMeshAgent chaser;
    public float speed;
    public TagHandle Player;

    void Start()
    {
        chaser = GetComponent<NavMeshAgent>();
        chaser.updateRotation = false;
        chaser.updateUpAxis = false;
        chaser.speed = speed;

        players = GameObject.FindGameObjectsWithTag("Player");
    }
    void Update()
    {
        GameObject targetPlayer = GetClosestPlayer();
        if (targetPlayer != null)
        {
            chaser.SetDestination(targetPlayer.transform.position);
            FlipSprite(targetPlayer.transform.position);
        }
    }
    GameObject GetClosestPlayer()
    {
        GameObject player = null;

        float minDistance = 10;
        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(transform.position, p.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                player = p;
            }
        }

        return player;
    }
    void FlipSprite(Vector3 targetPosition)
    {
        if (targetPosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (targetPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
