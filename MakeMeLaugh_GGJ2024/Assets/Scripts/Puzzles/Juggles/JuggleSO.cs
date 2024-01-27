using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Juggler", menuName ="Puzzles/Juggle")]
public class JuggleSO : Puzzle
{
    public bool TimingBased = false;

    public int amountOfInputs;
    public Queue<KeyCode> keyInputs = new Queue<KeyCode>();
    public List<KeyCode> keys = new List<KeyCode>()
    {
        KeyCode.Z,
        KeyCode.X,
        KeyCode.C
    };
    public int wrongInputs;


    public void update(PuzzleHolder holder)
    {
        if(keyInputs.Count <= 0)
        {
            holder.onJuggleFinish.Invoke();
            PuzzleManager.instance.FinishedPuzzle(holder);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckKey(holder, KeyCode.Z);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            CheckKey(holder, KeyCode.X);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CheckKey(holder, KeyCode.C);
        }
        else if (Input.anyKeyDown)
        {
            CheckKey(holder);
        }

        if(wrongInputs >= 3)
        {
            Debug.Log("Bad ending");
            holder.onJuggleFinish.Invoke();
            PuzzleManager.instance.FinishedPuzzle(holder);
        }
    }

    void CheckKey(PuzzleHolder holder, KeyCode input = KeyCode.None)
    {
        if (!TimingBased)
        {
            if (input == keyInputs.Peek())
            {
                Debug.Log("The right key!!");
                keyInputs.Dequeue();
            }
            else
            {
                //TODO: WRONG KEY FEEDBACK;
                Debug.Log("NOT THE RIGHT KEY");
                keyInputs.Dequeue();
            }
        
            Destroy(holder.JuggleKeysUI_Fast.GetChild(0).gameObject);
        }
        else
        {
            if(holder.overlap.keyOverlap != null)
            {
                holder.overlap.keyOverlap.holder = holder;
                if (input == holder.overlap.keyOverlap.key)
                {
                    holder.overlap.keyOverlap.PlayerPressed = true;
                    Debug.Log("The right key!!");
                }
                else
                {
                    //TODO: WRONG KEY FEEDBACK;
                    Debug.Log("NOT THE RIGHT KEY");
                }
            }
            else
            {
                wrongInputs++;
            }
        }
    }

    public void SetInputs()
    {
        for (int i = 0; i < amountOfInputs; i++)
        {
            keyInputs.Enqueue(keys[Random.Range(0, keys.Count)]);
        }
    }
}
