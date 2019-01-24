using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    public GameObject enemy;

    float timer = 0;
    bool isTime = true;
    float spawnTime;

    float where;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

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
