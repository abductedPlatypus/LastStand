using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour {
    public GameObject target;
    public float interval = 0.1f;
    //public float length;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!PlayerManager.Instance.playing)
        {
            target.SetActive(true);
        }
        else if(!blinking)
        {
            target.SetActive(false);
        }
	}
    bool blinking = false;
    public IEnumerator DoBlink()
    {
        blinking = true;
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        yield return new WaitForSeconds(interval);
        target.SetActive(true);
        yield return new WaitForSeconds(interval);
        target.SetActive(false);
        
        blinking = false;

    }
}
