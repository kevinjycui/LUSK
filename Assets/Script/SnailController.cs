using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    [SerializeField, HideInInspector]
    bool isHidden;
   

    [Header("Movement")]
    [SerializeField]
    float speed = 2f;
    [SerializeField, HideInInspector]
    float x_pos = 0f;
    

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {

        float horizontalMove = Input.GetAxis("Horizontal");

        if (horizontalMove != 0)
        {
            float horizontalVelocity = horizontalMove * speed;
            x_pos += Time.deltaTime * horizontalVelocity;
            transform.position = new Vector3(x_pos, 0f, 0f);
        }
    }

}
