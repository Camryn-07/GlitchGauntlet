using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashPowerup : MonoBehaviour
{
    public bool active;
    public int dLevel;
    public PlayerMovement PM;
    public float dashPower;

    //this is important because it lets us scale dash power forever!!
    //so we dont need to recode the entire thing if we decide "hey we want more/less levels for our powerup"
    public float dashPowerScaling;
    public float dashPowerBase;
    public int dashLevelMax;
    private InputAction dashAction;
    private Coroutine dashRoutine;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldownTime;
    private bool isDashing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDashing = false;
        active = false;
        dashPower = dashPowerBase + (dashPowerScaling * dLevel);
    }

    void OnDash()
    {
        if(active == true && isDashing == false)
        {
            Debug.Log("OnDash");
            dashRoutine = StartCoroutine(DashMovement());
        }
    }

    private IEnumerator DashMovement()
    {
        isDashing = true;
        //make player fast for a short time
        PM.speedAfterBoosts = PM.speedAfterBoosts + dashPower;
        yield return new WaitForSeconds(dashTime);
        PM.speedAfterBoosts = PM.baseSpeed;
        yield return new WaitForSeconds(dashCooldownTime);
        isDashing = false;
        StopCoroutine(dashRoutine);
    }
    public void LevelUpDash()
    {
        active = true;
        dLevel += 1;
        if(dLevel > dashLevelMax)
        {
            dLevel = dashLevelMax;
        }
        dashPower = dashPowerBase + (dashPowerScaling * dLevel);
    }
}


