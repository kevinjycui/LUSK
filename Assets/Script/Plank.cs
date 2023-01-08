using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public LayerMask snailLayer;
    [SerializeField] Rigidbody rigid;

    void Start()
    {
        rigid.useGravity = false;
        rigid.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == snailLayer)
        {
            rigid.useGravity = true;
            rigid.isKinematic = false;
        }
    }
}
