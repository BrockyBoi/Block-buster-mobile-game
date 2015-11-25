using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    Rigidbody2D rb2d;
    Transform trans;

    public Text distanceText;
    public Text winText;

    bool contact;
    float contactTime;

    float h;
    float v;
    float speed;

    int playerX;
    int distanceHome;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();

        contact = false;

        playerX = (int)trans.localPosition.x;
        distanceHome = 1000 - playerX;
        speed = .25f;

        winText.text = "";
        distanceText.text = "Distance To Home: " + distanceHome.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        playerX = (int)trans.localPosition.x;
        distanceHome = 1000 - playerX;
        distanceText.text = "Distance To Home: " + distanceHome.ToString();


        //h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        trans.position += new Vector3(.5f, v, 0) * speed;
        
        if (contact == false)
        {
            rb2d.AddForce(new Vector2(125, 0));
        }
        else
        {
            if(Time.time >= contactTime)
            {
                contact = false;
            }
        }


        if(transform.localPosition.y > 12)
        {
            transform.position = new Vector3(trans.localPosition.x, 12f, 0);
        }

        if (transform.localPosition.y < -12)
        {
            transform.position = new Vector3(trans.localPosition.x, -12f, 0);
        }

        if (trans.localPosition.x >= 1000)
        {
            winText.text = "You Win!";
        }
        else
        {
            winText.text = "";
        }

    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Snow")
        {
            contact = true;
            contactTime = Time.time + .5f;
        }
        else
        {
            rb2d.AddForce(new Vector2(10000, 0));
            Destroy(collider.gameObject);
        }

    }
}
