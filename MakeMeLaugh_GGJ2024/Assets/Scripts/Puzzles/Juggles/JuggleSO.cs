using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Juggler", menuName ="Puzzles/Juggle")]
public class JuggleSO : Puzzle
{
    public int amountOfInputs;
    public Queue<KeyCode> keyInputs = new Queue<KeyCode>();
    public List<KeyCode> keys = new List<KeyCode>()
    {
        KeyCode.Z,
        KeyCode.X,
        KeyCode.C
    };
    public int wrongInputs = 0;

    public void update(JuggleBehaviour behaviour, PuzzleHolder holder)
    {
        if(keyInputs.Count <= 0)
        {
            GameManager.instance.AddPoints(scoreOnSuccess);
            GameManager.instance.Success();
            PuzzleManager.instance.FinishedPuzzle(holder);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckKey(behaviour, KeyCode.Z);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            CheckKey(behaviour, KeyCode.X);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CheckKey(behaviour, KeyCode.C);
        }
        else if (Input.anyKeyDown)
        {
            CheckKey(behaviour);
        }

        if(wrongInputs >= 3)
        {
            wrongInputs = 0;
            holder.source.PlayOneShot(holder.failSound);
            PuzzleManager.instance.FinishedPuzzle(holder);
        }
    }

    void CheckKey(JuggleBehaviour behaviour, KeyCode input = KeyCode.None)
    {

        if (input == keyInputs.Peek())
        {
            keyInputs.Dequeue();
        }
        else
        {
            //TODO: WRONG KEY FEEDBACK;
            wrongInputs++;
            keyInputs.Dequeue();
        }
        
        Destroy(behaviour.JuggleKeysUI_Fast.GetChild(0).gameObject);
        
    }

    public void SetInputs()
    {
        for (int i = 0; i < amountOfInputs; i++)
        {
            keyInputs.Enqueue(keys[Random.Range(0, keys.Count)]);
        }
    }
}
