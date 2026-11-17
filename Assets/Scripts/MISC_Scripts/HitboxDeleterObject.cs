using UnityEngine;

public class HitboxDeleterObject : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D triggerObject)
    {
        if (triggerObject.gameObject.CompareTag("Hitbox"))
        {
            Debug.Log("targetscript hit by hitbox");

            if(triggerObject.transform.parent.gameObject != null)
            {
                Destroy(triggerObject.transform.parent.gameObject);
            } else
            {
                Destroy(triggerObject.gameObject);
            }
        }
    }
}
