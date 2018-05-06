using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {
    public float interval = 0.8f;
    public float respawnInterval = 15f;
    public GameObject climb1;
    public GameObject climb2;
    public GameObject stand;
    public GameObject throw1;
    public GameObject throw2;
    public GameObject throw3;
    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;
    public GameObject rock4;

    public GameObject SoundOrigin;
    public GameObject SpawnSound;

    private static Skeleton _Instance = null;
    public static Skeleton Instance
    {
        get
        {
            if (_Instance == null)
            {

                _Instance = FindObjectOfType<Skeleton>();
            }
            return _Instance;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!PlayerManager.Instance.playing)
        {
            state = 0;
            lastUpdate = Time.time;
            respawnTime = 15f;
            //show all
            climb1.SetActive(true);
            climb2.SetActive(true);
            stand.SetActive(true);
            throw1.SetActive(true);
            throw2.SetActive(true);
            throw3.SetActive(true);
            rock1.SetActive(true);
            rock2.SetActive(true);
            rock3.SetActive(true);
            rock4.SetActive(true);
        }
        else
        {
            if (activeBullets <= 0)
            {
                rock1.SetActive(false);
                rock2.SetActive(false);
                rock3.SetActive(false);
                rock4.SetActive(false);
            }
            if(state == 0 )
            {
                if (respawnTime < Time.time)
                {
                    state = 1;
                    climb1.SetActive(true);
                }
                else
                {
                    climb1.SetActive(Time.time%0.2f > 0.1f);
                }
                
                climb2.SetActive(false);
                stand.SetActive(false);
                throw1.SetActive(false);
                throw2.SetActive(false);
                throw3.SetActive(false);
            }
            if (lastUpdate + interval < Time.time)
            {
                lastUpdate = Time.time;
                switch (state)
                {
                    case 1:
                        {
                            Instantiate(SpawnSound, SoundOrigin.transform);
                            climb1.SetActive(true);
                            climb2.SetActive(false);
                            stand.SetActive(false);
                            throw1.SetActive(false);
                            throw2.SetActive(false);
                            throw3.SetActive(false);
                            state++;
                            break;
                        }
                    case 2:
                        {
                            climb1.SetActive(false);
                            climb2.SetActive(true);
                            stand.SetActive(false);
                            throw1.SetActive(false);
                            throw2.SetActive(false);
                            throw3.SetActive(false);
                            state++;
                            break;
                        }
                    case 3:
                        {
                            climb1.SetActive(false);
                            climb2.SetActive(false);
                            stand.SetActive(true);
                            throw1.SetActive(true);
                            throw2.SetActive(false);
                            throw3.SetActive(false);
                            state++;
                            break;
                        }
                    case 4:
                        {
                            climb1.SetActive(false);
                            climb2.SetActive(false);
                            stand.SetActive(true);
                            throw1.SetActive(false);
                            throw2.SetActive(true);
                            throw3.SetActive(false);
                            state++;
                            break;
                        }
                    case 5:
                        {
                            climb1.SetActive(false);
                            climb2.SetActive(false);
                            stand.SetActive(true);
                            throw1.SetActive(false);
                            throw2.SetActive(false);
                            throw3.SetActive(true);
                            StartCoroutine(Fire());
                            state=3;
                            break;
                        }
                    default: break;
                }
            }
        }
	}

    public IEnumerator Fire() //dangerous code, can be called twice at the same time
    {
        activeBullets++;
        rock1.SetActive(true);
        yield return new WaitForSeconds(interval);
        rock1.SetActive(false);
        rock2.SetActive(true);
        yield return new WaitForSeconds(interval);
        rock2.SetActive(false);
        if (PlayerManager.Instance.slot == 0)
        {
            PlayerManager.Instance.Damage(0, false, true);
        }
        else
        {
            rock3.SetActive(true);
            yield return new WaitForSeconds(interval);
            rock3.SetActive(false);
            rock4.SetActive(true);
            yield return new WaitForSeconds(interval);
            rock4.SetActive(false);
            if (PlayerManager.Instance.slot == 1) // allows for a dodge
            {
                PlayerManager.Instance.Damage(1, true, false);
            }
        }


        activeBullets--;
    }

    public bool Damage()
    {
        if(state >= 3)
        {
            state = 0;
            respawnTime = Time.time + (Random.Range(0,respawnInterval * 0.4f) + respawnInterval) * PlayerManager.Instance.difficulty;
            return true;
        }
        return false;
    }

    private int state = 0;
    private float lastUpdate = 0;
    private float respawnTime = 15f;
    private int activeBullets = 0;
}
