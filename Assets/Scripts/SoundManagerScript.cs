using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    public static AudioClip swordNoise, gunNoise, enemyDeathNoise1, enemyDeathNoise2, enemyDeathNoise3;
    static AudioSource audioSrc1, audioSrc2, audioSrc3, audioSrc4, audioSrc5;

	// Use this for initialization
	void Start () {

		enemyDeathNoise1 = Resources.Load<AudioClip>("Sounds/enemyDeathNoise1");

        audioSrc1 = GetComponentInChildren<AudioSource>();
		audioSrc2 = GetComponentInChildren<AudioSource>();
		audioSrc3 = GetComponentInChildren<AudioSource>();
		audioSrc4 = GetComponentInChildren<AudioSource>();
		audioSrc5 = GetComponentInChildren<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "swordNoise":
                audioSrc1.PlayOneShot(swordNoise);
                break;
            case "gunNoise":
                audioSrc2.PlayOneShot(gunNoise);
                break;
        }
    }

    public static void PlayDeathSound(string clip)
    {
        switch (clip)
        { 
            case "enemyDeathNoise1":
                audioSrc3.PlayOneShot(enemyDeathNoise1);
                break;
            case "enemyDeathNoise2":
                audioSrc4.PlayOneShot(enemyDeathNoise2);
                break;
            case "enemyDeathNoise3":
                audioSrc5.PlayOneShot(enemyDeathNoise3);
                break;
        }

    }
}
