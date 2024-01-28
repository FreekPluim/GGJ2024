using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class LevelData : ScriptableObject
{
    public List<Puzzle> puzzlesInLevel = new List<Puzzle>();
    public int OneStarScore;
    public int TwoStarScore;
    public int ThreeStarScore;
}
