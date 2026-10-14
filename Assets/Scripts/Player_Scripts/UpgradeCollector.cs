using UnityEngine;

public class UpgradeCollector : MonoBehaviour
{
    public DashPowerup dashPU;
    public ProjectilePowerup projectilePU;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DashPowerUp"))
        {
            Debug.Log("touched dash powerup!");
            //add 1 to the DashPowerup's level
            dashPU.LevelUpDash();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("ProjectilePowerUp"))
        {
            Debug.Log("touched Projectile powerup!");
            // add 1 to the Projectile Powerup's level
            projectilePU.LevelUpProjectile();
            Destroy(collision.gameObject);
        }
    }
}


