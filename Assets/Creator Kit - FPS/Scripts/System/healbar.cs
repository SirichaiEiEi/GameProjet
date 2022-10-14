using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healbar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private float _reduceSpeed = 2;
    private float _target = 1;
    private Camera aa;
    // Start is called before the first frame update
    void Start()
    {
        aa = Camera.main;
        _healthbarSprite = GameObject.Find("fill").GetComponent<Image>();
    }
    public void UpdateHealthbar(float maxHealth,float currentHealth)
    {
        _target = currentHealth / maxHealth;
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        _healthbarSprite.fillAmount = Mathf.MoveTowards(_healthbarSprite.fillAmount,_target,_reduceSpeed);
    }
}
