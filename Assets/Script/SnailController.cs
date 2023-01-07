using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    [Header("Movement")]
    [SerializeField]
    float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

    }

    void HandleMovement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if(horizontalMove != 0 && verticalMove != 0)
        {
            Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
            controller.Move(speed * Time.deltaTime * move);

        }
    }
}
