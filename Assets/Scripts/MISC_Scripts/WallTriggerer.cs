using UnityEngine;

public class WallTriggerer : MonoBehaviour
{
    [SerializeField] private GameObject wallToSummon;
    public bool hasSummoned;
    [SerializeField] private Vector2 wallCoordinates;
    [SerializeField] private GameObject wallGraphic;
    [SerializeField] private float wallCDTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wallGraphic.SetActive(false);
        hasSummoned = false;
    }
    void Update(){
        if(wallCDTimer > 0){
            wallCDTimer -= Time.deltaTime;
            if(wallCDTimer <= 0){
                wallCDTimer = 0;
                wallGraphic.SetActive(false);
            }
        }
    }

    public void summonWall(){
        Instantiate(wallToSummon, wallCoordinates, transform.rotation);
        wallCDTimer = 15f;
        wallGraphic.SetActive(true);
        hasSummoned = true;
    }
        
}
