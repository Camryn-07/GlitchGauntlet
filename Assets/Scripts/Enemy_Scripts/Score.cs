using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    public int scoreOnHit;
    public GameObject Player1;
    public GameObject Player2;
    public TMP_Text P1ScoreText;
    public TMP_Text P2ScoreText;
    //public class
    void Start()
    {
       
    }

    public void P1ScoreCounter()
    {
       // if ()
        //{
          //P1ScoreText.text = "Player1:" + score;
        //}
        
    }
    public void P2ScoreCounter()
    {
        P2ScoreText.text = "Player2:" + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
