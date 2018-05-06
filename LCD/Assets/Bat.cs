using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public BatSprite[] Bats;
    public int damageSlot = 1;
    public Sonic sonic;
    public Sonic sonic2;
    public float interval = 2f;
    public bool isRecovering = false;
    public float deathTimeout = 20f;
    public float sonicChance = 0.1f;
    public bool IsDown { get { return currentBat == Bats.Length - 1; } }

    public GameObject SoundOrigin;
    public GameObject SonicSound;

    private static Bat _Instance = null;
    public static Bat Instance
    {
        get
        {
            if (_Instance == null)
            {

                _Instance = FindObjectOfType<Bat>();
            }
            return _Instance;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    private int currentBat = 0;
    // Update is called once per frame
    void Update()
    {
        
        if (!PlayerManager.Instance.playing)
        {
            currentBat = 0;

            for (int i = 0; i < Bats.Length; i++)
            {
                Bats[i].gameObject.SetActive(true);
            }
        }
        else
        {

            for (int i = 0; i < Bats.Length; i++)
            {
                if(i == currentBat)
                {
                    continue;
                }
                Bats[i].gameObject.SetActive(false);
            }
            if (lastMove + interval < Time.time)
            {
                lastMove = Time.time;
                Bats[currentBat].gameObject.SetActive(false);
                if (currentBat == 0)
                {
                    down = true;
                }
                else if (currentBat == Bats.Length - 1)
                {
                    down = false;
                }
                if (down)
                {
                    currentBat++;
                }
                else
                {
                    currentBat--;
                }
                if (currentBat == 0 && !sonic.isFiring && !isRecovering)
                {
                    StartCoroutine(sonic.Fire());
                    Instantiate(SonicSound, SoundOrigin.transform);
                }
                if (currentBat == 1 && !sonic2.isFiring && !isRecovering)
                {
                    StartCoroutine(sonic2.Fire());
                }
                Bats[currentBat].gameObject.SetActive(true);
            }

            if (isRecovering)
            {
                if (Time.time < lastDeath + deathTimeout * PlayerManager.Instance.difficulty)
                {
                    Bats[currentBat].gameObject.SetActive(Time.time%0.2f > 0.1f);
                }
                else
                {
                    isRecovering = false;
                }
            }
        }
    }
    public bool Damage()
    {
        if(IsDown && !isRecovering){ 
            lastDeath = Time.time;
            isRecovering = true;
            return true;
        }
        return false;
    }

    
    private float lastDeath = -10000;
    private bool down = true;
    private float lastMove = 0f;

}
