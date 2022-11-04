using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    // Laser speed
    [SerializeField]
    float speed = 15f;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Laser"))
        {
            MoveUp();
        }
        else if (gameObject.CompareTag("EnemyLaser"))
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (transform.position.y > 8)
        {
            if (transform.parent)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.y < -8)
        {
            if (transform.parent)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("EnemyLaser"))
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                player.TakeDamage();
            }
            Destroy(gameObject);
        }
    }

}
