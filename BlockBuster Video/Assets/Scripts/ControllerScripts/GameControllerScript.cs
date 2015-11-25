using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameControllerScript : MonoBehaviour {
    //Sizes of each block
    float sizes;
    float totalSize;
    float totalSizeX;
    float totalSizeY;

    float screenSize;


    //Spawn system
    float currentTime;
    float spawnTime;
    public static float spawnRate;


    //Round system
    float newRound;
    float roundLength;
    bool nextRound;
    bool bossRound;
    int round;


    //Block prefabs
    public GameObject block;
    public GameObject bomb;
    public GameObject blackHole;
    public GameObject freeze;
    public GameObject mutliplier;
    public GameObject smallBoss;
    public GameObject mediumBoss;
    public GameObject largeBoss;


    //Health system
    public static int blockHealth;
    public static float health;

    //Difficulty System
    public float nextDifficulty;
    public int difficultyLevel;

    //Variables all blocks share
    PhysicsBlockScript blockScript = new PhysicsBlockScript();

    public static float growRate;
    public static float attackRate;

    public static int regularDamage = 1;

    public static float smallBossAttack = 2;
    public static int smallBossDamage = 1;

    public static float mediumBossAttack = 2.5f;
    public static int mediumBossDamage;

    public static float bossAttack = 20;


    //Arraylist of all blocks currently in the game
    List<PhysicsBlockScript> blockList;


    // Use this for initialization
    void Start () {

        spawnSmallBoss();

        //spawnMediumBoss();

        //spawnLargeBoss();


        //Beginning health
        health = 5;

        //First round / difficulty
        bossRound = false;
        round = 1;
        difficultyLevel = 1;

        //Screen size stuff
        screenSize = CameraScript.screenSize.x * CameraScript.screenSize.y;

        //Initializes the arraylist
        blockList = PhysicsBlockScript.blockList;

        //Time when next difficulty curve happens
        nextDifficulty = Time.time + 15;

        //Spawn rate variables
        spawnRate = 1f;
        spawnTime = Time.time + spawnRate;

        //Block variables
        attackRate =  3;
        growRate = .15f;
        regularDamage = 1;
        blockHealth = 1;

        //Boss variables 
        bossAttack = 20;

        blockScript.newVariables(attackRate, growRate, regularDamage, blockHealth, bossAttack);
    }
	
	// Update is called once per frame
	void Update () {

        //spawnSystem();

        difficulty();

        if(health <= 0)
        {
            Debug.Log("deaderino");
        }

    }

    //Endless spawn system
    void spawnSystem()
    {
        if (Time.time >= spawnTime)
        {
            if (!bossRound)
            {
                spawnTime = Time.time + spawnRate;
                spawnRegular();
            }
            else if (round % 4 == 0)
            {
                bossRound = true;
                bossSpawner();
            }
        }

        isBoss();

    }

    void difficulty()
    {
        if(Time.time >= nextDifficulty && !bossRound)
        {
            difficultyLevel++;
            round++;
            if (round % 4 == 0)
            {
                spawnTime = Time.time + 60;
                nextDifficulty = Time.time + 60;
                bossRound = true;
                bossSpawner();
            }
            else
            {
                nextDifficulty = Time.time + 15;
            }

            growRate += .03f;

            if (spawnRate > .5f)
            {
                spawnRate -= .1f;
            }
            else
            {
                spawnRate -= .02f;
            }

            if(bossAttack > 12.5f)
            {
                bossAttack -= .5f;
            }
            else
            {
                bossAttack -= .05f;
            }

            if (attackRate > 1.5f)
            {
                attackRate -= .1f;
            }
            else
            {
                attackRate -= .01f;
            }

            //New variables are set for blocks
            blockScript.newVariables(attackRate, growRate, regularDamage, blockHealth, bossAttack);
        }
    }

    void isBoss()
    {
        if(bossRound)
        {
            if(blockList.Count == 0 || Time.time >= spawnTime)
            {
                round++;
                bossRound = false;
                nextDifficulty = Time.time + 15;
                spawnTime = Time.time;
            }
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }


    //List of spawning methods
    void spawnRegular()
    {
        Instantiate(block, new Vector3(Random.Range(-25f, 25f), Random.Range(-15f, 15f), 0), Quaternion.identity);
    }

    void spawnSmallBoss()
    {
        Instantiate(smallBoss, new Vector3(0, 0,0), Quaternion.identity);
    }

    void spawnMediumBoss()
    {
        Instantiate(mediumBoss, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void spawnLargeBoss()
    {
        Instantiate(largeBoss, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void bossSpawner()
    {
            float randomNum = Random.Range(0, 100);
            if (randomNum >= 0 && randomNum <= 33)
            {
                spawnSmallBoss();
            }
            else if (randomNum > 33 && randomNum <= 67)
            {
                spawnMediumBoss();
            }
            else
            {
                spawnLargeBoss();
            }
    }

    void spawnBomb()
    {
        Instantiate(bomb, new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f), 0), Quaternion.identity);
    }

    void spawnBlackHole()
    {
        Instantiate(blackHole, new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f), 0), Quaternion.identity);
    }

    void spawnFreeze()
    {
        Instantiate(freeze, new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f), 0), Quaternion.identity);
    }

    void spawnMultiplier()
    {
        Instantiate(mutliplier, new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f), 0), Quaternion.identity);
    }
}
