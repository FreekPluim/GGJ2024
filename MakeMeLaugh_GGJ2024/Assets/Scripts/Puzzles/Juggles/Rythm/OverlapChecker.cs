using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapChecker : MonoBehaviour
{
    public KeyMove keyOverlap;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        if(other.TryGetComponent<KeyMove>(out KeyMove key))
        {
            keyOverlap = key;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        keyOverlap = null;
    }
}
