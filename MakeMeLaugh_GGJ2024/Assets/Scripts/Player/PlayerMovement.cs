using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cc;
    public float speed;

    float x, y;
    Vector3 dir;

    void Start()
    {
        if (cc == null) GetComponent<CharacterController>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        dir = new Vector3(x, 0, y).normalized;
    }

    private void FixedUpdate()
    {
        cc.Move(dir * Time.fixedDeltaTime * speed);
    }
}
