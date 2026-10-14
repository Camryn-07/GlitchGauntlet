using UnityEngine;

public class UpgradeCollector : MonoBehaviour
{
    public DashPowerup dashPU;
    public ProjectilePowerup projectilePU;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DashPowerUp"))
        {
            //add 1 to the DashPowerup's level
            dashPU.LevelUpDash();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("ProjectilePowerUp"))
        {
            // add 1 to the Projectile Powerup's level
            //projectilePU.levelUpProjectile();
            Destroy(collision.gameObject);
        }
    }
}


