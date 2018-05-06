using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public SpriteRenderer[] Rocks;
    public int damageSlot = 0;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.Instance.playing)
        {
            for (int i = 0; i < Rocks.Length; i++)
            {
                Rocks[i].gameObject.SetActive(true);
            }
        }
        else if(!isFalling)
        {
            for (int i = 0; i < Rocks.Length; i++)
            {
                Rocks[i].gameObject.SetActive(false);
            }
        }

    }
    public IEnumerator FallRock(float interval = 1f)
    {
        isFalling = true;
        for (int i = 0; i < Rocks.Length; i++)
        {
            Rocks[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(interval);
            Rocks[i].gameObject.SetActive(false);
        }
        if (PlayerManager.Instance.playing)
        {
            PlayerManager.Instance.Damage(damageSlot, true, false);
        }
        isFalling = false;
    }

    bool isFalling = false;
}
