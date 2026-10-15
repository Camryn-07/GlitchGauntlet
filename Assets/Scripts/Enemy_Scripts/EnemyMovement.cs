using UnityEngine;
using UnityEngine.AI;

public class EnemyMovemnt : MonoBehaviour
{
    public GameObject player;
    public GameObject player2;
    private NavMeshAgent chaser;
    public float speed;

    void Start()
    {
        chaser = GetComponent<NavMeshAgent>();
        chaser.updateRotation = false;
        chaser.updateUpAxis = false;
        chaser.speed = speed;
        if (player == null) player = GameObject.Find("The Player 1");
        if (player2 == null) player2 = GameObject.Find("The Player 2");
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
        if (player != null && player2 == null) return player;
        if (player == null && player2 != null) return player2;
        float distanceToP1 = Vector3.Distance(transform.position, player.transform.position);
        float distanceToP2 = Vector3.Distance(transform.position, player2.transform.position);
        return (distanceToP1 < distanceToP2) ? player : player2;
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
