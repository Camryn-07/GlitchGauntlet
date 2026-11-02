using System.Collections;
using UnityEngine;

public class EnemyTilemapRainAttack2D : MonoBehaviour
{
    public TargetScript TC;
    [Header("Attack Prefabs")]
    public GameObject ammo;
    public GameObject warningSign;

    [Header("Timing Settings")]
    public float timeBetweenAttacks = 5f;
    public float attackDuration = 3f;
    public float spawnInterval = 0.3f;
    public float telegraphTime = 0.5f;

    [Header("Tilemap Attack Zone")]
    public float attackRadius = 5f;
    public float fallHeight = 6f;
    public float scanHeightOffset = 3f;

    public float castRadius = 0.3f;
    public LayerMask tilemapLayer;

    private void OnEnable()
    {
        TargetScript.startBossShooting += BossAttack;
        
    }

    private void BossAttack()
    {
        if (TC.healthValue <= 6)
        {
            Debug.Log("startit");
            StartCoroutine(InfiniteAttackLoop());
        }
    }

    private IEnumerator InfiniteAttackLoop()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnObjectsRoutine());
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    private IEnumerator SpawnObjectsRoutine()
    {
        float timer = 0f;

        while (timer < attackDuration)
        {
            float randomXOffset = Random.Range(-attackRadius, attackRadius);

            Vector3 scanOrigin = new Vector3(
                transform.position.x + randomXOffset,
                transform.position.y + scanHeightOffset,
                0f
            );

            RaycastHit2D tilemapHit = Physics2D.CircleCast(scanOrigin, castRadius, Vector2.down, 25f, tilemapLayer);

            if (tilemapHit.collider != null)
            {
                Vector3 floorPosition = tilemapHit.point;
                Vector3 highSpawnPosition = floorPosition + new Vector3(0f, fallHeight, 0f);

                StartCoroutine(TelegraphAndDrop(floorPosition, highSpawnPosition));
            }

            yield return new WaitForSeconds(spawnInterval);
            timer += spawnInterval;
        }
    }
    private IEnumerator TelegraphAndDrop(Vector3 groundPos, Vector3 spawnPos)
    {
        GameObject warning = Instantiate(warningSign, groundPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(warning);
        Instantiate(ammo, groundPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 leftBound = transform.position + Vector3.left * attackRadius;
        Vector3 rightBound = transform.position + Vector3.right * attackRadius;

        // Draw horizontal line representing the zone
        Gizmos.DrawLine(leftBound, rightBound);

        // Draw search boundary limits
        Gizmos.DrawWireSphere(leftBound, castRadius);
        Gizmos.DrawWireSphere(rightBound, castRadius);
    }
}
