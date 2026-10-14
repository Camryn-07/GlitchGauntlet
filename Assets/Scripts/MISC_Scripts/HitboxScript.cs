using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    //enemies are team 3, p1 is team 1, p2 is team 2.
    public int hitboxTeam;
    public float damageDealt;
    public float stunTimeDealt;
    public float iFrameTimeDealt;
    public float kbStrength;
    public float hitboxUptime;
    public bool deleteOnImpact;
    public bool isPlayer1;

    void Update()
    {
        if(hitboxUptime > 0){
            hitboxUptime -= Time.deltaTime;
            if(hitboxUptime < 0){
                //FOR THE LOVE OF GOD DO NOT MAKE ANYTHING THE PARENT OF A HITBOX IF YOU DONT WANT THAT PARENT TO BE DELETEDDDDDDD
                if(transform.parent.gameObject != null)
                {
                    Destroy(transform.parent.gameObject);
                } else
                {
                    Destroy(gameObject);
                }
                
            }
        }
    }
    
}
