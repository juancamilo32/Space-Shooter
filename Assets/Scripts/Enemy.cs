using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Movement speed of the enemy
    [SerializeField]
    float movementSpeed = 4f;

    Player player;
    Animator animator;
    BoxCollider2D boxCollider2D;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        if (!player)
        {
            Debug.LogError("Player is NULL.");
        }
        if (!animator)
        {
            Debug.LogError("Animator is NULL.");
        }
        if (!boxCollider2D)
        {
            Debug.LogError("Collider is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * movementSpeed);
        if (transform.position.y <= -5.5f)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                player.TakeDamage();
            }
            animator.SetTrigger("OnEnemyDeath");
            boxCollider2D.enabled = false;
            Destroy(gameObject, 2.6f);
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if (player)
            {
                player.AddScore(10);
            }
            animator.SetTrigger("OnEnemyDeath");
            boxCollider2D.enabled = false;
            Destroy(gameObject, 2.6f);
        }
    }

}
