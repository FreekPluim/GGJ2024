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

    public void fixedupdate(FartBehaviour behaviour)
    {
        if (behaviour.current < 0) behaviour.current = 0;
        if (behaviour.current > 0)
        {
            behaviour.current -= decreasePerFrame;
        }
    }

    public void update(FartBehaviour behaviour)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            behaviour.current += increasePerSpace;
        }
    }
}
