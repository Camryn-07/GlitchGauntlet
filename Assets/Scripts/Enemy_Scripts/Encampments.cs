using System.Collections;
using UnityEngine;

public class CampZone2D : MonoBehaviour
{
    [Header("Camp Settings")]
    [Tooltip("The item prefab to drop when this specific camp is empty.")]
    public GameObject itemToDrop;

    [Tooltip("The enemy prefab to spawn back in.")]
    public GameObject enemyPrefab;

    [Tooltip("How many seconds to wait before respawning this camp.")]
    public float respawnDelay = 5f;

    // We store the local layout positions of all initial children
    private System.Collections.Generic.List<Vector3> localSpawnPositions = new System.Collections.Generic.List<Vector3>();
    private bool isRespawning = false;

    private void Start()
    {
        // Save the starting positions of all enemies placed inside this folder
        foreach (Transform child in transform)
        {
            localSpawnPositions.Add(child.localPosition);
        }

        Debug.Log($"[{gameObject.name}] Setup complete. Tracking {localSpawnPositions.Count} total enemies.");
    }

    private void Update()
    {
        // If we are already counting down a respawn, do nothing
        if (isRespawning) return;

        // Check how many children are currently left alive inside this parent object
        if (transform.childCount == 0)
        {
            isRespawning = true;
            TriggerCampCleared();
        }
    }

    private void TriggerCampCleared()
    {
        Debug.Log($"[{gameObject.name}] SUCCESS: Every single child enemy is dead!");

        // Drop the item at the center of the camp zone
        if (itemToDrop != null)
        {
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
            Debug.Log($"[{gameObject.name}] Item dropped at camp center.");
        }

        // Start the 2D respawn clock
        StartCoroutine(RespawnCampTimer());
    }

    private IEnumerator RespawnCampTimer()
    {
        yield return new WaitForSeconds(respawnDelay);

        Debug.Log($"[{gameObject.name}] Respawning all {localSpawnPositions.Count} enemies now!");

        // Spawn them back exactly as children of this folder object
        foreach (Vector3 localPos in localSpawnPositions)
        {
            if (enemyPrefab != null)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, transform);
                newEnemy.transform.localPosition = localPos; // Places them right back at their starting offset
            }
        }

        // Open up tracking for the next battle loop
        isRespawning = false;
    }
}
