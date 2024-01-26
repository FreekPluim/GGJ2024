using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    public Queue<Puzzle> puzzleQueue = new Queue<Puzzle>();
    public int OneStarScore;
    public int TwoStarScore;
    public int ThreeStarScore;
}
