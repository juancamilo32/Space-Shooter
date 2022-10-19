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

    [SerializeField]
    bool isTripleShotActive = false;

    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    GameObject tripleShotPrefab;


    SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // Set starting position for the player
        transform.position = Vector3.zero;

        spawnManager = GameObject.FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();
        if (!spawnManager)
        {
            Debug.LogError("Spawn Manager is NULL.");
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

    public void TakeDamage()
    {
        lives--;
        if (lives < 1)
        {
            spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isTripleShotActive = false;
    }

}
