using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    //unfinished script!!!!!!!!!!!!!!!!!!!!!! the idea here is that the 'team' of the hitbox decides who's hit by it and who isnt (to stop the player from hitting themself with it).
    //enemies are team 3, p1 is team 1, p2 is team 2.
    public int hitboxTeam;
    public int damageDealt;
    public float stunTimeDealt;
    public float iFrameTimeDealt;
    public float kbStrength;
    public float hitboxUptime;

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
