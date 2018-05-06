using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DazeSprite : MonoBehaviour
{
    public float interval = 0.5f;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.Instance.playing)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else
        {
            if (Time.time % interval * 3 < interval)
            {
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
            }
            else if(Time.time % interval * 3 < interval*2)
            {
                star1.SetActive(false);
                star2.SetActive(true);
                star3.SetActive(false);
            }
            else
            {
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(true);
            }
        }
    }
}
