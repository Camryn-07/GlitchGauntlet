using System.Collections;
using UnityEngine;

public class CampZone2D : MonoBehaviour
{
    public GameObject itemToDrop;

    public GameObject enemyPrefab;

    public float respawnDelay = 5f;

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
        if (itemToDrop != null)
        {
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
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
            }
        }
        isRespawning = false;
    }
}
