using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    public int scoreP1;
    public int scoreP2;
    public GameObject Player1;
    public GameObject Player2;
    public TMP_Text P1ScoreText;
    public TMP_Text P2ScoreText;
    //public class
    void Awake()
    {
       instance = this;
    }  
    public void ChangeP1Score(int change)
    {
        scoreP1 += change;

        P1ScoreText.text = "Player1:" + scoreP1;
    }
    public void ChangeP2Score(int change)
    {
        scoreP2 += change;

        P2ScoreText.text = "Player2:" + scoreP2;
    }
}
