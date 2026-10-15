using System.Collections;
using UnityEngine;

public class EnemyRainAttack2D : MonoBehaviour
{
    public GameObject ammo;
    public GameObject warningSign;
    public float timeBetweenAttacks = 5f;
    public float attackDuration = 3f;
    public float spawnInterval = 0.3f;
    public float telegraphTime = 0.5f;
    public float spawnWidth = 6f;
    public float spawnHeight = 7f;

    private void Start()
    {
        StartCoroutine(InfiniteAttackLoop());
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
            float randomX = Random.Range(-spawnWidth / 2f, spawnWidth / 2f);
            Vector3 spawnPosition = new Vector3(
                transform.position.x + randomX,
                transform.position.y + spawnHeight,
                0f
            );

            StartCoroutine(TelegraphAndDrop(spawnPosition));

            yield return new WaitForSeconds(spawnInterval);
            timer += spawnInterval;
        }
    }

    private IEnumerator TelegraphAndDrop(Vector3 spawnPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, spawnHeight * 2f);

        if (hit.collider != null)
        {
            GameObject warning = Instantiate(warningSign, hit.point, Quaternion.identity);
            Destroy(warning, telegraphTime);
        }
        yield return new WaitForSeconds(telegraphTime);
        Instantiate(ammo, spawnPos, Quaternion.identity);
    }
}
