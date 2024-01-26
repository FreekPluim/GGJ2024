using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHolder : MonoBehaviour
{
    public GameObject stageCamera;
    public GameObject puzzleCamera;
    public GameObject puzzleObject;
    public Puzzle puzzle;

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

    public void SetPuzzle(Puzzle puzzle)
    {
        this.puzzle = puzzle;
    }

    public void FinishPuzzle()
    {
        puzzle = null;
        stageCamera.SetActive(true);
        puzzleCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puzzleCamera.SetActive(true);
            stageCamera.SetActive(false);
        }
    }
}
