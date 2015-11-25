using UnityEngine;
using System.Collections;

public class BlackHoleBlock : ExplosionBlockScript {

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (exploded == true)
        {
            if (collider.tag == "Block")
            {
                Vector2 target = collider.gameObject.transform.position;
                Vector2 hole = gameObject.transform.position;

                Vector2 direction = explosionForce * (hole - target);

                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(direction);
            }
        }
    }
}
