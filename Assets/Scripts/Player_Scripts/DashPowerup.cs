using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashPowerup : MonoBehaviour
{
    public bool active; 
    public int dLevel;
    public PlayerMovement PM;
    public float dashPower;
    public float dash1Power;
    public float dash2Power;
    public float dash3Power;
    private InputAction dashAction;
    private Coroutine dashRoutine;
    [SerializeField] private float dashTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        active = false;
        dashAction = InputSystem.actions.FindAction("Dash");
        dashAction.performed += dashActionPerformed;
    }

    private void dashActionPerformed(InputAction.CallbackContext obj)
    {
        if (active)
        {
            dashRoutine = StartCoroutine(dashMovement());
        }
    }

    private IEnumerator dashMovement()
    {
        //make player fast for a short time
        PM.speedAfterBoosts = PM.speedAfterBoosts + dashPower;
        yield return new WaitForSeconds(dashTime);
        PM.speedAfterBoosts = PM.baseSpeed;
        StopCoroutine(dashRoutine);
    }
    public void levelUpDash()
    {
        //activate ability if 
        if (dLevel <= 0)
        {
            active = true; 
        }
        dLevel++;
        if (dLevel == 1)
        {
            dashPower = dash1Power;
        }
        else if (dLevel == 2)
        {
            dashPower = dash2Power;
        }
        else if (dLevel == 3)
        {
            dashPower = dash3Power;
        }
    }
}
