using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public string PlayerInputString{set{playerInputString=value;}}
    private string playerInputString;
    private List<PlayerInputInfo> inputs=new List<PlayerInputInfo>();
    private bool bReset;

    // Start is called before the first frame update
    void Start()
    {
        bReset=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Istime()){
            
            GetPlayerInput();
            
        }
        else if(!Istime()){
            
        }
    }


    bool Istime(){
        return true;
    }

    void GetPlayerInput(){
        if(!bReset){
            inputs.Clear();
            bReset=true;
        }
        
    }
}
