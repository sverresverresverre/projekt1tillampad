using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShipController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float speed = 0.02f;

    [SerializeField]
    GameObject boltPrefab;

    [SerializeField]
    Transform gunPosition;

    [SerializeField]
    float timeBetweenShots = 0.5f;
    float timeSinceLastShot = 0;

    [SerializeField]
    int maxHealth = 10;
    public int currentHealth;

    [SerializeField]
    Slider hpBar;

    [SerializeField]
    int shieldMaxHealth = 5;
    int ShieldCurrentHealth;

    [SerializeField]
    Slider shieldBar;

    [SerializeField]
    TMP_Text pointsText;

    [SerializeField]
    TMP_Text shieldRegen;

    [SerializeField]
    float timeBetweenShield;

    [SerializeField]
    float timeSinceLastShield;

    public static int points = 0;

    void Start()
    {
        currentHealth = maxHealth;
        hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;

        ShieldCurrentHealth = shieldMaxHealth;
        shieldBar.maxValue = shieldMaxHealth;
        shieldBar.value = ShieldCurrentHealth;

        AddPoints(0);
        points = 0;

        pointsText.text = points.ToString();
    }

    public void Damage()
    {
        currentHealth--;
        hpBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(xInput, yInput) * speed * Time.deltaTime;
        // Vector2 movement2 = new Vector2(0, 1) * speed * yInput;

        transform.Translate(movement);

        timeSinceLastShot += Time.deltaTime;

        timeSinceLastShield += Time.deltaTime;

        if (Input.GetAxisRaw("Fire1") > 0 && timeSinceLastShot > timeBetweenShots)
        {
            timeSinceLastShot = 0;
            Instantiate(boltPrefab, gunPosition.position, Quaternion.identity);
        }

        if (timeSinceLastShield > timeBetweenShield)
        {
            ShieldCurrentHealth++;

            shieldBar.value = ShieldCurrentHealth;

            timeSinceLastShield = 0;

            if (ShieldCurrentHealth >= 5)
            {
                ShieldCurrentHealth = 5;
            }
        }

        if (points >= 5000)
        {
            EnemyController.boosted = true;

            EnemySpawnController.boosted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            ShieldCurrentHealth--;
            shieldBar.value = ShieldCurrentHealth;

            if(ShieldCurrentHealth < 0)
            {
                Damage();
            }
        }
    }

    public void AddPoints(int amount)
    {
        points += amount;
        pointsText.text = points.ToString();
    }
}
