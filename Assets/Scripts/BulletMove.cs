using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    Rigidbody2D rgbd;
    float isFlip;

	// Use this for initialization
	void Start ()
    {
        rgbd = gameObject.GetComponent<Rigidbody2D>();
        isFlip = GameObject.Find("Deer").GetComponent<DeerControl>().IsFlip;
        if (isFlip == -1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        rgbd.velocity = new Vector2(isFlip * 200.0f * Time.deltaTime, 0.0f);
        Destroy(gameObject, 3.0f);
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
