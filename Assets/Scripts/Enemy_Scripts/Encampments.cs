using System.Collections;
using UnityEngine;

public class CampZone2D : MonoBehaviour
{
    [SerializeField] private GameObject[] upgrades;
    public GameObject enemyPrefab;
    public float respawnDelay = 5f;
    private GameObject randomUpgrades;
    private System.Collections.Generic.List<Vector3> localSpawnPositions = new System.Collections.Generic.List<Vector3>();
    private bool isRespawning = false;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            localSpawnPositions.Add(child.localPosition);
        }
    }

    private void Update()
    {

        if (isRespawning) return;

        if (transform.childCount == 0)
        {
            isRespawning = true;
            TriggerCampCleared();
        }
    }
    private void TriggerCampCleared()
    {

        if (upgrades != null && upgrades.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, upgrades.Length);
            randomUpgrades = upgrades[randomIndex];

            if (randomUpgrades != null)
            {
                Instantiate(randomUpgrades, transform.position, Quaternion.identity);
            }
        }
        StartCoroutine(RespawnCampTimer());
    }
  
    private IEnumerator RespawnCampTimer()
    {
        yield return new WaitForSeconds(respawnDelay);
        foreach (Vector3 localPos in localSpawnPositions)
        {
            if (enemyPrefab != null)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, transform);
                newEnemy.transform.localPosition = localPos;
                newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(newEnemy.transform.position);
            }
        }
            isRespawning = false;
    }
}
