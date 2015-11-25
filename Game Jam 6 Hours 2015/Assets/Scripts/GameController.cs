using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
    Player playerScript;
    float playerX;

    public GameObject hotChocolate;

    Snow snowScript;
    public GameObject snow1;
    public GameObject snow2;
    public GameObject snow3;
    public GameObject snow4;
    public static List<Snow> snowList = new List<Snow>();

    int snowCount;
    float currentTime;
    float nextLevel;
    float spawnTime;
    float hCTime;
    public static int level;

	// Use this for initialization
	public void Start () {
        playerScript = FindObjectOfType<Player>();
        playerX = playerScript.transform.localPosition.x;

        level = 1;
        snowCount = 5;
        nextLevel = Time.time + 15;
        spawnTime = Time.time + 1;
        hCTime = Time.time + Random.Range(2.5f, 5);
	}
	
	// Update is called once per frame
	public void Update () {
        playerX = playerScript.transform.localPosition.x;
        //if (Time.time >= nextLevel)
        //{
        //    level++;
        //    snowCount += 5;
        //    nextLevel = Time.time + 15;
        //}
        if (playerX > 0 && playerX <= 200)
        {
            level = 1;
            snowCount = 3;
        }
        else if(playerX > 200 && playerX <= 400)
        {
            level = 2;
            snowCount = 6;
        }
        else if(playerX > 400 && playerX <= 700)
        {
            level = 3;
            snowCount = 8;
        }
        else if(playerX > 700)
        {
            level = 4;
            snowCount = 10;
        }

        if(Time.time >= spawnTime)
        {
            for (int i = 0; i < snowCount; i++)
            {
                if (level == 1)
                {
                    Instantiate(snow1);
                }
                else if (level == 2)
                {
                    Instantiate(snow2);
                }
                else if (level == 3)
                {
                    Instantiate(snow3);
                }
                else
                {
                    Instantiate(snow4);
                }
            }
            spawnTime = Time.time + 1f;
        }

        if(Time.time >= hCTime)
        {
            Instantiate(hotChocolate);
            hCTime = Time.time + Random.Range(2f, 4);
        }
        

        
    }
}
