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

    void Start()
    {
        MoveToGround();
        // targetNormal = transform.up;
    }

    // Update is called once per frame
    void Update()
    {

        WallCheck();
        CliffCheck();

        HandleTurning();
        // MoveToGround();

        // Debug.Log(targetAngleChange);

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
        RaycastHit hit;
        float frontHitAngle;

        Ray ray = new Ray();
        ray.origin = transform.position + transform.forward + transform.up;
        ray.direction = transform.forward;
        Debug.DrawRay(ray.origin, ray.direction * detectionLength, Color.green, 2, false);

        RaycastHit collisionHit;
        Physics.Raycast(ray, out collisionHit, detectionLength);
        if (Physics.Raycast(ray, out hit, detectionLength, wallMask)) {

            if (!GameObject.ReferenceEquals(hit.collider.gameObject, collisionHit.collider.gameObject)) return;

            transform.Rotate(-Vector3.Angle(transform.up, hit.normal), 0f, 0f);

            transform.position = hit.point;
                
            Quaternion targetRotation = TurretLookRotation(transform.forward, hit.normal);
            transform.rotation = targetRotation;
        }
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
    }

    private void CliffCheck()
    {
        float detectionLength = 3.2f;
        float detectionLength2 = 0.5f;
        float sphereCastRadiusGround = 2f;

        RaycastHit hit;
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

        RaycastHit collisionHit;
        Physics.Raycast(ray, out collisionHit, detectionLength);
        if (!Physics.Raycast(ray, out tmp, detectionLength, wallMask)) {
            if (Physics.SphereCast(ray2, sphereCastRadiusGround, out hit, detectionLength2, wallMask)) {

                if (!GameObject.ReferenceEquals(hit.collider.gameObject, collisionHit.collider.gameObject)) return;

                transform.Rotate(90-Vector3.Angle(transform.forward, hit.normal), 0f, 0f);

                transform.position = hit.point;
                
                Quaternion targetRotation = TurretLookRotation(transform.forward, hit.normal);
                transform.rotation = targetRotation;
            }
        }

    }

}
