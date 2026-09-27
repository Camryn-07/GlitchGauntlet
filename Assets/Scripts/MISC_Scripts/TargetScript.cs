using UnityEngine;
using System.Threading.Tasks;

public class TargetScript : MonoBehaviour
{
    //Works with HitboxScript
    public int targetTeam;
    //players should have canTakeDamage marked false, enemies should have it set to true.
    [SerializeField] private bool canTakeDamage;
    public bool inIFrames;
    public bool isStunned;
    public int healthValue;
    [SerializeField] private int maxHealthValue;
    [SerializeField] private float stunTimer;
    [SerializeField] private float iFramesTimer;
    [SerializeField] private Rigidbody2D myRb;

    [SerializeField]
    private int scorePerHit = 50;
    [SerializeField]
    private int scoreOnDeath = 100;
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
    public void isHitByAttack(bool isPlayer1, int damageValue, float kbStrength, Vector2 kbDirection, float stunTime, float iFrameTime){
        if(canTakeDamage)
        {
            if (isPlayer1)
            {
                Score.instance.ChangeP1Score(scorePerHit);
            }
            else
            {
                Score.instance.ChangeP2Score(scorePerHit);
            }

            healthValue -= damageValue;
            if (healthValue == 0)
            {
                if (isPlayer1)
                {
                    Score.instance.ChangeP1Score(scoreOnDeath);
                }
                else
                {
                    Score.instance.ChangeP2Score(scoreOnDeath);
                }

                Destroy(gameObject);           
            }
        }
        //sets the stun timer to stunTime if it would cause the stunned target to be stunned for longer
        //this is so you can't negate a large stun with a smaller stun
        if(stunTime > stunTimer){
            isStunned = true;
            stunTimer = stunTime;
            myRb.linearVelocity = new Vector2(0, 0);
        }
        myRb.AddForce(kbDirection * kbStrength, ForceMode2D.Impulse);
        iFramesTimer = iFrameTime;
    }
    void OnTriggerEnter2D(Collider2D triggerObject)
    {
        if (triggerObject.gameObject.CompareTag("Hitbox"))
        {
            HitboxScript HS = triggerObject.GetComponent<HitboxScript>();
            if(HS.hitboxTeam != targetTeam && inIFrames == false){
                Vector2 knockbackDirection = (transform.position - triggerObject.transform.position).normalized;
                isHitByAttack(HS.isPlayer1, HS.damageDealt, HS.kbStrength, knockbackDirection, HS.stunTimeDealt, HS.iFrameTimeDealt);
            }
            
        }
    }
}
