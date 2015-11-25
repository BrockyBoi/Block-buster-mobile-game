using UnityEngine;
using System.Collections;

public class ExplosionBlockScript : PhysicsBlockScript {

    // https://www.youtube.com/watch?v=q6YJ0rtDlGk
    Rigidbody2D rb2d;
	public CircleCollider2D explosionRadius;
	public float currentRadius;
	public float explosionRate;
	public float maxExplosionSize;
	public float explosionForce;
	public bool exploded; 
	


	void Awake()
	{
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Use this for initialization
	public override void Start () {
        base.Start();
        exploded = false;
		transform.localScale = new Vector3 (0, 0, 0);
		explosionRadius = gameObject.GetComponent<CircleCollider2D> ();
        explosionRadius.isTrigger = true;
        currentRadius = .001f;
        explosionRadius.radius = currentRadius;

    }

    // Update is called once per frame

    public override void Update () {
        base.Update();
        spawnBlock();
        grow();

		maxExplosionSize = transform.localScale.x * .02f;

		if (explosionForce < 400) {
			explosionForce = transform.localScale.x * 3;
		}
    }

	public virtual void FixedUpdate()
	{
		if (exploded == true) {
			if(currentRadius < maxExplosionSize)
			{
				currentRadius += explosionRate;
				Renderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
				spriteRenderer.enabled = false;
			}
			else
			{
                blockList.Remove(thisBlock);
                Destroy(gameObject);
			}

			explosionRadius.radius = currentRadius;
		}
	}

   public override void OnMouseDown()
    {
        if (frozen != true)
        { 
            //health--;
            exploded = true;
        }
	}

    public override void spawnBlock()
    {
        //Do nothing
    }

	public virtual void OnTriggerEnter2D(Collider2D collider)
	{
		if (exploded == true) {
            if (collider.tag == "Block")
            {
                Vector2 target = collider.gameObject.transform.position;
                Vector2 bomb = gameObject.transform.position;

                Vector2 direction = explosionForce * (target - bomb);

                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(direction);
                collider.gameObject.SendMessage("flyAway") ;
            }
        }
	}

    public void detonate()
    {
        exploded = true;
    }
}
