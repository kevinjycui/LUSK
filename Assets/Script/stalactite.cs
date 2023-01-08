using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stalactite: MonoBehaviour
{
    private Transform player;
    private Transform transform;
    [SerializeField] public LayerMask snailLayer;
    private Rigidbody rigid;
    [SerializeField] FieldManager fm;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        transform = gameObject.transform;
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        if ((Mathf.Abs(transform.position.x - player.position.x) < 0.5) && (Mathf.Abs(transform.position.z - player.position.z) < 0.5) && (0 < (transform.position.y - player.position.y)) && (transform.position.y - player.position.y) < 12.26){
            rigid.useGravity = true;
        }
       
    }

    void OnCollisionEnter(Collision collision)
    {
        
        gameObject.SetActive(false);
        fm.stal_fell = true;

    }

}
