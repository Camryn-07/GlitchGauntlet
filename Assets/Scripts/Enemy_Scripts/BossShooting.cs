using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTilemapRainAttack2D : MonoBehaviour
{
    public TargetScript TC;

    [Header("Attack Prefabs")]
    public GameObject ammo;
    public GameObject warningSign;

    [Header("Timing Settings")]
    public float timeBetweenAttacks = 5f;
    public float attackDuration = 3f;
    public float spawnInterval = 0.25f;
    public float telegraphTime = 1f;

    [Header("Circle Scatter Settings")]
    public float attackRadius = 5f;
    public bool checkForWalkableFloor = false;
    public LayerMask tilemapLayer;

    private bool isAttacking = false;
    private NavMeshAgent agent;
    private EnemyMovemnt enemyMovement;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyMovement = GetComponent<EnemyMovemnt>();
    }

    private void OnEnable()
    {
        TargetScript.startBossShooting += BossAttack;
    }

    private void OnDisable()
    {
        TargetScript.startBossShooting -= BossAttack;
    }

    private void BossAttack()
    {
        if (TC.healthValue <= 6 && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(InfiniteAttackLoop());
        }
    }

    private IEnumerator InfiniteAttackLoop()
    {
        while (true)
        {
            animator.SetBool("isShooting", true);
            yield return StartCoroutine(SpawnCircleScatterRoutine());
            animator.SetBool("isShooting", false);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    private IEnumerator SpawnCircleScatterRoutine()
    {
        SetBossMovement(false);

        float timer = 0f;

        while (timer < attackDuration)
        {
            Vector2 randomCircleOffset = Random.insideUnitCircle * attackRadius;
            Vector3 targetPos = transform.position + new Vector3(randomCircleOffset.x, randomCircleOffset.y, 0f);

            if (!checkForWalkableFloor || Physics2D.OverlapCircle(targetPos, 0.2f, tilemapLayer) != null)
            {
                StartCoroutine(TelegraphAndDrop(targetPos));
            }

            yield return new WaitForSeconds(spawnInterval);
            timer += spawnInterval;
        }

        yield return new WaitForSeconds(telegraphTime);

        SetBossMovement(true);
    }

    private void SetBossMovement(bool canMove)
    {
        if (enemyMovement != null)
        {
            enemyMovement.enabled = canMove;
        }

        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = !canMove;
            if (!canMove)
            {
                agent.ResetPath();
            }
        }
    }

    private IEnumerator TelegraphAndDrop(Vector3 groundPos)
    {
        GameObject warning = Instantiate(warningSign, groundPos, Quaternion.identity);
        yield return new WaitForSeconds(telegraphTime);
        if (warning != null) Destroy(warning);
        Instantiate(ammo, groundPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
