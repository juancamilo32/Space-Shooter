using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    float movementSpeed = 3f;

    [SerializeField]
    int powerUpID; // Triple shot = 0, Speed = 1, Shield = 2

    [SerializeField]
    AudioClip powerUpSFX;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * movementSpeed);
        if (transform.position.y <= -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                AudioSource.PlayClipAtPoint(powerUpSFX, new Vector3(0, 1, -10), 0.4f);
                switch (powerUpID)
                {
                    case 0:
                        player.ActivateTripleShot();
                        break;
                    case 1:
                        player.ActivateSpeed();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                };
            }
            Destroy(gameObject);
        }
    }


}
