using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cc;
    public float speed;
    public Transform visuals;

    float x, y;
    Vector3 lookDir;

    void Start()
    {
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
        cc.Move(lookDir * Time.fixedDeltaTime * speed);

        if(x != 0 || y != 0)
        {
            visuals.transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
