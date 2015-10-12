using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {
	public GameObject block;

	private float spawnRate;
	private float currentTime;

	public int health;
	private Vector3 originalScale;

	void Awake()
	{
		currentTime = Time.time;
		spawnRate = Random.Range (2, 5);
	}

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy(gameObject);
		}

		grow ();

		if (Time.time > currentTime + spawnRate) {
			spawnBlock();
		}
		
	}

	void spawnBlock()
	{
		Instantiate(block, new Vector3(Random.Range(-20f,20f),Random.Range(-20f,20f),0), transform.rotation);
		currentTime = Time.time;
		spawnRate++;
	}

	void grow()
	{
		transform.localScale = transform.localScale += new Vector3(.1f,.1f,0);
	}

	void OnMouseDown()
	{
		health--;
	}

	void destroyBlock()
	{
		Destroy (gameObject);
	}
}
