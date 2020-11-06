using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float _speed = 5;
    private Rigidbody2D playerBody;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement != Vector3.zero)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        playerBody.MovePosition(
            transform.position + movement.normalized * _speed * Time.deltaTime
            );
        //Debug.Log(movement * _speed * Time.deltaTime);
    }
}
