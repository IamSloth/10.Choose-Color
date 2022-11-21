using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    
    [Header("Time")] 
    public static float maxTime = 60f;
    public static float playTime;
    public static float maxComboTime = 3f;
    public static float playComboTime;
    public static bool isGameOver = true;
    
    [Header("Player")]
    public static float score;
    public static float hit;
    public static float combo;

    [Header("UI")]
    public GameObject uiInfo;
    public GameObject uiSelect;
    public GameObject uiGameover;
    public GameObject uiGameStart;
    public GameObject uiNewRecode;

    [Header("Effect")]
    public ParticleSystem myParticle;
    public Animator myAnimator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Init();
    }

    void Init()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetFloat("Score", 0f);
        }
        instance = this;
        playTime = 0;
        playComboTime = 0;
        score = 0;
        combo = 0;
        
    }

    public static void Success()
    {
        hit++;
        combo++;
        score += 1 + (combo * 0.1f);
        playComboTime = 0;
        instance.myParticle.Play();
        instance.myAnimator.SetTrigger("Hit");
        SoundManager.PlaySound("Hit");
    }

    public static void Fail()
    {
        combo = 0;
        playTime += 10;
        SoundManager.PlaySound("Fail");
    }

    IEnumerator ComboTime()
    {
        while (!isGameOver)
        {
            yield return null;
            playComboTime += Time.deltaTime;

            if (playComboTime > maxComboTime)
            {
                combo = 0;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;

        GameTimer();
    }

    void GameTimer()
    {
        playTime += Time.deltaTime;
        if (playTime > maxTime)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        uiInfo.SetActive(false);
        uiSelect.SetActive(false);
        uiGameover.SetActive(true);
        SoundManager.PlaySound("Over");
        SoundManager.BgmStop();

        float maxScore = PlayerPrefs.GetFloat("Score");
        if (score > maxScore)
        {
            PlayerPrefs.SetFloat("Score", score);
            uiNewRecode.SetActive(true);
        }
    }

    public void GameStart()
    {
        isGameOver = false;
        uiInfo.SetActive(true);
        uiSelect.SetActive(true);
        uiGameStart.SetActive(false);
        instance.StartCoroutine(instance.ComboTime());
        SoundManager.PlaySound("Start");
        SoundManager.BgmStart();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }
}
