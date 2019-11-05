using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{

    [SerializeField]
    Image topCurtain, bottomCurtain, blackCurtain;
    [SerializeField]
    Text stageNumberText, gameOverText;
    [SerializeField]
    RectTransform canvas;
    GameObject[] spawnPoints, spawnPlayerPoints;
    bool stageStart = false;
    bool tankReserveEmpty = false;

    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            blackCurtain.rectTransform.localScale = new Vector3(blackCurtain.rectTransform.localScale.x, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), blackCurtain.rectTransform.localScale.z);
            yield return null;
        }
    }

    IEnumerator RevealTopStage()
    {
        float moveTopUpMin = topCurtain.rectTransform.position.y + (canvas.rect.height / 2) + 10;
        stageNumberText.enabled = false;
        while (topCurtain.rectTransform.position.y < moveTopUpMin)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    IEnumerator RevealBottomStage()
    {
        float moveBottomDownMin = bottomCurtain.rectTransform.position.y - (canvas.rect.height / 2) - 10;
        while (bottomCurtain.rectTransform.position.y > moveBottomDownMin)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
    }

    public IEnumerator GameOver()
    {
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(gameOverText.rectTransform.localPosition.x, gameOverText.rectTransform.localPosition.y + 40f * Time.deltaTime, gameOverText.rectTransform.localPosition.z);
            yield return null;
        }
        MasterTracker.stageCleared = false;
        LevelCompleted();
    }
    // Start is called before the first frame update
    void Start()
    {
        stageNumberText.text = "STAGE " + MasterTracker.stageNumber.ToString();
        stageStart = true;
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        spawnPlayerPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        StartCoroutine(StartStage());
        //StartCoroutine(GameOver());
    }

    IEnumerator StartStage()
    {
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(3);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        yield return null;
        InvokeRepeating("SpawnEnemy", LevelManager.spawnRate, LevelManager.spawnRate);
        SpawnPlayer();
    }

    // Update is called once per frame
    private void Update()
    {
        if (tankReserveEmpty && GameObject.FindWithTag("Small") == null && GameObject.FindWithTag("Fast") == null && GameObject.FindWithTag("Big") == null && GameObject.FindWithTag("Armored") == null)
        {
            MasterTracker.stageCleared = true;
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        tankReserveEmpty = false;
        SceneManager.LoadScene("Score");
    }

    void SpawnEnemy()
    {
        if (LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks > 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Animator anime = spawnPoints[spawnPointIndex].GetComponent<Animator>();
            anime.SetTrigger("Spawning");
        }
        else
        {
            CancelInvoke();
            tankReserveEmpty = true;
        }
    }

    public void SpawnPlayer()
    {
        if (MasterTracker.playerLives > 0)
        {
            if (!stageStart)
            {
                MasterTracker.playerLives--;
            }
            stageStart = false;
            Animator anime = spawnPlayerPoints[0].GetComponent<Animator>();
            anime.SetTrigger("Spawning");
        }
        else
        {
            StartCoroutine(GameOver());
       }
    }
}
