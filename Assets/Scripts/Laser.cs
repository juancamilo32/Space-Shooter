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
        CalculateMovement();
    }

    // Function related to the movement of the laser
    void CalculateMovement()
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

}
