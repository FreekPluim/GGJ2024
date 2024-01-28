using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public CharacterController cc;
    public float speed;
    public Transform visuals;
    public bool inPuzzle;
    public bool GameStarted;
    public Animator anim;

    float x, y;
    Vector3 lookDir;

    void Start()
    {
        if (instance != null) { Destroy(this); }
        else instance = this;

        if (cc == null) GetComponent<CharacterController>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        lookDir = new Vector3(-y, 0, x).normalized;
    }

    private void FixedUpdate()
    {
        if (GameStarted)
        {
            if (!inPuzzle)
            {
                cc.Move(lookDir * Time.fixedDeltaTime * speed);

                if(x != 0 || y != 0)
                {
                    anim.SetBool("isWalking", true);
                    visuals.transform.rotation = Quaternion.LookRotation(lookDir);
                }
                else
                {
                    anim.SetBool("isWalking", false);
                }
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }
    }
}
