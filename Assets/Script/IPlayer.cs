using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer : MonoBehaviour
{
    PlayerInput input;
    List<PlayerInputInfo> InputResults{get{return input.InputResults;}}
    void Awake() {
        input=GetComponent<PlayerInput>();
        input.Parent=this;    
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
