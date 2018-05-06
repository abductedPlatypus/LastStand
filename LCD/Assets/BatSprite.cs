using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSprite : MonoBehaviour {
    public float interval = 0.5f;
    public GameObject up;
    public GameObject down;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!PlayerManager.Instance.playing)
        {
            up.SetActive(true);
            down.SetActive(true);
        }
        else
        {
            if (Time.time % interval * 2 > interval)
            {
                up.SetActive(true);
                down.SetActive(false);
            }
            else
            {
                up.SetActive(false);
                down.SetActive(true);
            }
        }
	}
}
