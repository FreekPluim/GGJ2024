using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PuzzleHolder : MonoBehaviour
{
    public bool playerActive = false;

    public GameObject stageCamera;
    public GameObject puzzleCamera;
    public GameObject puzzleObject;
    public Puzzle puzzle;

    [Header("Behaviours")]
    public JuggleBehaviour JuggleBehaviour;
    public FartBehaviour FartBehaviour;
    public BallanceBehaviour BallanceBehaviour;

    public Transform mid;
    public AudioSource source;

    public void SetPuzzle(Puzzle puzzle)
    {
        this.puzzle = puzzle;
    }
    private void Update()
    {
        if(puzzle != null && puzzleObject.activeSelf == false)
        {
            puzzleObject.SetActive(true);
        }
        else if(puzzle == null && puzzleObject.activeSelf == true)
        {
            puzzleObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(puzzle != null)
        {
            if (other.CompareTag("Player"))
            {
                puzzleCamera.SetActive(true);
                stageCamera.SetActive(false);
                PlayerMovement.instance.inPuzzle = true;
                other.transform.position = mid.position;
                other.transform.GetChild(0).rotation = Quaternion.Euler(0, 90, 0);
                StartCoroutine(TimerToStart());
            }
        }
    }
    IEnumerator TimerToStart()
    {
        yield return new WaitForSeconds(0.5f);
        playerActive = true;

        if (puzzle is JuggleSO)
        {
            JuggleBehaviour.enabled = true;
            JuggleBehaviour.jug = puzzle as JuggleSO;
        }
        if (puzzle is FartSO)
        {
            FartBehaviour.enabled = true;
            FartBehaviour.puzzle = puzzle as FartSO;
        }
        if (puzzle is BallanceSO)
        {
            BallanceBehaviour.enabled = true;
            BallanceBehaviour.puzzle = puzzle as BallanceSO;
        }

        PuzzleManager.instance.puzzleFinish.AddListener(FinishPuzzle);
    }
    public void FinishPuzzle()
    {
        puzzle = null;
        PlayerMovement.instance.inPuzzle = false;
        PlayerMovement.instance.anim.SetInteger("miniGame", 0);
        playerActive = false;
        stageCamera.SetActive(true);
        puzzleCamera.SetActive(false);
        puzzleObject.SetActive(false);

        if (JuggleBehaviour.enabled) JuggleBehaviour.enabled = false;
        if (FartBehaviour.enabled) FartBehaviour.enabled = false;
        if (BallanceBehaviour.enabled) BallanceBehaviour.enabled = false;

        PuzzleManager.instance.puzzleFinish.RemoveListener(FinishPuzzle);
    }
}

