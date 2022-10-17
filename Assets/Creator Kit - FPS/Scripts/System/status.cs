using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class status : MonoBehaviour
{
    public float hp = 100;
    public GameSystem GM;
    public GameObject target;
    Image imgHP;
    private int Sx;
    private TextMeshProUGUI text1;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target");
        imgHP = GameObject.Find("HP").GetComponent<Image>();
        text1 = GameObject.Find("hptext").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        imgHP.fillAmount = hp / 100;
        text1.text = "HP : " + ((int)hp).ToString();
        death();  
    }
    void death()
    {
        if (hp <= 0)
        {
            GameSystem.Instance.StopTimer();
            GameSystem.Instance.FinishRun();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        hp -= 1;
    }
}
