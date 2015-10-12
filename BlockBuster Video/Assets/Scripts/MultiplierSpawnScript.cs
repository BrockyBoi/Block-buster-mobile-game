using UnityEngine;
using System.Collections;

public class MultiplierSpawnScript : PhysicsBlockScript {

	private Rigidbody2D rb2d;
	
	void Awake()
	{
		
	}
	
	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        spawnRate = Random.Range(2, 5);

        transform.localScale = new Vector3 (10, 10, 0);
        totalSize += transform.localScale.x + transform.localScale.y;
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		launch ();
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();

      //  spawnBlock();
	}

    //When launch is triggered the block will be sent in a random direction depending on the random generator
    void launch()
	{
        float range = Random.Range(0f, 100f);

        if (range <= 25)
        {
            rb2d.AddForce(new Vector3(Random.Range(500f, 1000f), Random.Range(500f, 1000f), 0));
        }
        else if(range > 25 && range <= 50)
        {
            rb2d.AddForce(new Vector3(Random.Range(-500f, -1000f), Random.Range(500f, 1000f), 0));
        }
        else if(range > 50 && range <= 75)
        {
            rb2d.AddForce(new Vector3(Random.Range(-500f, -1000f), Random.Range(-500f, -1000f), 0));
        }
        else
        {
            rb2d.AddForce(new Vector3(Random.Range(500f, 1000f), Random.Range(-500f, -1000f), 0));
        }
	}
}
