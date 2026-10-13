using UnityEngine;

public class EnemiesListing : MonoBehaviour
{
    private TargetScript healthValue;

    private void Awake()
    {
        healthValue = GetComponent<TargetScript>();
   }
}
