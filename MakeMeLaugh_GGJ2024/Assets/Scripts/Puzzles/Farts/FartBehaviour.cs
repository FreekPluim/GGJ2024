using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FartBehaviour : MonoBehaviour
{
    [Header("Fart")]
    public GameObject FartUI;
    public Slider slider;
    public int current;
    public FartSO puzzle;
    public PuzzleHolder holder;

    public TextMeshProUGUI timerUI;
    Coroutine TimerCo;
    public int maxTime = 10;
    int time;

    public AudioClip fart;

    private void OnEnable()
    {
        current = 0;
        FartUI.SetActive(true);
        PlayerMovement.instance.anim.SetInteger("miniGame", 1);
        time = maxTime;
        TimerCo = StartCoroutine(timerCo());
        PuzzleManager.instance.puzzleFinish.AddListener(OnFinish);
    }
     
    private void Update()
    {
        puzzle.update(this);
        slider.value = current;
        slider.maxValue = puzzle.max;
        timerUI.text = time.ToString();

        if (current >= puzzle.max)
        {
            //TODO: Play reverb fart sound
            holder.source.PlayOneShot(fart);
            GameManager.instance.AddPoints(puzzle.scoreOnSuccess);
            GameManager.instance.Success();
            PuzzleManager.instance.FinishedPuzzle(holder);
        }

        if(time <= 0)
        {
            PuzzleManager.instance.FinishedPuzzle(holder);
        }
    }

    void OnFinish()
    {
        current = 0;
        FartUI.SetActive(false);
        StopCoroutine(TimerCo);
        PuzzleManager.instance.puzzleFinish.RemoveListener(OnFinish);
    }

    private void FixedUpdate()
    {
        puzzle.fixedupdate(this);
    }

    IEnumerator timerCo()
    {
        for (int i = 0; i < maxTime; i++)
        {
            time--;
            yield return new WaitForSeconds(1);
        }
    }
}
