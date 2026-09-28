using TMPro;
using System.Collections;
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
    public float countdown;
    public TMP_Text countdownText;
    public GameObject Boss;
    public bool bossSpawn;
    public GameObject p1Wins;
    public GameObject p2Wins;
    public GameObject tie;
    //public class
    void Awake()
    {
       instance = this;
    }
    void Start()
    {
        StartCoroutine(Timer());
        bossSpawn = false;
    }
    IEnumerator Timer()
    {
        while (countdown > 0)
        {
            yield return new WaitForSeconds(1f);
            countdown -= 1f;

            if (countdown == 20)
            {
                SpawnBoss();
            }
            if (countdown == 0)
            {
                if (scoreP1 > scoreP2)
                {
                    p1Wins.SetActive(true);
                }
                else if (scoreP1 < scoreP2)
                {
                    p2Wins.SetActive(true);
                }
                else
                {
                    tie.SetActive(true);
                }
            }
        }
    }
    void SpawnBoss()
    {
        Instantiate(Boss, transform.position, transform.rotation);
        bossSpawn = true;
    }
    void Update()
    {
        countdownText.text = countdown.ToString();
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






