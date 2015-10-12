using UnityEngine;
using System.Collections;

public class MultiplierBlockScript : PhysicsBlockScript {

    public GameObject multiplierSpawn;


    void Awake()
    {

    }

    // Use this for initialization
   public override void Start() {
        base.Start();
        spawnRate = Random.Range(2, 5);

    }

    // Update is called once per frame
   public override void Update() {
        base.Update();

        spawnBlock();
	}

	void multiply()
	{
		for (int i = 0; i < 4; i++) {
			Instantiate(multiplierSpawn, transform.position, Quaternion.identity);
		}	
	}
	
	public override void OnMouseDown()
	{
		multiply ();
        blockList.Remove(thisBlock);
        Destroy(gameObject);
	}
}
