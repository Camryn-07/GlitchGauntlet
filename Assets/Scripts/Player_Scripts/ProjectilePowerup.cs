using UnityEngine;

public class ProjectilePowerup : MonoBehaviour
{
    public bool active;
    public int pLevel;
    [SerializeField] private int projLevelMax;
    [SerializeField] private float projPowerMult;
    [SerializeField] private float projPowerBase;
    public float projPower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelUpProjectile()
    {
        //initial activation
        if (!active)
        {
            active = true;
        }
        //progressive projectile damage per level of upgrade
        pLevel += 1;
        if(pLevel >= projLevelMax)
        {
            pLevel = projLevelMax;
        }
        projPower = projPowerBase * (1 + (projPowerMult * pLevel));
    }
}