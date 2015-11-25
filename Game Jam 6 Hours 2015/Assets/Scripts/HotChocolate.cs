using UnityEngine;
using System.Collections;

public class HotChocolate : MonoBehaviour {
    Player player;
    GameController controller;
    float playerX;
    float playerY;

    Rigidbody2D rb2d;
    Transform trans;
    float posX;
    float posY;

    float speed;
    int level;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();

        player = FindObjectOfType<Player>();
        playerX = player.transform.localPosition.x;
        playerY = player.transform.localPosition.y;

        transform.position = new Vector3(playerX + Random.Range(35, 60), Random.Range(-12, 12), 0);
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 movement = new Vector3(-1, 0, 0);
        rb2d.AddForce(movement * speed);

        if (transform.localPosition.y > 12)
        {
            transform.position = new Vector3(trans.localPosition.x, 12f, 0);
        }

        if (transform.localPosition.y < -12)
        {
            transform.position = new Vector3(trans.localPosition.x, -12f, 0);
        }
    }
}
