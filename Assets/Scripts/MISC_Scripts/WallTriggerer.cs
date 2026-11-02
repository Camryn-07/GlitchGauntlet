using UnityEngine;

public class WallTriggerer : MonoBehaviour
{
    [SerializeField] private GameObject wallToSummon;
    public bool hasSummoned;
    [SerializeField] private Vector2 wallCoordinates;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasSummoned = false;
    }

    public void summonWall(){
        Instantiate(wallToSummon, wallCoordinates, transform.rotation);
        hasSummoned = true;
    }
        
}
