using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour {
    public FallingRock rock1;
    public FallingRock rock2;
    public float interval = 15f;
    public GameObject SoundOrigin;
    public GameObject SpawnSound;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!PlayerManager.Instance.playing)
        {
            nextRock = Time.time + interval + Random.Range(0f, 0.4f*interval);
        }
        else
        {
            if (nextRock < Time.time)
            {
                if(Random.Range(0f,1f) > 0.5f){
                    StartCoroutine(rock1.FallRock(PlayerManager.Instance.difficulty));
                }else{
                    StartCoroutine(rock1.FallRock(PlayerManager.Instance.difficulty));
                }
                nextRock = Time.time+interval + Random.Range(0f, 0.4f * interval);
                Instantiate(SpawnSound, SoundOrigin.transform);
            }
        }
	}

    float nextRock = 0;
}
