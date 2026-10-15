using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f,0);;
    }
}
