using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField]
    public static int score = 0;
    private float speed = 800f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * speed);
    }
    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag=="Target"){
            ++score;
        }
        if(other.gameObject.tag=="Terrain"||other.gameObject.tag=="Track"||other.gameObject.tag=="Target"){
        Destroy(gameObject);            
        }
        

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
