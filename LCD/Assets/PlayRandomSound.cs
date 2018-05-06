using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    public AudioClip[] clips;
    public float volume = 1f;
    // Use this for initialization
    void Start()
    {
        int random = Random.Range(0, clips.Length);
        AudioSource.PlayClipAtPoint(clips[random], transform.position, volume);
        Destroy(this.gameObject, clips[random].length);
    }
}
