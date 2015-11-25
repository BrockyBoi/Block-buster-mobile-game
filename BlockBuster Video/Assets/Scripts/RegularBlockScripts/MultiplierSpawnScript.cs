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
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		base.launch ();
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();

      //  spawnBlock();
	}

    //When launch is triggered the block will be sent in a random direction depending on the random generator
  
}
