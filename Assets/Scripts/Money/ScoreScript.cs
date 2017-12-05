using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    public int score;
    private Text sco;
	// Use this for initialization
    void Start()
    {
        sco = GetComponent<Text>();
        sco.text = "SCORE: " + score.ToString();
    }
	public void AddScore (int amount) {
        sco = GetComponent<Text>();
        score += amount;
        sco.text = "SCORE: " + score.ToString();
        if (score > 50)
        {
            //WIN
        }
	}

}
