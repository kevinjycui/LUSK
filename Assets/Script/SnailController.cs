using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    [SerializeField, HideInInspector]
    bool isHidden;

    [Header("References")]
    private Transform orientation;
    public Rigidbody rigidBody;
    public LayerMask wallLayer;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private Transform lastWall;
    private Vector3 lastWallNormal;
    public float minWallNormalAngleChange;

    [Header("Exiting")]
    public bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Movement")]
    [SerializeField]
    float speed = 2f;
    [SerializeField,HideInInspector]
    private bool climbing;


    // Update is called once per frame
    void Update()
    {
        WallCheck();
        HandleMovement();
    }

    void HandleMovement()
    {

        float horizontalMove = Input.GetAxis("Horizontal");

        if (horizontalMove != 0)
        {
            float horizontalVelocity = horizontalMove * speed;
            transform.position = new Vector3(transform.position.x + Time.deltaTime * horizontalVelocity, transform.position.y, transform.position.z);
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, wallLayer);
    }

    private void ClimbOn()
    {
        climbing = true;
    }

    private void HandleClimb()
    {
        rigidBody.velocity = new Vector3(rigidBody.)
    }

    private void ClimbOff()
    {
        climbing = false;
    }

}
