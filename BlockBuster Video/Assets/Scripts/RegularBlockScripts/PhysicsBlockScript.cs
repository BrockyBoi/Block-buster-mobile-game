using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PhysicsBlockScript : MonoBehaviour
{
    public GameObject block;
    public Rigidbody2D rb2d;
    public SpriteRenderer rend;
    public Color col;
    public float red;

    public float spawnRate;

    public float currentTime;
    private double nextHealth;

    //Boss variables
    public static float smallBossAttack;
    public static float smallBossAttackTime;
    public static int smallBossDamage;

    public static float mediumBossAttack;
    public static float mediumBossAttackTime;
    public static int mediumBossDamage;

    public static float largeBossAttack;
    public static float largeBossAttackTime;

    public static float bossAttack;
    public static float bossAttackTime;

    //Block attack variables
    public float damage;
    public float attackRate;
    private float attackTime;

    //Block health variables
    public int health;
    public int maxHealth;
    public double healthRate;

    //Block size variables
    public Vector3 originalScale;
    public Vector3 maxSize;

    public bool frozen;
    public double thawTime;

    public bool clickedOn;
    public double clickTimer;

    public bool canGrow;
    public float growRate;

    public static List<PhysicsBlockScript> blockList = new List<PhysicsBlockScript>();
    public PhysicsBlockScript thisBlock;

    void Awake()
    {

    }

    // Use this for initialization
    public virtual void Start()
    {
        generalConstructor();
    }

    // Update is called once per frame
    public virtual void Update()
    {

        destroyBlock();

        // addHealth();

        grow();

        // spawnBlock();

        thaw();

        // regenerate();
    }

    public void generalConstructor()
    {
        //This calls every variable that can be changed from the difficulty setting for every type 
        //of block (boss and regular)
        newVariables(GameControllerScript.attackRate, GameControllerScript.growRate,
                     GameControllerScript.regularDamage, GameControllerScript.blockHealth,
                     GameControllerScript.bossAttack);

        //Spawnrate if they ever need it
        spawnRate = Random.Range(2, 5);

        //Rigid body
        rb2d = GetComponent<Rigidbody2D>();

        //Makes it so the boss stays in one spot
        if (tag == "Block")
        {
            rb2d.AddForce(new Vector3(Random.Range(-25f, 25f), Random.Range(-25f, 25f), 0));
        }

        //Places blocks in arraylist
        thisBlock = this;
        blockList.Add(thisBlock);

        //Color and renderers
        rend = gameObject.GetComponent<SpriteRenderer>();
        col = rend.material.color;
        red = 0;

        frozen = false;

        currentTime = Time.time;
        nextHealth = currentTime + 1.5;

        //Size variables
        transform.localScale = new Vector3(5, 5, 0);
        maxSize = new Vector3(60, 60, 0);

        //Block health variables
        maxHealth = health;
        healthRate = 2.5;

        clickedOn = false;

        //Boss Variables
    }

    public void newVariables(float myAttackRate, float myGrowRate, float myDamage, int myHealth, float myBossAttack)
    {
        attackRate = myAttackRate;
        growRate = myGrowRate;
        damage = myDamage;
        health = myHealth;
        bossAttack = myBossAttack;
    }

    public virtual void spawnBlock()
    {
        if (Time.time > currentTime + spawnRate && frozen != true)
        {
            Instantiate(block, new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f), 0), transform.rotation);
            currentTime = Time.time;
            //spawnRate++;
        }
    }

    public virtual void grow()
    {
        if (!frozen && transform.localScale.x < maxSize.x)
        {
            transform.localScale = transform.localScale += new Vector3(growRate, growRate, 0);

            if (transform.localScale.x + growRate >= maxSize.x)
            {
                currentTime = Time.time;
                attackTime = currentTime + attackRate;
            }
        }
        else if (transform.localScale.x >= maxSize.x)
        {
            attack();
        }

    }


    public virtual void OnMouseDown()
    {
        health--;
        clickedOn = true;
        clickTimer = Time.time + 2;
        float num = ((float)health / maxHealth);
        //col.a = col.a * num;
        //rend.color = col;

    }

    public virtual void destroyBlock()
    {
        if (health <= 0)
        {
            blockList.Remove(thisBlock);
            Destroy(gameObject);
        }
    }

    public virtual void addHealth()
    {
        if (Time.time >= nextHealth && frozen != true)
        {
            currentTime = Time.time;
            nextHealth = currentTime + healthRate;
            health++;
            if (maxHealth < health)
            {
                maxHealth = health;
            }
        }
    }

    public virtual void setFreeze()
    {
        frozen = true;
        thawTime = Time.time + 1.5;
    }

    public virtual void thaw()
    {
        if (frozen == true)
        {
            if (Time.time >= thawTime)
            {
                frozen = false;
            }
        }
    }

    public virtual void regenerate()
    {
        if (clickedOn)
        {
            if (Time.time >= clickTimer)
            {
                clickedOn = false;
                health = maxHealth;
                //col.a = 1.0f;
                //rend.color = col;
            }
        }
    }

    public void flyAway()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Destroy(gameObject, 3);
    }

    public void chargeRedNormal()
    {
        red = ((Time.time - currentTime) / (attackTime - currentTime));
        col.r = red;
        col = new Color(1, 1 - red, 1 - red);
        rend.color = col;
    }

    public void chargeRedBoss()
    {
        red = ((Time.time - currentTime) / (bossAttackTime - currentTime));
        col.r = red;
        col = new Color(1, 1 - red, 1 - red);
        rend.color = col;
    }


    public virtual void attack()
    {
        if (!frozen)
        {
            if (tag == "Block")
            {
                chargeRedNormal();
                if (Time.time >= attackTime)
                {
                    GameControllerScript.health -= damage;
                    currentTime = Time.time;
                    attackTime = currentTime + attackRate;
                }
            }
        }
    }

    public virtual void launch()
    {
        float range = Random.Range(0f, 100f);

        if (range <= 25)
        {
            rb2d.AddForce(new Vector3(Random.Range(500f, 1000f), Random.Range(500f, 1000f), 0));
        }
        else if (range > 25 && range <= 50)
        {
            rb2d.AddForce(new Vector3(Random.Range(-500f, -1000f), Random.Range(500f, 1000f), 0));
        }
        else if (range > 50 && range <= 75)
        {
            rb2d.AddForce(new Vector3(Random.Range(-500f, -1000f), Random.Range(-500f, -1000f), 0));
        }
        else
        {
            rb2d.AddForce(new Vector3(Random.Range(500f, 1000f), Random.Range(-500f, -1000f), 0));
        }
    }
}
