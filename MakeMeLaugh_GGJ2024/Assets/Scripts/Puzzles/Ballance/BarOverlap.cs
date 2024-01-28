using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarOverlap : MonoBehaviour
{
    public bool overlapping;

    private void OnTriggerEnter(Collider other)
    {
        overlapping = true;
    }

    private void OnTriggerExit(Collider other)
    {
        overlapping = false;

    }
}
