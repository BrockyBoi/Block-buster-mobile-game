using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PhysicsBlockScript : MonoBehaviour {
	public GameObject block;
    public Rigidbody2D rb2d;
    public SpriteRenderer rend;
    public Color col;
    float red;

    public float spawnRate;

	public float currentTime;
    public double nextTime;

    //Block attack variables
    public float damage;
    public float attackRate;
    public float attackTime;
    public int attackRound;

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

    public bool canTime;
    public float growthTime;
    public bool canGrow;
    public float growRate;

    public static float size;
    public float sizeX;
    public float sizeY;
    public static float totalSize;
    public static float totalSizeX = 0;
    public static float totalSizeY = 0;

    public static List<PhysicsBlockScript> blockList = new List<PhysicsBlockScript>();
    public PhysicsBlockScript thisBlock;

    public float fullScreen;
    public float currentScreen;
    public float screenRatio;


    void Awake()
	{

	}
	
	// Use this for initialization
	public virtual void Start () {
        generalConstructor();
    }
	
	// Update is called once per frame
	public virtual void Update () {


        attack();

        destroyBlock();
		
       // addHealth();

        grow ();
		
		//spawnBlock();

        thaw();

      //  regenerate();
    }

    public void generalConstructor()
    {
        //Spawnrate if they ever need it
        spawnRate = Random.Range(2, 5);

        //Rigid body
        rb2d = GetComponent<Rigidbody2D>();

        //Makes it so the boss stays in one spot
        if (tag != "Boss")
        {
            rb2d.AddForce(new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0));
        }

        //Places blocks in arraylist
        thisBlock = this;
        blockList.Add(thisBlock);

        //Color and renderers
        rend = gameObject.GetComponent<SpriteRenderer>();
        col = rend.material.color;
        red = 0;

        frozen = false;

        canTime = true;
        currentTime = Time.time;
        nextTime = currentTime + 1.5;

        //Size variables
        growthTime = maxSize.x * growRate;
        transform.localScale = new Vector3(0, 0, 0);
        maxSize = new Vector3(60,60,0);

        //Block health variables
        maxHealth = health;
        healthRate = 2.5;

        clickedOn = false;


        fullScreen = 918 * 404;
        currentScreen = Screen.width * Screen.height;
        screenRatio = currentScreen / fullScreen;
        growRate = GameControllerScript.growRate;

        newVariables(GameControllerScript.attackRate, GameControllerScript.growRate, 
                     GameControllerScript.regularDamage, GameControllerScript.blockHealth);
    }

    public void newVariables(float myAttackRate, float myGrowRate, float myDamage, int myHealth)
    {
        attackRate = myAttackRate;
        growRate = myGrowRate;
        damage = myDamage;
        health = myHealth;
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
        if(!frozen && transform.localScale.x <= maxSize.x)
        {
            transform.localScale = transform.localScale += new Vector3(GameControllerScript.growRate, GameControllerScript.growRate, 0);
            size = transform.localScale.x * transform.localScale.y;
            sizeX = transform.localScale.x;
            sizeY = transform.localScale.y;

            totalSize += (GameControllerScript.growRate * GameControllerScript.growRate);
        }
        
    }


    public virtual void OnMouseDown()
    {
        health--;
        clickedOn = true;
        clickTimer = Time.time + 2;
        float num = ((float)health / maxHealth);
        col.a = col.a * num;
        rend.color = col;
       
	}
	
	public virtual void destroyBlock()
	{
        if(health <= 0)
        {
            blockList.Remove(thisBlock);
            Destroy(gameObject);
        }
	}

   public virtual void addHealth()
    {
        if(Time.time >= nextTime && frozen != true)
        {
            currentTime = Time.time;
            nextTime = currentTime + healthRate;
            health++;
            if(maxHealth < health)
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
        if(frozen == true)
        {
            if(Time.time >= thawTime)
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
                col.a = 1.0f;
                rend.color = col;
            }
        }
    }

    public float getSize()
    { 
            return size;
    }

    public void flyAway()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Destroy(gameObject, 3);
    }

    public void attack()
    {
        if (!frozen && transform.localScale.x >= maxSize.x)
        {
            if (tag == "Small Boss")
            {
                if (Time.time >= GameControllerScript.smallBossAttack)
                {
                    GameControllerScript.health -= GameControllerScript.smallBossDamage;
                    GameControllerScript.smallBossAttack = Time.time + GameControllerScript.smallBossAttack;
                }
            }

            if (tag == "Medium Boss")
            {
                if (Time.time >= GameControllerScript.mediumBossAttack)
                {
                    GameControllerScript.health -= GameControllerScript.mediumBossDamage;
                    GameControllerScript.mediumBossAttack = Time.time + GameControllerScript.mediumBossAttack;
                }
            }

            if (tag == "Large Boss")
            {
                if (Time.time >= GameControllerScript.largeBossAttack)
                {
                    GameControllerScript.health -= GameControllerScript.health;
                    GameControllerScript.mediumBossAttack = Time.time + GameControllerScript.largeBossAttack;
                }
            }

            if (tag == "Block")
            {
                red = ((Time.time - currentTime) / (attackTime - currentTime));
                col.r = red;
                col = new Color(red, 0, 0);
                rend.color = col;
                if (Time.time >= attackTime)
                {
                    if(attackRound == 0)
                    {
                        currentTime = Time.time;
                        attackRound++;
                    }
                    Debug.Log(damage);
                    GameControllerScript.health -= damage;
                    currentTime = Time.time;
                    attackTime = currentTime + attackRate;
                    Debug.Log("Block attacked.  Health: " + GameControllerScript.health);
                    col = new Color(0, 0, 0);
                }
            }
        }
    }
}
