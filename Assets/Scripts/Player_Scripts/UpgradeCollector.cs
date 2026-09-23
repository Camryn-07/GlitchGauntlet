using UnityEngine;

public class UpgradeCollector : MonoBehaviour
{
    public DashPowerup dashPU;
    public ProjectilePowerup projectilePU;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DashPowerUp"))
        {
            //add 1 to the DashPowerup's value
            //dashPU.dLevel++;
            dashPU.levelUpDash();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("ProjectilePowerUp"))
        {
            // add 1 to the Projectile Powerup's value
            //projectilePU.pLevel;
            //projectilePU.levelUpProjectile();
            Destroy(collision.gameObject);
        }
    }
}
