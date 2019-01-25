using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerControl : MonoBehaviour {

    GameObject sceneManager;

    public Sprite stand;
    public Sprite crouch;
    public GameObject bullet;

    GameObject[] clip;
    const int maxBullet = 3;
    int bulletNum = 3;
    bool canShoot = true;
    float timer = 0;
    float reloadTime = 1.5f;

    float bulletOffsetY = 0.05f;
    float isFlip = 1;

    public float IsFlip
    {
        get { return isFlip; }
    }

	// Use this for initialization
	void Start ()
    {
        sceneManager = GameObject.Find("SceneManager");

        clip = new GameObject[maxBullet];
        for (int i = 0; i < maxBullet; i++)
        {
            clip[i] = gameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (bulletNum < maxBullet)
        {
            clip[bulletNum].GetComponent<SpriteRenderer>().enabled = false;
        }

        if (bulletNum == 0)
        {
            canShoot = false;
            timer += Time.deltaTime;
            for (int i = 0; i < maxBullet; i++)
            {
                if (timer >= reloadTime * (i+1) / maxBullet)
                {
                    clip[i].GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
        
        if (timer >= reloadTime)
        {
            canShoot = true;
            timer = 0;
            bulletNum = 3;
        }

        Controls();
    }

    void Controls()
    {
        //Crouching control
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = crouch;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, -0.06f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.27f);

            gameObject.transform.GetChild(0).transform.localPosition = new Vector3(0.0f, -0.07f, -1.0f);
            bulletOffsetY = -0.02f;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stand;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, -0.03f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.34f);

            gameObject.transform.GetChild(0).transform.localPosition = new Vector3(0.0f, -0.0f, -1.0f);
            bulletOffsetY = 0.05f;
        }

        //Turning left and right
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            isFlip = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            isFlip = 1;
        }

        //Shoot
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            GameObject newBullet = Instantiate(bullet, gameObject.transform.position + new Vector3(0.20f * isFlip, bulletOffsetY, 0.0f), Quaternion.identity) as GameObject;
            gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
            bulletNum -= 1;
        }

        //Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            bulletNum = 0;
            for (int i = 0; i < maxBullet; i++)
            {
                clip[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
