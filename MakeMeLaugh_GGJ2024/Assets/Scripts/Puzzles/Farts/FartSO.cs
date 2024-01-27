using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fart", menuName = "Puzzles/Fart")]
public class FartSO : Puzzle
{
    public int decreasePerFrame = 1;
    public int increasePerSpace = 30;
    public float timeToCompleteInSeconds = 5;
    public int max = 1000;

    public void update(PuzzleHolder holder)
    {
        if (holder.current < 0) holder.current = 0;
        if (holder.current > 0)
        {
            holder.current -= decreasePerFrame;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            holder.current += increasePerSpace;
        }
    }
}
