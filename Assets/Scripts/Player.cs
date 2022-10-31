using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    float movementSpeed = 8.5f;

    // Map boundary in the x axis
    float xAxisMapLimit = 11.3f;

    // Map boundary in the y axis
    float yAxisMapLimit = -3.8f;

    // Shooting cooldown
    [SerializeField]
    float fireRate = 0.2f;

    // Variable to determine if player can shoot
    float canFire = -1f;

    // Player health
    [SerializeField]
    int lives = 3;
    int score = 0;

    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    GameObject tripleShotPrefab;

    [SerializeField]
    GameObject shield;
    [SerializeField]
    GameObject rightEngine;
    [SerializeField]
    GameObject leftEngine;

    bool isShieldActive = false;
    bool isTripleShotActive = false;


    SpawnManager spawnManager;
    UIManager uIManager;

    // Start is called before the first frame update
    void Start()
    {
        // Set starting position for the player
        transform.position = Vector3.zero;

        uIManager = GameObject.FindObjectOfType<UIManager>().GetComponent<UIManager>();
        spawnManager = GameObject.FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();
        if (!spawnManager)
        {
            Debug.LogError("Spawn Manager is NULL.");
        }
        if (!uIManager)
        {
            Debug.LogError("UI Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        SetMapBoundaries();
        if (Input.GetKey(KeyCode.Space) && Time.time > canFire)
        {
            ShootLaser();
        }
    }

    // Function used to move the player in the x and y axis
    void CalculateMovement()
    {
        // Input for player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementVector = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(movementVector * Time.deltaTime * movementSpeed);
    }

    // Function used to set map boundaries for the player
    void SetMapBoundaries()
    {
        // Map boundaries in y axis
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, yAxisMapLimit, 0), 0);

        // Map boundaries in x axis
        if (transform.position.x >= xAxisMapLimit)
        {
            transform.position = new Vector3(-xAxisMapLimit, transform.position.y, 0);
        }
        else if (transform.position.x <= -xAxisMapLimit)
        {
            transform.position = new Vector3(xAxisMapLimit, transform.position.y, 0);
        }
    }

    // Function used to spawn lasers
    void ShootLaser()
    {
        canFire = Time.time + fireRate;
        Vector3 spawnPosition = transform.position + Vector3.up;
        if (isTripleShotActive)
        {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {

            Instantiate(laserPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Function that controls the damage taken by the player
    public void TakeDamage()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            shield.SetActive(false);
            return;
        }
        
        lives--;
        if (lives == 2)
        {
            rightEngine.SetActive(true);
        }
        else if (lives == 1)
        {
            leftEngine.SetActive(true);
        }

        uIManager.UpdateLives(lives);
        if (lives < 1)
        {
            spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    // Function that activates the triple shot powerup
    public void ActivateTripleShot()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    // Routine to countdown the triple shot powerup
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isTripleShotActive = false;
    }

    // Function that activates the speed powerup
    public void ActivateSpeed()
    {
        movementSpeed *= 2;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    // Routine to countdown the speed powerup
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        movementSpeed /= 2;
    }

    // Function that activates the shield powerup
    public void ActivateShield()
    {
        isShieldActive = true;
        shield.SetActive(true);
    }

    // Function to update the score value and score text
    public void AddScore(int points)
    {
        score += points;
        uIManager.UpdateScore(score);
    }

}
