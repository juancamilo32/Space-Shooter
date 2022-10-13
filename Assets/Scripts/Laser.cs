using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    // Laser speed
    [SerializeField]
    float speed = 15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (transform.position.y > 8)
        {
            Destroy(gameObject);
        }
    }

}
