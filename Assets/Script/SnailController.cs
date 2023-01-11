using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private LayerMask boundaryMask;
    [Header("Movement")]
    [SerializeField]
    public float speed = 5f;
    public float rotationalSpeed = 120f;

    private Vector3 targetNormal;

    void Start()
    {
        MoveToGround();
        targetNormal = transform.up;
    }

    // Update is called once per frame
    void Update()
    {

        WallCheck();
        CliffCheck();

        HandleTurning();
        // MoveToGround();

        // Debug.Log(targetAngleChange);

        Quaternion targetRotation = TurretLookRotation(transform.forward, targetNormal);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);

        float detectionLength = 2.6f;
        Ray ray = new Ray();
        ray.origin = transform.position + transform.up;
        ray.direction = transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, detectionLength, boundaryMask)) {
            return;
        }

        HandleMovement();
    }

    void HandleMovement()
    {

        float verticalMove = Input.GetAxisRaw("Vertical");

        if (verticalMove > 0) {
            float verticalVelocity = verticalMove * speed;
            transform.Translate(0, 0, Time.deltaTime * verticalVelocity);
        }
    }

    void HandleTurning()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");

        if (horizontalMove != 0 ){
            float horizontalVelocity = horizontalMove * speed;
            transform.Rotate(0f, Time.deltaTime * horizontalVelocity * 15, 0f);
        }
        // transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f));
    }

    private void WallCheck()
    {
        float detectionLength = 0.6f;
        RaycastHit frontWallHit;
        float frontHitAngle;

        Ray ray = new Ray();
        ray.origin = transform.position + transform.forward + transform.up;
        ray.direction = transform.forward;
        Debug.DrawRay(ray.origin, ray.direction * detectionLength, Color.green, 2, false);
        if (Physics.Raycast(ray, out frontWallHit, detectionLength, wallMask)) {
            // Debug.Log("up");
            // frontHitAngle = Vector3.Angle(transform.up, frontWallHit.normal);
            
            // Debug.Log(frontHitAngle);
            
            // transform.Rotate(targetAngleChange, 0f, 0f);
            // targetAngleChange = -frontHitAngle;

            transform.Rotate(-Vector3.Angle(transform.up, frontWallHit.normal), 0f, 0f);

            // transform.up = frontWallHit.normal;

            transform.position = frontWallHit.point;

            MoveToGround();

            // float angle = Vector3.SignedAngle(Vector3.right, frontWallHit.normal, Vector3.up);
            // // Debug.Log(angle);
            // transform.Rotate(0f, angle-90, 0f);
        }
        // Debug.Log(frontHitAngle);
    }
    
    private bool GroundCheck()
    {
        float detectionLength = 4f;
        float sphereCastRadiusGround = 3f;

        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = -transform.up;
        Debug.DrawRay(ray.origin, ray.direction * detectionLength, Color.white, 2, false);
        return Physics.SphereCast(ray, sphereCastRadiusGround, detectionLength, wallMask);
    }

    private Quaternion TurretLookRotation(Vector3 approximateForward, Vector3 exactUp) {
        Quaternion zToUp = Quaternion.LookRotation(exactUp, -approximateForward);
        Quaternion yToz = Quaternion.Euler(90, 0, 0);
        return zToUp * yToz;
    }

    private void MoveToGround()
    {
        // if (GroundCheck()) return;

        // Debug.Log("ground");

        Ray ray = new Ray();
        ray.origin = transform.position + transform.up;
        ray.direction = -transform.up;
        
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, wallMask);

        transform.position = hit.point;
        targetNormal = hit.normal;
    }

    private void CliffCheck()
    {
        float detectionLength = 3.2f;
        float detectionLength2 = 0.5f;
        float sphereCastRadiusGround = 2f;

        RaycastHit groundHit;
        float nearCliffAngle;

        Ray ray = new Ray();
        ray.origin = transform.position + transform.up;
        ray.direction = -transform.up;
        Debug.DrawRay(ray.origin, ray.direction * detectionLength, Color.red, 2, false);

        Ray ray2 = new Ray();
        ray2.origin = ray.origin + ray.direction * 2f;
        ray2.direction = -transform.forward;
        Debug.DrawRay(ray2.origin, ray2.direction * detectionLength2, Color.blue, 2, false);

        RaycastHit tmp;

        if (!Physics.Raycast(ray, out tmp, detectionLength, wallMask)) {
            // Debug.Log("cliff");
            if (Physics.SphereCast(ray2, sphereCastRadiusGround, out groundHit, detectionLength2, wallMask)) {
                // Debug.Log("down");

                // Ray ray3 = new Ray();
                // ray3.origin = ray2.origin;
                // ray3.direction = groundHit.point - ray2.origin;
                // Physics.Raycast(ray3, out groundHit, detectionLength2 * 2f, wallMask);

                // nearCliffAngle = Vector3.Angle(transform.forward, groundHit.normal);

                // Debug.Log(nearCliffAngle);

                // transform.Rotate(targetAngleChange, 0f, 0f);
                // targetAngleChange = 90-nearCliffAngle;

                transform.Rotate(90-Vector3.Angle(transform.forward, groundHit.normal), 0f, 0f);

                transform.position = groundHit.point;

                MoveToGround();

                // float angle = Vector3.SignedAngle(Vector3.right, groundHit.normal, Vector3.up);
                // transform.Rotate(0f, angle+90, 0f);
            }
        }

    }

}
