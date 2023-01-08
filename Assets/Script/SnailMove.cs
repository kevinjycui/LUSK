using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMove : MonoBehaviour
{
    [SerializeField] private float speed;

    [Header("Ground")]
    [SerializeField, HideInInspector] bool onGround;
    [SerializeField] LayerMask wallMask;
    [SerializeField] float sphereRadius;
    [SerializeField] float fallingSpeed;

    [Header("Wall")]
    [SerializeField, HideInInspector] RaycastHit wallHit;
    [SerializeField] float maxDistance;
    [SerializeField, HideInInspector] float wallAngle;

    void Start()
    {
        
    }

    void Update()
    {
        /*
        if (!GroundCheck())
        {
            transform.Translate(-transform.up * Time.deltaTime);
        }
        else
        {
            if (WallCheck())
            {
                transform.Rotate(-wallAngle, 0f, 0f);
            }
            else
            {
                HandleMovement();
            }
        }
        */
        if (WallCheck())
        {
            transform.Rotate(-wallAngle, 0f, 0f);
        }
        else
        {
            HandleMovement();
        }

    }

    private bool GroundCheck()
    {
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = -transform.up;
        return Physics.SphereCast(ray, sphereRadius, 0.5f, wallMask);
    }

    private bool WallCheck()
    {
        bool frontWall = Physics.Raycast((transform.position + transform.forward * 0.3f), transform.forward, out wallHit, maxDistance);
        wallAngle = Vector3.Angle(transform.up, wallHit.normal);
        return frontWall;
    }

    void HandleMovement()
    {

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove != 0)
        {
            float horizontalVelocity = horizontalMove * speed;
            transform.Rotate(0f, Time.deltaTime * horizontalVelocity * 15, 0f);
        }
        if (verticalMove > 0)
        {
            float verticalVelocity = verticalMove * speed;
            transform.Translate(0, 0, Time.deltaTime * verticalVelocity);
        }
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
    }
}
