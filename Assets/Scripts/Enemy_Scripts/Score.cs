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
    public GameObject controlsP1;
    public GameObject controlsP2;
    public GameObject quit;
    public GameObject resrart;
    public Camera player1Camera;
    public Camera player2Camera;
    public Camera bossCamera;
    public GameObject bossWall;
    private bool cutsceneTriggered = false;

    //public class
    void Awake()
    {
       instance = this;
    }
    void Start()
    {
        if (bossCamera != null)
        {
            bossCamera.gameObject.SetActive(false);
        }

        StartCoroutine(Timer());
        bossSpawn = false;
    }
    IEnumerator Timer()
    {
        while (countdown > 0)
        {
            Time.timeScale = 1;
            yield return new WaitForSeconds(1f);
            countdown -= 1f;

            if (countdown == 30 && !cutsceneTriggered)
            {
                cutsceneTriggered = true;
                SpawnBoss();
                yield return StartCoroutine(ShowBoss());
            }
            if (countdown == 0)
            {
                Time.timeScale = 0f;
                controlsP1.SetActive(false);
                controlsP2.SetActive(false);
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
                quit.SetActive(true);
                resrart.SetActive(true);
            }
        }
    }
    void SpawnBoss()
    {
        Instantiate(Boss, transform.position, transform.rotation);
        bossSpawn = true;
    }
    IEnumerator ShowBoss()
    {
        bossWall.gameObject.SetActive(false);
        player1Camera.gameObject.SetActive(false);
        player2Camera.gameObject.SetActive(false);
        bossCamera.gameObject.SetActive(true);
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1f;
        bossCamera.gameObject.SetActive(false);
        player1Camera.gameObject.SetActive(true);
        player2Camera.gameObject.SetActive(true);
    }
    void Update()
    {
        countdownText.text = countdown.ToString();
    }
    public void ChangeP1Score(int change)
    {
        scoreP1 += change;
        scoreP1 = Mathf.Clamp(scoreP1 + change, 0, 99999999);
        P1ScoreText.text = "Player1:" + scoreP1;
    }
    public void ChangeP2Score(int change)
    {
        scoreP2 += change;
        scoreP2 = Mathf.Clamp(scoreP2 + change, 0, 99999999);
        P2ScoreText.text = "Player2:" + scoreP2;
    }
    private void Pause(bool pause)
    {
        
    }
}






