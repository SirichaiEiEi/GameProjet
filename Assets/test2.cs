using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    [SerializeField] private float Seeee = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Seeee);
    }

    // Update is called once per frame
}
