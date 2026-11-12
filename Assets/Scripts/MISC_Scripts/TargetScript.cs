using UnityEngine;
using System.Threading.Tasks;
using System;

public class TargetScript : MonoBehaviour
{
    //0 = p1
    //1 = p2
    //2 = enemy
    //3 = boss
    public int targetType;
    //Works with HitboxScript
    public int targetTeam;
    //players should have canTakeDamage marked false, enemies should have it set to true.
    [SerializeField] public bool canTakeDamage;
    public bool inIFrames;
    public bool isStunned;
    public float healthValue;
    [SerializeField] private float maxHealthValue;
    [SerializeField] private float stunTimer;
    [SerializeField] private float iFramesTimer;
    [SerializeField] private Rigidbody2D myRb;
    public AudioClip attack;
    
    [SerializeField]
    private int scorePerHit = 50;
    [SerializeField]
    private int scoreOnDeath = 100;
    [SerializeField] private Material flashMaterial;
    private Material tempMatStore;
    private int scoreLost = 100;
    [SerializeField] private int flashTime = 500;
    public static Action startBossShooting;
    [SerializeField] private bool spriteFlashPlayer;
    [SerializeField] private GameObject deathParticles;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        healthValue = maxHealthValue;
    }

    void Update(){
        if(stunTimer > 0){
            stunTimer -= Time.deltaTime;
            if(stunTimer < 0){
                stunTimer = 0;
            }
        }
        if(iFramesTimer > 0){
            iFramesTimer -= Time.deltaTime;
            if(iFramesTimer < 0){
                iFramesTimer = 0;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(stunTimer > 0){
            isStunned = true;
        } else {
            isStunned = false;
        }
        if(iFramesTimer > 0){
            inIFrames = true;
        } else {
            inIFrames = false;
        }
    }


    //handles being hit by a hitbox & the values pertaining to that
    public void isHitByAttack(bool isPlayer1, float damageValue, float kbStrength, Vector2 kbDirection, float stunTime, float iFrameTime)
    {
        if(canTakeDamage)
        {
            if (targetType == 3)
            {
                startBossShooting?.Invoke();
                if (isPlayer1)
                {
                Score.instance.ChangeP1Score(scorePerHit);
                }
                else
                {
                Score.instance.ChangeP2Score(scorePerHit);
                }
            }


            healthValue -= damageValue;
            if (healthValue <= 0)
            {
                if (isPlayer1)
                {
                    Score.instance.ChangeP1Score(scoreOnDeath);
                }
                else
                {
                    Score.instance.ChangeP2Score(scoreOnDeath);
                }
                Instantiate(deathParticles, transform.position, transform.rotation);
                Destroy(gameObject);           
            }
        }
         
        //sets the stun timer to stunTime if it would cause the stunned target to be stunned for longer
        //this is so you can't negate a large stun with a smaller stun
        if(stunTime > stunTimer){
            isStunned = true;
            AudioSource.PlayClipAtPoint(attack,transform.position);
            stunTimer = stunTime;
            myRb.linearVelocity = new Vector2(0, 0);
        }
        myRb.AddForce(kbDirection * kbStrength, ForceMode2D.Impulse);
        iFramesTimer = iFrameTime;
        Debug.Log(gameObject.name+ "  Knockback: "+myRb.linearVelocity.magnitude);
        TempFlash();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.LogWarning("Hit sometin");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            if (collision.gameObject.TryGetComponent<BasicAttackScript>(out BasicAttackScript attackScript))
            {
            
                Debug.Log("Hit player with HitboxScript: " + attackScript.isPlayer1);

                if (attackScript.isPlayer1)
                {
                    Score.instance.ChangeP1Score(scoreLost);
                }
                else
                {
                    Score.instance.ChangeP2Score(scoreLost);
                }
            }
        }
        else
        {
            HitboxScript HS = collision.gameObject.GetComponent<HitboxScript>();
            if (HS != null && HS.hitboxTeam != targetTeam && inIFrames == false)
            {
                Debug.Log("Call knockback");
                Vector2 knockbackDirection = (transform.position - collision.gameObject.transform.position).normalized;
                isHitByAttack(HS.isPlayer1, HS.damageDealt, HS.kbStrength, knockbackDirection, HS.stunTimeDealt, HS.iFrameTimeDealt);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D triggerObject)
    {
        if (triggerObject.gameObject.CompareTag("Hitbox"))
        {
            Debug.Log("targetscript hit by hitbox");
            if (TryGetComponent<EnemyMovemnt>(out EnemyMovemnt enemyMovement))
            {
                enemyMovement.StartCoroutine(enemyMovement.DisableAgentRoutine());        
            }
            HitboxScript HS = triggerObject.GetComponent<HitboxScript>();
            if(HS.hitboxTeam != targetTeam && inIFrames == false){
                Debug.Log("Call knockback");
                Vector2 knockbackDirection = (transform.position - triggerObject.transform.position).normalized;
                isHitByAttack(HS.isPlayer1, HS.damageDealt, HS.kbStrength, knockbackDirection, HS.stunTimeDealt, HS.iFrameTimeDealt);
            }
            if (HS.hitboxTeam != targetTeam && inIFrames == false && HS.deleteOnImpact == true){
                if(triggerObject.transform.parent.gameObject != null)
                {
                    Destroy(triggerObject.transform.parent.gameObject);
                } else
                {
                    Destroy(triggerObject.gameObject);
                }
            }
            
        }
        if (triggerObject.gameObject.CompareTag("WallTrigger") && triggerObject.gameObject.GetComponent<WallTriggerer>().hasSummoned == false && gameObject.CompareTag("Player"))
        {
            triggerObject.gameObject.GetComponent<WallTriggerer>().summonWall();
        }
    }
    public async Task TempFlash(){
        if(spriteFlashPlayer == false){
            tempMatStore = gameObject.GetComponent<SpriteRenderer>().material;
            gameObject.GetComponent<SpriteRenderer>().material = flashMaterial;
            await Task.Delay(flashTime);
            gameObject.GetComponent<SpriteRenderer>().material = tempMatStore;
        } else if(spriteFlashPlayer == true){
            Debug.Log("spriteFlashPlayer True!!");
            GameObject theSkin = gameObject.GetComponent<PlayerControler>().theSkin;
            tempMatStore = theSkin.GetComponent<SpriteRenderer>().material;
            theSkin.GetComponent<SpriteRenderer>().material = flashMaterial;
            await Task.Delay(flashTime);
            theSkin.GetComponent<SpriteRenderer>().material = tempMatStore;
        }
    }
}