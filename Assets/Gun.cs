using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public GameObject bullet;

    [SerializeField]
    private float speed = 800f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKey("space")&&!a10.dead) {Rigidbody bulletCopy = (Rigidbody)Instantiate(bullet.GetComponent<Rigidbody>(), muzzle.position, muzzle.rotation);
        bulletCopy.velocity = bulletCopy.transform.TransformDirection(Vector3.forward * speed);
        }
        
    }
}
