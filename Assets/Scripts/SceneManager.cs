using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    GameObject player;

    public GameObject enemy;
    public Text scoreText;
    int score = 0;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public Image damaged;

    public RectTransform healthObj;
    int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    Image[] hearts;
    const int maxHeart = 3;

    float timer = 0;
    bool isTime = true;
    float spawnTime;

    float where;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHeart;
        hearts = new Image[maxHeart];
        for (int i = 0; i < maxHeart; i++)
        {
            hearts[i] = healthObj.GetChild(i).GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;

        if (health < maxHeart)
        {
            if (health >= 0)
                hearts[health].GetComponent<Image>().enabled = false;
        }
        
        if (health > 0)
        {
            timer += Time.deltaTime;
        }
        else
        {
            player.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (isTime == true)
        {
            spawnTime = Random.Range(1.0f, 3.0f);
            isTime = false;
        }

        if (timer >= spawnTime)
        {
            where = Random.Range(0.0f, 25.0f);
            if (where <= 20.0f)
            {
                float side = Random.Range(-1.0f, 1.0f);
                bool flip;

                if (side > 0)
                {
                    side = Mathf.Ceil(side);
                    flip = false;
                }
                else
                {
                    side = Mathf.Floor(side);
                    flip = true;
                }
                CreateEnemy(side, flip);
            }
            else
            {
                CreateEnemy(-1.0f, true);
                CreateEnemy(1.0f, false);
            }
            timer = 0;
            isTime = true;
        }
    }

    void CreateEnemy(float side, bool isFlip)
    {
        GameObject newEnemy = Instantiate(enemy, new Vector3(4.5f * side, -1.41f, 0.0f), Quaternion.identity) as GameObject;
        newEnemy.GetComponent<SpriteRenderer>().flipX = isFlip;
    }
}
