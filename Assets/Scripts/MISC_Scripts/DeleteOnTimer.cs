using UnityEngine;

public class DeleteOnTimer : MonoBehaviour
{
    public float objUptime;
    void Update()
    {
        if(objUptime > 0){
            objUptime -= Time.deltaTime;
            if(objUptime < 0){
                {
                    Destroy(gameObject);
                }
                
            }
        }
    }
}
