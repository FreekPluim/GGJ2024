using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallanceBehaviour : MonoBehaviour
{
    [Header("Important variables")]
    public float barSpeed = 8;
    public float targetSpeed = 6;
    public float progressionSpeed = 2.5f;

    #region variables
    [Header("Ballance")]
    public GameObject BallanceUI;
    public BallanceSO puzzle;
    public PuzzleHolder holder;
    public RectTransform playerBar;
    public Transform target;
    public Slider ballance_Progression;
    public int charge;
    int min = -330;
    int max = 330;
    public int maxTime;
    int time;
    public TextMeshProUGUI timer;
    public BarOverlap barOverlap;

    Vector3 target_targetPosition;
    Coroutine targetPos, TimerCo;
    #endregion

    void OnEnable()
    {
        BallanceUI.SetActive(true);
        time = maxTime;
        TimerCo = StartCoroutine(timerCo());
        targetPos = StartCoroutine(targetChangePos());
        PlayerMovement.instance.anim.SetInteger("miniGame", 3);
    }
    bool updown;

    void Update()
    {
        if (ballance_Progression.value >= ballance_Progression.maxValue - 1)
        {
            GameManager.instance.AddPoints(puzzle.scoreOnSuccess);
            GameManager.instance.Success();
            PuzzleManager.instance.FinishedPuzzle(holder);
            ballance_Progression.value = 0;
            BalanceFinish();
        }
        if (time == 0)
        {
            //Failed
            PuzzleManager.instance.FinishedPuzzle(holder);
            holder.source.PlayOneShot(holder.failSound);
            ballance_Progression.value = 0;
            BalanceFinish();
        }

        timer.text = time.ToString();
        
        if (Input.GetKey(KeyCode.Space))
        {
            updown = true;
        }
        else
        {
            updown = false;
        }
        
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (playerBar.localPosition.y < max)
            {
                playerBar.transform.localPosition += new Vector3(0, barSpeed, 0);
            }
        }
        else
        {
            if (playerBar.localPosition.y > min)
            {
                playerBar.transform.localPosition -= new Vector3(0, barSpeed, 0);
            }
        }

        if (target.localPosition != target_targetPosition)
        {
            if (target_targetPosition.y > target.localPosition.y)
            {
                target.localPosition += new Vector3(0, targetSpeed, 0);
            }
            if (target_targetPosition.y < target.localPosition.y)
            {
                target.localPosition += new Vector3(0, -targetSpeed, 0);
            }
        }

        if (barOverlap.overlapping) ballance_Progression.value += 3;
        else { ballance_Progression.value -= 3; }
    }

    void BalanceFinish()
    {
        StopCoroutine(targetPos);
        StopCoroutine(TimerCo);
        BallanceUI.SetActive(false);
        time = maxTime;
    }
    IEnumerator targetChangePos()
    {
        int pos = Random.Range(min, max);
        target_targetPosition = new Vector3(0, pos, 0);
        yield return new WaitForSeconds(2f);
        targetPos = StartCoroutine(targetChangePos());
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
