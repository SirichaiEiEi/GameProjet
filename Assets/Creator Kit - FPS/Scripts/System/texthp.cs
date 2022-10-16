using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class texthp : MonoBehaviour
{
    public Target hpal;
    public TextMeshProUGUI text2;

    // Start is called before the first frame update
    void Start()
    {
        text2 = GameObject.Find("test1").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text2.text = hpal.m_CurrentHealth.ToString();
    }
}
