using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Camera : MonoBehaviour {
    Rigidbody2D rb2d;
    Player player;
    float h;
    float v;
    float speed;
    Transform trans;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        //speed = 5f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(.12f, 0, 0);
        rb2d.AddForce(new Vector2(125, 0));

        if (player.transform.localPosition.x < 1000)
        {
            transform.position = new Vector3(player.transform.localPosition.x, 0, -10);
        }
        else
        {
            transform.position = new Vector3(1000, 0, -10);
        }
    }
}
