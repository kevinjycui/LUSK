using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalactite: MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public LayerMask snailLayer;
    [SerializeField] private float distanceFall;
    [SerializeField] private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        if ((Mathf.Abs(transform.position.x - player.position.x) < 0.5) && (Mathf.Abs(transform.position.z - player.position.z) < 0.5)){
            rigid.useGravity = true;
        }
       
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject.Destroy(gameObject);
    }

}
