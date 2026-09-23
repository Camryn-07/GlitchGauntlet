using UnityEngine;

public class BasicAttackScript : MonoBehaviour
{
    //the hitbox of the basic attack
    [SerializeField] private GameObject basicAttackHitbox;
    //the timer that enforces the attack cooldown
    [SerializeField] private float attackCDTimer;
    //the attack cooldown
    [SerializeField] private float attackCDStandard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if(attackCDTimer > 0){
            attackCDTimer -= Time.deltaTime;
            if(attackCDTimer < 0){
                attackCDTimer = 0;
            }
        }
    }
    void OnAttack(){
        if(attackCDTimer <= 0){
        GameObject summonedObj = Instantiate(basicAttackHitbox, transform.position, transform.rotation);
        //sets hitbox to team of user automatically!!! im so smart and cool and awesome and goated for this
        //assuming the hitbox has a HitboxScript and the object this is attached to has a TargetScript. and also that the hitbox is the child of an object (you'll see when you look at the hitbox prefab why this matters (it's because the parent is the center of the object))
        //which is, like, required anyways.
        summonedObj.GetComponentInChildren<HitboxScript>().hitboxTeam = gameObject.GetComponent<TargetScript>().targetTeam;
        attackCDTimer = attackCDStandard;
        }
    }
}
