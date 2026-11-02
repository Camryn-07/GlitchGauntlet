using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovemnt : MonoBehaviour
{
    public GameObject[] players;
    private NavMeshAgent chaser;
    public float speed;
    public TagHandle Player;
    public float disableDuration = 0.3f;   
    private bool isAgentDisabled = false;
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
        if (isAgentDisabled) return;
        GameObject targetPlayer = GetClosestPlayer();
        if (targetPlayer != null)
        {
            chaser.SetDestination(targetPlayer.transform.position);
            FlipSprite(targetPlayer.transform.position);
        }
    }

    public IEnumerator DisableAgentRoutine()
    {
        isAgentDisabled = true;

        // 1. Completely turn off the NavMeshAgent component so external forces can push it
        chaser.enabled = false;

        // 2. Wait for the duration of the hit/knockback state
        yield return new WaitForSeconds(disableDuration);

        // 3. Turn the NavMeshAgent back on and resume tracking
        chaser.enabled = true;
        isAgentDisabled = false;
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
