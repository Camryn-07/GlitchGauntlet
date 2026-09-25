using System.Collections;
using TMPro;
using UnityEngine;
public class CountDown : MonoBehaviour
{
    public float countdown;
    public TMP_Text countdownText;
    public GameObject Boss;
    public bool bossSpawn;
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
        }
        if (countdown == 10)
        {
            SpawnBoss();
        }
        StartCoroutine(Timer());
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
