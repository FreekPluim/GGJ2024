using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    public LevelData levelData;
    public List<PuzzleHolder> puzzleHolders = new List<PuzzleHolder>();
    Dictionary<PuzzleHolder, bool> usedHolders = new Dictionary<PuzzleHolder, bool>();
    public int score;

    public Coroutine Queue;

    [Header("Events")]
    public UnityEvent puzzleFinish;
    public UnityEvent levelFinish;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        foreach (var puzzle in puzzleHolders)
        {
            usedHolders.Add(puzzle, false);
        }
    }

    private void Start()
    {
        if(levelData != null)
        {
            Queue = StartCoroutine(LevelQueue());
        }
    }

    IEnumerator LevelQueue()
    {
        SpawnPuzzle();
        levelData.puzzleQueue.Dequeue();
        yield return new WaitForSeconds(Random.Range(8, 12));
        if(levelData.puzzleQueue.Count > 0)
        {
            Queue = StartCoroutine(LevelQueue());
        }
    }

    public void FinishedPuzzle(PuzzleHolder holder)
    {
        puzzleFinish.Invoke();
        usedHolders[holder] = false;
    }

    public void FinishedLevel()
    {
        StopCoroutine(Queue);
    }
    void SpawnPuzzle()
    {
        Puzzle puzzle = levelData.puzzleQueue.Peek();
        List<PuzzleHolder> temp = new List<PuzzleHolder>();
        foreach (var item in usedHolders)
        {
            if (!item.Value)
            {
                temp.Add(item.Key);
            }
        }
        int random = Random.Range(0, temp.Count);
        puzzleHolders[random].SetPuzzle(puzzle);
        usedHolders[puzzleHolders[random]] = true;
    }
}
