using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class healbar : MonoBehaviour
{
    public Slider SLD;
    public int maxHealth;
    public int currentHealth;
    public TextMeshProUGUI Textheal;


    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        SLD.maxValue = maxHealth;
        SLD.value = currentHealth;
        Textheal.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
