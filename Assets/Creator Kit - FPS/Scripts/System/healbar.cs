using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healbar : MonoBehaviour
{
    private Image healthbar;
    public Target dddd;

    void Start()
    {
        healthbar = GetComponent<Image>();   
    }
    void Update()
    {
        healthbar.fillAmount = dddd.m_CurrentHealth / dddd.health;
    }






























}