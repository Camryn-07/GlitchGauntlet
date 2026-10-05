using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashPowerup : MonoBehaviour
{
    public bool active;
    public int dLevel;
    [SerializeField] private int dashLevelMax;
    [SerializeField] private float dashPowerScaling;
    [SerializeField] float startupPower;
    public float dashPower;
    public PlayerControler PC;
    private InputAction dashAction;
    [SerializeField] private string playerDash;
    private Coroutine dashRoutine;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldownTime;
    public bool canDash;
    
    void Start()
    {
        canDash = true;
        active = false;
        dLevel = 0;
        dashPower = 0;
        dashAction = InputSystem.actions.FindAction(playerDash);
        dashAction.performed += dashActionPerformed;
    }

    private void dashActionPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("try to dash");
        if (active == true && canDash == true)
        {
            Debug.Log("Dash start");
            dashRoutine = StartCoroutine(dashMovement());
        }
    }

    private IEnumerator dashMovement()
    {
        canDash = false;
        //make player super fast for a time
        PC.moveSpeed = PC.moveSpeed + dashPower;
        Debug.Log("am fast");
        PC.moving();
        yield return new WaitForSeconds(dashTime);
        Debug.Log(PC.moveSpeed);
        //reset player speed
        Debug.Log("am normal speed");
        PC.moveSpeed = PC.baseSpeed;
        Debug.Log(PC.moveSpeed);
        //cooldown
        Debug.Log("gotta wait");
        yield return new WaitForSeconds(dashCooldownTime);
        Debug.Log("done waiting");
        canDash = true;
        StopCoroutine(dashRoutine);
    }

    public void LevelUpDash()
    {
        //initial activation
        if (!active)
        {
            active = true;
        }
        //progressive dash speed per level of upgrade
        dLevel += 1;
        if(dLevel >= dashLevelMax)
        {
            dLevel = dashLevelMax;
        }
        dashPower = startupPower + (dashPowerScaling * dLevel);
    }
    private void OnDestroy()
    {
        dashAction.performed -= dashActionPerformed;
    }
}


