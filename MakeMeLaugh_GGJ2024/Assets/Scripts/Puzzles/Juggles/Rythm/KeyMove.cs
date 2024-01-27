using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMove : MonoBehaviour
{
    public KeyCode key;
    public int speed = 3;
    public bool PlayerPressed = false;
    public PuzzleHolder holder;

    bool added = false;

    // Update is called once per frame
    void Update()
    {
        float step = (speed * 100) * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(-1200, 0, 0), step);

        if (transform.localPosition.x <= -650)
        {
            if(PlayerPressed == false && !added)
            {
                holder.missedInputs++;
                added = true;
            }
        }
        // If the object has arrived, stop the coroutine
        if (transform.localPosition.x <= -1200)
        {
            Destroy(this.gameObject);
        }
    }
}
