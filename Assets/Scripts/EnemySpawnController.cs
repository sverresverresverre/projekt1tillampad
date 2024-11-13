using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject EnemyPrefab;

    [SerializeField]
    float timeBetweenEnemy = 2f;

    [SerializeField]
    float timeSinceLastEnemy = 5;

    public static bool boosted = false;

    void Start()
    {
        boosted = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastEnemy += Time.deltaTime;

        if (boosted == false)
        {
            timeBetweenEnemy = 0.8f;
        }
        if (boosted == true)
        {
            timeBetweenEnemy = 0.5f;
        }

        if (timeSinceLastEnemy > timeBetweenEnemy)
        {
            timeSinceLastEnemy = 0;
            Instantiate(EnemyPrefab);
        }
    }
}
