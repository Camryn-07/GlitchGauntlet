using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 5f);
    }
}