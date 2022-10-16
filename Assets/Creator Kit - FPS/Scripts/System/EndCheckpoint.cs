using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheckpoint : MonoBehaviour
{
    public Controller hpa;
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Controller>() == null)
            return;
        
        
        GameSystem.Instance.StopTimer();
        GameSystem.Instance.FinishRun();
        Destroy(gameObject);
    }
}
