using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float interval = 10;
    public float chargeInterval = 1f;
    public float animInterval = 1f;
    public float dazeInterval = 4f;
    public GameObject Spider1;
    public GameObject Spider2;
    public GameObject Spider2Idle;
    public GameObject Spider2Attack;
    public GameObject Spider2Dazed;
    public float damageTimeout = 5f;

    public GameObject SoundOrigin;
    public GameObject SpawnSound;

    private static Spider _Instance = null;
    public static Spider Instance
    {
        get
        {
            if (_Instance == null)
            {

                _Instance = FindObjectOfType<Spider>();
            }
            return _Instance;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (!PlayerManager.Instance.playing)
        {
            state = 0;
            //show all
            Spider1.SetActive(true);
            Spider2.SetActive(true);
            Spider2Idle.SetActive(true);
            Spider2Attack.SetActive(true);
            Spider2Dazed.SetActive(true);
        }
        else
        {
            if(damagedTime + damageTimeout * (1f-(1f-PlayerManager.Instance.difficulty)/2f) > Time.time)
            {
                damageable = false;
                Spider2.SetActive(false);
                Spider2Idle.SetActive(false);
                Spider2Attack.SetActive(false);
                Spider2Dazed.SetActive(false);
                Spider1.SetActive(Time.time %0.2f > 0.1f);
            }
            else if (nextMove < Time.time)
            {
                Spider1.SetActive(false);
                Spider2.SetActive(false);
                Spider2Idle.SetActive(false);
                Spider2Attack.SetActive(false);
                Spider2Dazed.SetActive(false);
                switch (state)
                {
                    case 0:
                        {
                            damageable = false;
                            nextMove = Time.time + (Random.Range(0f, 0.6f * interval) + interval) * PlayerManager.Instance.difficulty;
                            state++;
                            break;
                        }
                    case 1:
                        {
                            damageable = false;
                            nextMove = Time.time + chargeInterval;
                            Spider1.SetActive(true);
                            Instantiate(SpawnSound, SoundOrigin.transform);
                            state++;
                            break;
                        }
                    case 2:
                        {
                            damageable = true;
                            nextMove = Time.time + animInterval;
                            Spider2.SetActive(true);
                            Spider2Attack.SetActive(true);
                            PlayerManager.Instance.Damage(1, false, true);
                            if (PlayerManager.Instance.blocking && PlayerManager.Instance.slot == 1)
                            {
                                state = 4;
                            }
                            else
                            {
                                state++;
                            }
                            break;
                        }
                    case 3:
                        {
                            nextMove = Time.time + animInterval;
                            Spider2.SetActive(true);
                            Spider2Idle.SetActive(true);
                            damageable = true;
                            state = 1;
                            break;
                        }
                    case 4:
                        {
                            nextMove = Time.time + dazeInterval;
                            Spider2.SetActive(true);
                            Spider2Dazed.SetActive(true);
                            damageable = true;
                            state = 5;
                            break;
                        }
                    case 5:
                        {
                            nextMove = Time.time + animInterval;
                            Spider1.SetActive(true);
                            state = 0;
                            damageable = false;
                            break;
                            
                        }
                    default: break;
                }
            }
        }
    }
    public bool Damage()
    {
        if(damageable)
        {
            damagedTime = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public int state = 0;
    private float nextMove = 0;
    private bool damageable = false;
    private float damagedTime = -100;
}
