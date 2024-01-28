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

    private void Start()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        GameTimer = MaxTime;
        StartCoroutine(timerCo());
    }

    private void Update()
    {
        GameTimerText.text = $"{GameTimer / 60}:{GameTimer % 60}";

        if (GameTimer <= 0)
        {
            //Stop game;

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
    }

}
