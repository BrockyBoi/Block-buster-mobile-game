using UnityEngine;
using System.Collections;

public class FrezeBlockScript : PhysicsBlockScript {

    // https://www.youtube.com/watch?v=q6YJ0rtDlGk
    CircleCollider2D explosionRadius;
    public float currentRadius;
    public float explosionRate;
    private float maxExplosionSize;
    private float explosionForce;
    private bool exploded;

    void Awake()
    {

    }

    // Use this for initialization
   public override void Start()
    {
        base.Start();

        exploded = false;
        explosionRadius = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame

   public override void Update()
    {
        base.Update();
        spawnBlock();

        maxExplosionSize = transform.localScale.x * .02f;

        if (explosionForce < 400)
        {
            explosionForce = transform.localScale.x * 8;
        }
    }

    void FixedUpdate()
    {
        if (exploded == true)
        {
            if (currentRadius < maxExplosionSize)
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

    public override void spawnBlock()
    {
        //Do nothing
    }

   public override void OnMouseDown()
    {
        //health--;
        exploded = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (exploded == true)
        {
            if (collider.tag == "Block")
            {
                Rigidbody2D rb2d = collider.gameObject.GetComponent<Rigidbody2D>();
                Transform transform = collider.gameObject.GetComponent<Transform>();
                rb2d.freezeRotation = true;
                rb2d.velocity = new Vector2(0, 0);

                collider.gameObject.SendMessage("setFreeze");
            }
        }
    }
}
