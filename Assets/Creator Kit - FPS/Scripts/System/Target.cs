using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.UI;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using TMPro;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [SerializeField] public float health;
    public TextMeshProUGUI text3;
    float bulletSpeed = 100;
    public GameObject bullet;
    int minHealth = 1;
    int maxHealth = 100;
    NavMeshAgent agent;
    public GameObject player;
    Animator anim;
    public GameObject popup;
    public GameObject pointpopup;
    public int pointValue;
    public LineRenderer line;
    public Transform pos1;
    public Transform pos2;

    public ParticleSystem DestroyedEffect;

    [Header("Audio")]
    public RandomPlayer HitPlayer;
    public AudioSource IdleSource;
    
    public bool Destroyed => m_Destroyed;

    bool m_Destroyed = false;
    public float m_CurrentHealth;



    void Awake()
    {
        Helpers.RecursiveLayerChange(transform, LayerMask.NameToLayer("Target"));
    }

    void Fire()
    {
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward);
        Destroy(tempBullet, 2f);
    }

    void Start()
    {
        pos2 = GameObject.Find("Character").transform.GetComponent<Transform>();
        health = Random.Range(minHealth, maxHealth);
        if (health <= 10)
        {
            text3.text = "Lv 1";
        }
        if (health >= 11 && health < 20)
        {
            text3.text = "Lv 2";
        }
        if (health >= 20 && health < 30)
        {
            text3.text = "Lv 3";
        }
        if (health >= 30 && health < 40)
        {
            text3.text = "Lv 4";
        }
        if (health >= 40 && health < 50)
        {
            text3.text = "Lv 5";
        }
        if (health >= 50 && health < 60)
        {
            text3.text = "Lv 6";
        }
        if (health >= 60 && health < 70)
        {
            text3.text = "Lv 7";
        }
        if (health >= 70 && health < 80)
        {
            text3.text = "Lv 8";
        }
        if (health >= 80 && health < 90)
        {
            text3.text = "Lv 9";
        }
        if (health >= 90)
        {
            text3.text = "Lv 10";
        }
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        popup = GameObject.Find("FloatingParent");
        pointpopup = GameObject.Find("FloatingParent1");
        if (DestroyedEffect)
            PoolSystem.Instance.InitPool(DestroyedEffect, 16);
        
        m_CurrentHealth = health;
        if (IdleSource != null)
            IdleSource.time = Random.Range(0.0f, IdleSource.clip.length);
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist <= 3f)
        {
            agent.SetDestination(player.transform.position);
            transform.LookAt(player.transform);
        }
        if (dist <= 10.5f)
        {
            Fire();
            agent.SetDestination(player.transform.position);
            transform.LookAt(player.transform);
        }
    }
    void attrak()
    {
        if (FindObjectOfType<status>().hp <= 0)
        {
            FindObjectOfType<status>().hp -= 0;
        }
        else
        {
            FindObjectOfType<status>().hp -= 0.1f;
        }
    }

    public void Got(float damage)
    {
        ShowDamage(damage.ToString());
        m_CurrentHealth -= damage;
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);

        if (HitPlayer != null)
            HitPlayer.PlayRandom();

        if (m_CurrentHealth > 0)
            return;

        Vector3 position = transform.position;
        

        //the audiosource of the target will get destroyed, so we need to grab a world one and play the clip through it
        if (HitPlayer != null)
        {
            var source = WorldAudioPool.GetWorldSFXSource();
            source.transform.position = position;
            source.pitch = HitPlayer.source.pitch;
            source.PlayOneShot(HitPlayer.GetRandomClip());
        }

        if (DestroyedEffect != null)
        {
            var effect = PoolSystem.Instance.GetInstance<ParticleSystem>(DestroyedEffect);
            effect.time = 0.0f;
            effect.Play();
            effect.transform.position = position;
        }

        m_Destroyed = true;
        
        gameObject.SetActive(false);
       
        GameSystem.Instance.TargetDestroyed(pointValue);
        ShowPoint(pointValue.ToString(pointValue + "Point"));
    }
    void ShowDamage(string text)
    {
        if(popup)
        {
            GameObject prefab = Instantiate(popup, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
            
        }
    }
    void ShowPoint(string text)
    {
        if (pointpopup)
        {
            GameObject prefab = Instantiate(pointpopup, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;

        }
    }
}
