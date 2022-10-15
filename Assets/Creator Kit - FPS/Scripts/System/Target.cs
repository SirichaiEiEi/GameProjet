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
    public float health = 5.0f;
    NavMeshAgent agent;
    public GameObject player;
    Animator anim;
    public GameObject popup;
    public GameObject pointpopup;
    public int pointValue;

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
    
    void Start()
    {
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
        if (dist <= 10.5f) 
            agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
    }

    public void Got(float damage)
    {
        ShowDamage(damage.ToString());
        m_CurrentHealth -= damage;

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
        ShowPoint(pointValue.ToString(pointValue+"Point"));
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
