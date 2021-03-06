﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMove : MonoBehaviour {

    GameObject sceneManager;

    Rigidbody2D rgbd;
    Animator anim;
    float isFlip = 1;

    bool isHit = false;

    // Use this for initialization
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager");

        rgbd = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        if (gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            isFlip = 1;
        }
        else
        {
            isFlip = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
            anim.SetTrigger("isHit");
            rgbd.velocity *= new Vector2(0.925f, 0.925f);
            if (rgbd.velocity.y <= 0.1f)
            {
                rgbd.velocity = new Vector2(rgbd.velocity.x, -Mathf.Abs(rgbd.velocity.y) * 1.15f);
            }
            if (gameObject.transform.position.y <= -1.41f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1.41f);
            }
        }
        else if (sceneManager.GetComponent<SceneManager>().Health <= 0)
        {
            rgbd.velocity = Vector2.zero;
        }
        else
        {
            rgbd.velocity = new Vector2(isFlip * 175.0f * Time.deltaTime, 0.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            rgbd.velocity = new Vector2(-rgbd.velocity.x * 0.5f, 0.5f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isHit = true;
            sceneManager.GetComponent<SceneManager>().Score += 1;
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 5.0f);
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            sceneManager.GetComponent<SceneManager>().Health -= 1;
            collision.gameObject.GetComponent<DeerControl>().Anim.SetInteger("Health", sceneManager.GetComponent<SceneManager>().Health);
            if (sceneManager.GetComponent<SceneManager>().Health <= 0)
                Destroy(gameObject, 2.0f);
            else
                Destroy(gameObject, 5.0f);
        }
    }
}
