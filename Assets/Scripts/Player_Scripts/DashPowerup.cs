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
    public AudioClip dashSound;
    private bool wasMovingBeforeDash;
    [SerializeField] private GameObject dashTrailsObj;
    private TrailRenderer[] dashTrails;
    void Start()
    {
        canDash = true;
        active = false;
        dLevel = 0;
        dashPower = 0;
        dashAction = InputSystem.actions.FindAction(playerDash);
        dashAction.performed += dashActionPerformed;
        dashTrails = dashTrailsObj.GetComponentsInChildren<TrailRenderer>();
    }
   
    private void dashActionPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("try to dash.");
        if (active == true && canDash == true)
        {
            Debug.Log("Dash start");
            AudioSource.PlayClipAtPoint(dashSound, transform.position);
            dashRoutine = StartCoroutine(dashMovement());
        }
    }

    private IEnumerator dashMovement()
    {
        canDash = false;

        // 1. Check if the player is actually pressing a direction when they start the dash
        wasMovingBeforeDash = (PC.moveDirection != Vector2.zero);

        PC.moveSpeed = PC.moveSpeed + dashPower;
        Debug.Log("am fast");

        PC.moving();
        UpdateDashTrails(true);
        yield return new WaitForSeconds(dashTime);
        endDash();
    }
    private void endDash()
    {
        Debug.Log(PC.moveSpeed);

        PC.moveSpeed = PC.baseSpeed;
        Debug.Log("am normal speed");

        bool isTouchingControls = (Keyboard.current != null && Keyboard.current.anyKey.isPressed) ||
                                  (Gamepad.current != null && Gamepad.current.leftStick.ReadValue().magnitude > 0.15f);

        if (isTouchingControls)
        {
            PC.moving();
        }
        else
        {
            PC.moveDirection = Vector2.zero;
            PC.playerMovement = Vector2.zero;
            Debug.Log("Controls empty. Character stopped completely.");
        }

        Debug.Log(PC.moveSpeed);
        StartCoroutine(restartDash());
    }
    private IEnumerator restartDash()
    {
        yield return new WaitForSeconds(dashCooldownTime);
        Debug.Log("done waiting");
        canDash = true;
        UpdateDashTrails(false);
        dashRoutine = null;
    }
    private void UpdateDashTrails(bool isEmitting)
    {
        foreach (TrailRenderer trail in dashTrails)
        {
            trail.emitting = isEmitting;
        }   
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


