using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PhysicsBlockScript : MonoBehaviour {
	public GameObject block;

    public SpriteRenderer rend;
    public Color col;

    public float spawnRate;

	public float currentTime;
    public double nextTime;

    public int health;
    public int maxHealth;
    public double healthRate;

	public Vector3 originalScale;

    public bool frozen;
    public double thawTime;

    public bool clickedOn;
    public double clickTimer;

    public float growRate;

    public static float size;
    public float sizeX;
    public float sizeY;
    public static float totalSize = 0;
    public static float totalSizeX = 0;
    public static float totalSizeY = 0;

    public static List<PhysicsBlockScript> blockList = new List<PhysicsBlockScript>();
    public PhysicsBlockScript thisBlock;


    void Awake()
	{

	}
	
	// Use this for initialization
	public virtual void Start () {
        generalConstructor();

        spawnRate = Random.Range(2, 5);
    }
	
	// Update is called once per frame
	public virtual void Update () {

        destroyBlock();
		
        addHealth();

        grow ();
		
		spawnBlock();

        thaw();

        regenerate();
    }

    public void generalConstructor()
    {

        thisBlock = this;
        blockList.Add(thisBlock);

        rend = gameObject.GetComponent<SpriteRenderer>();
        col = rend.material.color;

        frozen = false;

        currentTime = Time.time;
        nextTime = currentTime + 1.5;

        transform.localScale = new Vector3(0, 0, 0);

        health = 1;
        maxHealth = 1;
        healthRate = 2.5;

        clickedOn = false;

        growRate = .1f;
    }
	
	public virtual void spawnBlock()
	{
        if (Time.time > currentTime + spawnRate && frozen != true)
        {
            Instantiate(block, new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0), transform.rotation);
            currentTime = Time.time;
            //spawnRate++;
        }
    }
	
	public virtual void grow()
	{
        if(frozen != true)
        {
            transform.localScale = transform.localScale += new Vector3(growRate, growRate, 0);
            size = transform.localScale.x + transform.localScale.y;
            sizeX = transform.localScale.x;
            sizeY = transform.localScale.y;

            
            //totalSize += (growRate * 4);
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
        if (gameObject != null)
        {
            return transform.localScale.x * transform.localScale.y;
        }
        else
        {
            return -(transform.localScale.x * transform.localScale.y);
        }
    }
}
