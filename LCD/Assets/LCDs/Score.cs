using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
    public int score = 1000;
    public int highscore = 1000;
    public bool showHighscore = true;
    public UnityEngine.UI.Text display;

    private static Score _Instance = null;
    public static Score Instance
    {
        get
        {
            if (_Instance == null)
            {

                _Instance = FindObjectOfType<Score>();
            }
            return _Instance;
        }
    }
    // Use this for initialization
    void Start () {
        highscore = PlayerPrefs.GetInt("Highscore");
        display.text = highscore.ToString("0000000");
	}
	
	// Update is called once per frame
	void Update () {
		if(score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscore = score;
        }
        if (showHighscore)
        {
            display.text = highscore.ToString("0000000");
        }
        else
        {
            display.text = score.ToString("0000000");
        }
	}
}
