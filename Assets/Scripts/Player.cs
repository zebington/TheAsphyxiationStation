using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float speed = 3.0f;
    [SerializeField]
    float oxygenTank = 100; // players health

    Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0);
        transform.position += move * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }

        LoseOxygenOverTime();
    }

    void LoseOxygenOverTime()
    {
        oxygenTank -= 1 * Time.deltaTime;
    }
}
