using UnityEngine;
using System.Collections;

public class MouseControllerScript : MonoBehaviour {

    Vector3 position;
    CircleCollider2D explosionRadius;
    float currentRadius;
    float explosionRate;
    float maxExplosionSize;
    float explosionForce;
    public bool exploded;

    public bool explosionPowerUp;

    // Use this for initialization
    void Start () {
        explosionRadius = gameObject.GetComponent<CircleCollider2D>();
        currentRadius = .001f;
        explosionRate = 5;
        maxExplosionSize = 20;
        explosionForce = 250;
        exploded = false;
        explosionRadius.isTrigger = true;
        explosionRadius.radius = currentRadius;

        explosionPowerUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = position;

        mouseCheck();
	}

    void FixedUpdate()
    {
        if (exploded == true)
        {
            if (currentRadius < maxExplosionSize)
            {
                currentRadius += explosionRate;
            }
            else
            {
               reset();
            }

            explosionRadius.radius = currentRadius;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (exploded == true)
        {
            Vector2 target = collider.gameObject.transform.position;
            Vector2 bomb = gameObject.transform.position;

            Vector2 direction = explosionForce * (target - bomb);

            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(direction);
        }
    }

    void mouseCheck()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (explosionPowerUp)
            {
                exploded = true;
            }
       }
    }

    void reset()
    {
        exploded = false;
        currentRadius = .001f;
    }
}
