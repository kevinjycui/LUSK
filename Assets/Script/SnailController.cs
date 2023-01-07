using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    [SerializeField, HideInInspector]
    bool isHidden;

    [Header("References")]
    private Rigidbody rigidBody;
    [SerializeField]
    LayerMask wallLayer;

    [Header("Detection")]
    [SerializeField]
    float detectionLength;
    [SerializeField]
    float sphereCastRadius;

    private RaycastHit frontWallHit;
    private bool wallFront;

    [Header("Movement")]
    [SerializeField]
    float speed = 5f;
    [SerializeField,HideInInspector]
    private bool climbing;


    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        StateMachine();
        if (climbing)
        {
            HandleClimb();
        }
        HandleMovement();
    }

    void HandleMovement()
    {

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove != 0 || verticalMove != 0)
        {
            float horizontalVelocity = horizontalMove * speed;
            float verticalVelocity = verticalMove * speed;

            transform.localPosition += transform.forward * Time.deltaTime * verticalVelocity;
            transform.Rotate(0f, Time.deltaTime * horizontalVelocity * 10, 0f);
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, transform.forward, out frontWallHit, detectionLength, wallLayer);
    }

    private void ClimbOn()
    {
        climbing = true;
    }

    private void HandleClimb()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 2*speed, rigidBody.velocity.z);
    }

    private void ClimbOff()
    {
        climbing = false;
    }

    private void StateMachine()
    {
        // Climbing State
        if (wallFront && Input.GetAxis("Vertical") != 0) 
        {
            if (!climbing) { ClimbOn(); }
        }

        // Other State
        else
        {
            if(climbing) { ClimbOff(); }
        }
    }
}
