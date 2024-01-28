using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public GameObject uiButtons, uiTimer, ResultUI;

    public Sprite emptyStar, fullStar;
    public TextMeshProUGUI scoreText;
    public GameObject starHolder;

    private void Start()
    {
        InMenu = true;
        if (instance != null) Destroy(this);
        else instance = this;

        GameTimer = MaxTime;
    }

    public void OnStartPressed()
    {
        set = false;
        GameTimer = MaxTime;
        for (int i = 0; i < starHolder.transform.childCount; i++)
        {
            starHolder.transform.GetChild(i).GetComponent<Image>().sprite = emptyStar;
        }
        points = 0;
        PlayerMovement.instance.GameStarted = true;
        uiButtons.SetActive(false);
        uiTimer.SetActive(true);
        ResultUI.SetActive(false);
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
    bool set = false;
    private void Update()
    {
        GameTimerText.text = $"{GameTimer / 60}:{GameTimer % 60}";

        if (GameTimer <= 0)
        {
            InMenu = true;
            anim.SetBool("GameStart", false);
            PlayerMovement.instance.GameStarted = false;
            scoreText.text = points.ToString();

            if (!set)
            {
                PuzzleManager.instance.puzzleFinish?.Invoke();
                if(points > levelData.ThreeStarScore)
                {
                    for (int i = 0; i < starHolder.transform.childCount; i++)
                    {
                        starHolder.transform.GetChild(i).GetComponent<Image>().sprite = fullStar;
                    }
                } 
                else if (points  > levelData.TwoStarScore)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        starHolder.transform.GetChild(i).GetComponent<Image>().sprite = fullStar;
                    }
                }
                else if (points > levelData.OneStarScore)
                {
                    starHolder.transform.GetChild(0).GetComponent<Image>().sprite = fullStar;

                }
                else
                {
                    for (int i = 0; i < starHolder.transform.childCount; i++)
                    {
                        starHolder.transform.GetChild(i).GetComponent<Image>().sprite = emptyStar;
                    }
                }
                set = true;
                uiTimer.SetActive(false);
                ResultUI.SetActive(true);
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
