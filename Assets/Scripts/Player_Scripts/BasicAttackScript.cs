using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicAttackScript : MonoBehaviour
{
    [SerializeField] private string playerAttack;
    [SerializeField] private string playerShoot;
    private InputAction attack;
    private InputAction shoot;
    //the hitbox of the basic attack
    [SerializeField] private GameObject basicAttackHitbox;
    [SerializeField] private GameObject projectileHitbox;
    //the timer that enforces the attack cooldown
    [SerializeField] private float attackCDTimer;
    //the attack cooldown
    [SerializeField] private float attackCDStandard;
    //the timer that enforces the shoot cooldown
    [SerializeField] private float shootCDTimer;
    //the shoot cooldown
    [SerializeField] private float shootCDStandard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private bool isPlayer1;
    // Update is called once per frame
    public AudioClip gauntlets;
    void Start(){
        attack = InputSystem.actions.FindAction(playerAttack);
        attack.performed += attackPerformed;
        shoot = InputSystem.actions.FindAction(playerShoot);
        shoot.performed += shootPerformed;
    }

    void Update()
    {
        if(attackCDTimer > 0){
            attackCDTimer -= Time.deltaTime;
            if(attackCDTimer < 0){
                attackCDTimer = 0;
            }
        }
        if(shootCDTimer > 0){
            shootCDTimer -= Time.deltaTime;
            if(shootCDTimer < 0){
                shootCDTimer = 0;
            }
        }
    }
    private void attackPerformed(InputAction.CallbackContext obj)
    {
        if(attackCDTimer <= 0){
        GameObject summonedObj = Instantiate(basicAttackHitbox, transform.position, transform.rotation);
            //sets hitbox to team of user automatically!!! im so smart and cool and awesome and goated for this
            //assuming the hitbox has a HitboxScript and the object this is attached to has a TargetScript. and also that the hitbox is the child of an object (you'll see when you look at the hitbox prefab why this matters (it's because the parent is the center of the object))
            //which is, like, required anyways.
            HitboxScript hitBox = summonedObj.GetComponentInChildren<HitboxScript>();
            hitBox.hitboxTeam = gameObject.GetComponent<TargetScript>().targetTeam;
            hitBox.isPlayer1 = isPlayer1;
            AudioSource.PlayClipAtPoint(gauntlets, transform.position);
            attackCDTimer = attackCDStandard;
        }
    }
    private void shootPerformed(InputAction.CallbackContext obj)
    {
        if(shootCDTimer <= 0){
            if(gameObject.GetComponent<ProjectilePowerup>().active == true){
            GameObject summonedObjR = Instantiate(projectileHitbox, transform.position, transform.rotation);
            HitboxScript hitBoxR = summonedObjR.GetComponentInChildren<HitboxScript>();
            hitBoxR.hitboxTeam = gameObject.GetComponent<TargetScript>().targetTeam;
            hitBoxR.isPlayer1 = isPlayer1;
            hitBoxR.damageDealt = gameObject.GetComponent<ProjectilePowerup>().projPower;
            shootCDTimer = shootCDStandard;
            }
        }
    }
    //void OnAttack(){
        
    //} 
}

