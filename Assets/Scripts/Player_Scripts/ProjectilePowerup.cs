using UnityEngine;
using TMPro;
public class ProjectilePowerup : MonoBehaviour
{
    public bool active;
    public int pLevel;
    [SerializeField] private int projLevelMax;
    [SerializeField] private float projPowerMult;
    [SerializeField] private float projPowerBase;
    [SerializeField] private GameObject shootAbilityIcon;
    [SerializeField] private TMP_Text projLevelUpText;
    public float projPower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootAbilityIcon.SetActive(false);
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
            shootAbilityIcon.SetActive(true);
            active = true;
        }
        //progressive projectile damage per level of upgrade
        pLevel += 1;
        if(pLevel >= projLevelMax)
        {
            pLevel = projLevelMax;
        }
        projLevelUpText.text = pLevel + " ";
        projPower = projPowerBase * (1 + (projPowerMult * pLevel));
    }
}