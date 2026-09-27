using System.Collections;
using TMPro;
using UnityEngine;
public class CountDown : MonoBehaviour
{
    public float countdown;
    public TMP_Text countdownText;
    public GameObject Boss;
    public bool bossSpawn;
    public int p1score;
    public int p2score;
    public GameObject p1Wins;
    public GameObject p2Wins;
    public GameObject tie;
    void Start()
    {
        Score.instance.scoreP1 = p1score;
        Score.instance.scoreP2 = p2score;
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
                if (p1score> p2score)
                {                    
                    p1Wins.SetActive(true);
                }
                else if (p1score <= p2score) 
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
}
