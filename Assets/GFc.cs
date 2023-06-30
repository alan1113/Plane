using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using TMPro;


public class GFc : MonoBehaviour
{
    public static int f;
    public TMP_Text gf_text;
    public static int th;
    public TMP_Text thrust_text;
    public static int hi;
    public TMP_Text hi_text;
    public TMP_Text s_text;
    public TMP_Text end_text;
    // Start is called before the first frame update
    void Start()
    {
        f = 0;
        th=0;
        hi=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!a10.dead){
        gf_text.text="GF: "+ f;
        thrust_text.text="Thrust: "+ th+"%";
        hi_text.text="High: "+ hi+"m";
        s_text.text="Score: "+ Ground.score;
        end_text.text="";
        }
        else{
            gf_text.text="";
            thrust_text.text="";
            hi_text.text="";
            end_text.text="END";
        }
    }
}
