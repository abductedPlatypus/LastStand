using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonic : MonoBehaviour
{
    public SpriteRenderer[] Sonics;
    public int damageSlot = 1;
    public bool canBeBlocked = true;
    public bool isFiring = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.Instance.playing)
        {
            for (int i = 0; i < Sonics.Length; i++)
            {
                Sonics[i].gameObject.SetActive(true);
            }
        }
        else if(!isFiring)
        {
            for (int i = 0; i < Sonics.Length; i++)
            {               
                Sonics[i].gameObject.SetActive(false);
            }
        }
    }
    public IEnumerator Fire(float interval = 1f)
    {
        isFiring = true;
        for (int i = 0; i < Sonics.Length; i++)
        {
            Sonics[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(interval);
            Sonics[i].gameObject.SetActive(false);
            if (!PlayerManager.Instance.playing)
            {
                isFiring = false;
                break;
            }
        }
        if (PlayerManager.Instance.playing)
        {
            PlayerManager.Instance.Damage(damageSlot, !canBeBlocked, true);
        }
        isFiring = false;
    }
}
