using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float speed = 5f;
    

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (horizontalMove != 0 || verticalMove != 0)
        {
            float horizontalVelocity = horizontalMove * speed;
            float verticalVelocity = verticalMove * speed;

            transform.localPosition += transform.forward * Time.deltaTime * verticalVelocity;
            transform.Rotate(0f, Time.deltaTime * horizontalVelocity * 5, 0f);
        }
    }

}
