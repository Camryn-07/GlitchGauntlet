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
        //chaser = GetComponent<NavMeshAgent>();
        //chaser.updateRotation = false;
        //chaser.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        //chaser.SetDestination(player.transform.position);
        //chaser.SetDestination(player2.transform.position);
    }
}


