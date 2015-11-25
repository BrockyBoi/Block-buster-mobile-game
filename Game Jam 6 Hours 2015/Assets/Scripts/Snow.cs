using UnityEngine;
using System.Collections;

public class Snow : MonoBehaviour {
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
        player = FindObjectOfType<Player>();
        playerX = player.transform.localPosition.x;
        playerY = player.transform.localPosition.y;

        //level = controller.level;

        

        rb2d = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        if (rb2d.mass == 10)
        {
            speed = 50/2;
        }
        else if (rb2d.mass == 15)
        {
            speed = 75/2;
        }
        else if (rb2d.mass == 30)
        {
            speed = 100/2;
        }
        else if (rb2d.mass == 35)
        {
            speed = 130/2;
        }

        transform.position = new Vector3(playerX + Random.Range(35,60), Random.Range(-12,12),0);

        Destroy(gameObject, 20);
	}
	
	// Update is called once per frame
	void Update () {
        //playerX = player.transform.localPosition.x;
        //playerY = player.transform.localPosition.y;
        posX = trans.localPosition.x;
        posY = trans.localPosition.y;


        //rb2d.velocity = new Vector2(-1, 0) * speed;
        Vector3 movement = new Vector3 (-1,0,0);
        rb2d.AddForce(movement * speed);
        //trans.position = trans.position + new Vector3(-.05f, 0, 0) * speed;

    }
}
