using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 3;

    float speedBoosted = 5;

    public static bool boosted = false;

    [SerializeField]
    GameObject explosionPrefab;

    void Start()
    {
        Vector2 position = new();
        position.y = Camera.main.orthographicSize + 1;

        position.x = Random.Range(-5f, 5f);

        transform.position = position;

        boosted = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.down;
        
        if (boosted == false)
        {
            transform.Translate(movement * speed * Time.deltaTime);
        }
        if (boosted == true)
        {
            transform.Translate(movement * speedBoosted * Time.deltaTime);
        }

        if (transform.position.y < -Camera.main.orthographicSize - 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        ShipController controller = player.GetComponent<ShipController>();

        controller.AddPoints(250);  
    }

}
