using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelData levelData;

    public TextMeshProUGUI GameTimerText;

    public int points;
    public int MaxTime;
    public int GameTimer;

    public int SuccesfullMinigames;
    public int succesAddition = 10;

    public Animator anim;

    [Header("Audio")]
    public AudioSource audienceSource;
    public AudioClip laughter;
    public AudioSource backgroundMusicSource;
    public AudioClip GameplayMusic;
    public AudioClip backgoundMusic;

    public bool InMenu = false;
    bool current = false;

    public GameObject uiButtons, uiTimer;

    private void Start()
    {
        InMenu = true;
        if (instance != null) Destroy(this);
        else instance = this;

        GameTimer = MaxTime;
    }

    public void OnStartPressed()
    {
        PlayerMovement.instance.GameStarted = true;
        uiButtons.SetActive(false);
        uiTimer.SetActive(true);
        anim.SetBool("GameStart", true);
        InMenu = false;
    }
    public void OnQuit()
    {
        Application.Quit();
    }

    public void PlayLaughter()
    {
        audienceSource.PlayOneShot(laughter);
    }
    void PlayBackground()
    {
        backgroundMusicSource.Stop();
        backgroundMusicSource.clip = backgoundMusic;
        backgroundMusicSource.Play();
    }
    void PlayGameplay()
    {
        backgroundMusicSource.Stop();
        backgroundMusicSource.clip = GameplayMusic;
        backgroundMusicSource.Play();
    }
    private void Update()
    {
        GameTimerText.text = $"{GameTimer / 60}:{GameTimer % 60}";

        if (GameTimer <= 0)
        {
            //Stop game;
            InMenu = true;

            //TODO: Handle UI Bullshit
            if(points + (succesAddition * SuccesfullMinigames) > levelData.ThreeStarScore)
            {

            } 
            else if (points + (succesAddition * SuccesfullMinigames) > levelData.TwoStarScore)
            {

            }
            else if (points + (succesAddition * SuccesfullMinigames) > levelData.OneStarScore)
            {

            }
            else
            {
                //FAILURE;
            }
        }

        if(InMenu == false && InMenu != current)
        {
            StartCoroutine(timerCo());
            PlayGameplay();
            current = InMenu;
        }
        if(InMenu == true && InMenu != current)
        {
            PlayBackground();
            current = InMenu;
        }
    }

    IEnumerator timerCo()
    {
        for (int i = 0; i < MaxTime; i++)
        {
            GameTimer--;
            yield return new WaitForSeconds(1);
        }
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }
    public void Success()
    {
        SuccesfullMinigames++;
        PlayLaughter();
    }

}
