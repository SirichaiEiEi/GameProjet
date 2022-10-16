using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class healbar : MonoBehaviour
{
    private Image healthbar;
    public Target dddd;
    public TextMeshProUGUI text2;

    void Start()
    {
        healthbar = GetComponent<Image>();

    }
    void Update()
    {
        healthbar.fillAmount = dddd.m_CurrentHealth / dddd.health;
        text2.text = dddd.m_CurrentHealth.ToString();
    
    }






























}