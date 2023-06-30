using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class a10 : MonoBehaviour
{
    [SerializeField]
    public static bool dead=false;
    [SerializeField]
    float maxThrust=100;
    [SerializeField]
    float nowThrust=100;
    [SerializeField]
    AnimationCurve dragCoefficient;
    [SerializeField]
    AnimationCurve liftCoefficient;
    [SerializeField]
    float adjust=1;
    [SerializeField]
    float delta=1;
    [SerializeField]
    float tdelta=1;
    public Rigidbody rb { get; private set; }
    float anglex;
    Vector3  velocity;
    Vector3  LocalVelocity;
    Vector3  LocalAngularVelocity;
    float AngleOfAttack;
    float AngleOfAttackYaw;
    float velocityspeed;
    [SerializeField]
    float Gf=0.0F;
    float pvelocity=0;
    [SerializeField]
    bool cont=true;
    bool b=true;
    Vector3 nv; 
    
    public GameObject mainCamera;
    public GameObject ima;
    //public CanvasGroup canvasGroup;


    //public GFc gfs= GetComponent<GFc>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //mainCamera =GameObject.Find("Camera");
        //ima =GameObject.Find("image");
        //canvasGroup = mainCamera.GetComponent<CanvasGroup>();
        
    }

    void thrust() {
        
        rb.AddRelativeForce(nowThrust * Vector3.forward);
        GFc.th=(int)(nowThrust/maxThrust*100);
    }

    void lift(){
        if(rb.transform.position.y>13500)return ;
        //var vs=velocity.x * velocity.x + velocity.z * velocity.z;
        var vs = velocity.sqrMagnitude;
        var coefficient = liftCoefficient.Evaluate(anglex);
        adjust =0.005F;
        var liftForce=vs * coefficient * adjust;
        //Debug.Log("liftForce");
        //Debug.Log(liftForce);
        rb.AddRelativeForce(liftForce*Vector3.up);
    }

    void drag(){
        var vs = velocity.sqrMagnitude;
        
        var coefficient = liftCoefficient.Evaluate(anglex);
        var dadjust =adj()*0.25F;

        //var zd=Mathf.Abs(transform.eulerAngles.z);
        var zd=0.9;

      

        //Debug.Log(dadjust);
        var ldcoefficient = coefficient * coefficient;
        var dragForce= vs * (float)(dadjust+0.07);
        
        var inducedDrag = vs * ldcoefficient* (float)(dadjust+zd);
        nv = velocity.normalized;
        if (velocity.y < 0) {
             
            nv = new Vector3(nv.x, 0, nv.z); 
        }

        //Debug.Log("drag");
        //Debug.Log(ldcoefficient);
        //Debug.Log(anglex);
        Debug.Log(nv);
        if(velocityspeed>5){
        rb.AddForce(-dragForce*nv);
        rb.AddForce(-inducedDrag*nv);}
    }
    
    void OnCollisionExit(Collision other){ 
        cont = true;
    }

    // void AoA() {
    //     AngleOfAttack = Mathf.Atan2(-LocalVelocity.y, LocalVelocity.z);
    //     AngleOfAttackYaw = Mathf.Atan2(LocalVelocity.x, LocalVelocity.z);
    // }




    private void Boom()
{
    int childCount = transform.childCount;
    for (int i = childCount - 1; i >= 0; i--)
    {
        Transform childTransform = transform.GetChild(i);
        if (childTransform.gameObject.tag == "Boom")
        {
            Rigidbody existingRigidbody = childTransform.gameObject.GetComponent<Rigidbody>();
            if (existingRigidbody == null)
            {
                childTransform.gameObject.AddComponent<Rigidbody>();
            }
        }
        else
        {
            childTransform.position = new Vector3(childTransform.position.x, 50f, childTransform.position.z);
            childTransform.rotation = Quaternion.Euler(90f, childTransform.rotation.eulerAngles.y, childTransform.rotation.eulerAngles.z);
            childTransform.parent = null;
        }
    }
}




    
    void Die(){
        Debug.Log("Die");
        if(!dead){
            
        }
        dead=true;
        
        }

    
    

    void OnCollisionEnter(Collision other){//cont = false;
    if(other.gameObject.tag!="Track"&&other.gameObject.tag!="Bullet"){
        Die();
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y+20f, transform.position.z);
        Boom();
        Color imageColor = ima.GetComponent<Image>().color;

        imageColor.r = 0f;
        imageColor.a = 0f;

        ima.GetComponent<Image>().color = imageColor;
        Debug.Log("1");
     }}

     


    float adj(){
        var dxadjust =0.0005F;
        var dyadjust =0.0F;
        if(velocity.y>0){dyadjust =0.015F;}
        else {rb.AddForce(new Vector3(0,velocity.y * dyadjust,0));}
        var dzadjust =0.0005F;
        var result =new Vector3(velocity.x * dxadjust , velocity.y * dyadjust, velocity.z * dzadjust);
        return (float)result.magnitude;
        
    }
    

    void Gforce(){
        Gf=(velocityspeed-pvelocity);
        //if(Gf<0.1)Gf=0;
        GFc.f=(int)Gf;
        if(!dead&&Gf>9){
            dead=true;
            Color imageColor = ima.GetComponent<Image>().color;

            imageColor.a = 0.9f;

            ima.GetComponent<Image>().color = imageColor;
        }else if(!dead&&Gf<-4){
            dead=true;
            Color imageColor = ima.GetComponent<Image>().color;

            imageColor.r = 0.7f;
            imageColor.a = 0.9f;

            ima.GetComponent<Image>().color = imageColor;
        }
        Debug.Log(velocityspeed);
        Debug.Log(pvelocity);
        Debug.Log(Gf);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( Input.GetKey("1")) {  delta=1;  }
        if ( Input.GetKey("2")) {  delta=5;  }
        if ( Input.GetKey("3")) {  delta=10;  }
        if ( Input.GetKey("4")) {  delta=15;  }
        if ( Input.GetKey("5")) {  delta=20;  }
        if ( Input.GetKey("6")) {  delta=30;  }
        if ( Input.GetKey("7")) {  delta=40;  }
        if ( Input.GetKey("8")) {  delta=50;  }
        if ( Input.GetKey("9")) {  delta=70;  }
        if ( Input.GetKey("0")) {  delta=90;  }

        




        if ( Input.GetKey("up")) {  
            if ( nowThrust+delta<=maxThrust) {nowThrust+=delta;  }
            else {nowThrust=maxThrust;}
        }

        if ( Input.GetKey("down")) { if ( nowThrust-delta>=0)nowThrust-=delta;  
            else {nowThrust=0;}
        }
        

        if ( Input.GetKey("left")) {  //rb.AddTorque(new Vector3( 0, 0, 0.01F), ForceMode.Force);   
            transform.Rotate( 0, -0.1F, 0 );   
        }
        if ( Input.GetKey("right")) {  //rb.AddTorque(new Vector3( 0, 0, -0.01F), ForceMode.Force);  
            transform.Rotate( 0, 0.1F, 0 ); 
        }

        if ( Input.GetKey("w")&&cont) {  transform.Rotate( -0.01F*5, 0,0 );}
        if ( Input.GetKey("s")&&cont) {  transform.Rotate( 0.01F*5, 0,0 );  }
        if ( Input.GetKey("a")&&cont) {transform.Rotate( 0, 0,0.01F*10);   }
        if ( Input.GetKey("d")&&cont) {  transform.Rotate( 0, 0,-0.01F*10);  }

        var dt = Time.deltaTime;
        velocity = rb.velocity;
        velocityspeed=velocity.magnitude;
        
        //Debug.Log("V");
        //Debug.Log(velocity);
        //Debug.Log(velocity.magnitude);
        //Debug.Log(velocity.y);
        anglex =-transform.eulerAngles.x;
        if(anglex<-180){
            anglex+=360;
        }
        GFc.hi=(int)transform.position.y;
        
        
        if(!dead){
        Gforce();
        thrust();
        
        lift();
        drag();
        }
        pvelocity = velocity.magnitude;
    }


}