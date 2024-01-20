using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool objectPool;
    public CardController cardController;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public float spawnInterval = 1f;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI wave1Text;
    public TextMeshProUGUI wave2Text;
    public TextMeshProUGUI wave3Text;
    public TextMeshProUGUI wave4Text;

    public AudioSource countdownAS;
    public AudioSource waveAS;
    public AudioClip countdownAC;
    public AudioClip waveAC;

    public float spawnDuration = 10f;
    public float countDownDuration = 3f;

    public TextMeshProUGUI killedEnemiesText;
    private int killedEnemiesCount = 0;

    public int currentWave = 1;
    public int totalWaves = 4; // Toplam ka� wave olacaksa buray� ayarlay�n
    private bool isGameOver = false;

    private void Start()
    {
        StartCoroutine(StartCountdown());
        objectPool = FindObjectOfType<ObjectPool>();
        wave1Text.gameObject.SetActive(false);
        wave2Text.gameObject.SetActive(false);
        wave3Text.gameObject.SetActive(false);
        wave4Text.gameObject.SetActive(false);
        killedEnemiesText.text = "Killed Enemies: 0";
    }


    IEnumerator StartCountdown()
    {
        int countdown = 3;

        while (countdown > 0)
        {
            if (countdown == 3)
            {
                countdownAS.PlayOneShot(countdownAC);
            }

            countDownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countDownText.text = "Go!";
        yield return new WaitForSeconds(1f);


        countDownText.gameObject.SetActive(false);

        // wave1Text.gameObject.SetActive(true);
        SetWaveTextActive(currentWave);
        yield return new WaitForSeconds(1f);
        //waveText.gameObject.SetActive(false);

        StopCoroutine(SpawnEnemies());

        StartCoroutine(SpawnEnemies());
    }

    

    IEnumerator SpawnEnemies()
    {
        

        float elapsedTime = 0f;

        while (elapsedTime < spawnDuration)
        {
            xPos = Random.Range(-5, 5);
            zPos = Random.Range(-23, 10);
            GameObject enemy = objectPool.GetPooledEnemy();
            enemy.SetActive(true);
            if (enemy != null)
            {
                enemy.transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-23, 10));
            }
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
            enemyCount += 1;
        }

        CheckAllEnemiesDead();
        //StopCoroutine(SpawnEnemies());
    }

    private void CheckAllEnemiesDead()
    {
        StartCoroutine(CheckEnemiesCoroutine());
    }

    private IEnumerator CheckEnemiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

            if (enemies.Length == 0)
            {
                OnAllEnemiesDead();
                yield break;
            }
        }
    }
    public void NotifyEnemyKilled()
    {
        // Bu metod, d��man�n �ld��� bilgisini al�r ve sayac� artt�r�r.
        killedEnemiesCount++;

        // �ld�r�len d��man say�s�n� g�ncelleyip g�r�nt�ler.
        killedEnemiesText.text = "Killed Enemies: " + killedEnemiesCount;
    }

    private void OnAllEnemiesDead()
    {
        Debug.Log("Tum dusmanlar oldu.");
        waveAS.Play();
        cardController.cardPanel.gameObject.SetActive(true);
        killedEnemiesText.text = "Killed Enemies: " + (++killedEnemiesCount);

        if (currentWave < totalWaves)
        {
            StartCoroutine(StartNextWave());
        }
        else
        {
            // Toplam dalga say�s�na ula��ld���nda oyunu tamamlanm�� kabul edebilirsiniz.
            Debug.Log("Oyun tamamland�.");
            if (!isGameOver)
            {
                cardController.cardPanel.gameObject.SetActive(false);
                isGameOver = true;
                Time.timeScale = 0;
            }
            
        }
    }

    private IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(3f);

        // Mevcut dalga say�s�n� bir artt�r
        currentWave++;


        yield return new WaitForSeconds(1f);
        

        SetWaveTextActive(currentWave);
        // wave2Text.gameObject.SetActive(true);


        // Yeni wave ba�lad���nda d��manlar� spawn et
        //StartCoroutine(SpawnEnemies());
        if (currentWave <= totalWaves)
        {
            // Oyun devam ediyorsa yeni wave i�in d��manlar� spawn et
            StartCoroutine(SpawnEnemies());
        }
        else if (currentWave == totalWaves + 1)
        {
            // Oyun tamamland���nda burada ek i�lemler yapabilirsiniz.
            // �rne�in, Game Over ekran�n� a�abilirsiniz.
            cardController.cardPanel.SetActive(false);
            isGameOver = true;
            Time.timeScale = 0;
        }
    }

    private void SetWaveTextActive(int waveNumber)
    {
        
        wave1Text.gameObject.SetActive(waveNumber == 1);
        wave2Text.gameObject.SetActive(waveNumber == 2);
        wave3Text.gameObject.SetActive(waveNumber == 3);
        wave4Text.gameObject.SetActive(waveNumber == 4);

        wave1Text.gameObject.SetActive(false);
        wave2Text.gameObject.SetActive(false);
        wave3Text.gameObject.SetActive(false);
        wave4Text.gameObject.SetActive(false);

        // Ard�ndan sadece ilgili wave text'i aktif edilir
        switch (waveNumber)
        {
            case 1:
                wave1Text.gameObject.SetActive(true);
                break;
            case 2:
                wave2Text.gameObject.SetActive(true);
                break;
            case 3:
                wave3Text.gameObject.SetActive(true);
                break;
            case 4:
                wave4Text.gameObject.SetActive(true);
               
                break;
            default:
                break;
        }


    }
}
