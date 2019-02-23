using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    protected IPlayer parent = null;
    public IPlayer Parent { set { if (parent == null) parent = value; } }
    enum EPlayerState
    {
        IDLE,
        WALK,
    }
    protected bool bGetInput;
    protected Animator anim;
    private EPlayerState state;
    private List<PlayerInputInfo> InputResults=new List<PlayerInputInfo>();
    private void Awake() {
        anim=GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bGetInput=false;   
    }

    // Update is called once per frame
    void Update()
    {
        if(isTime()){
            state=EPlayerState.WALK;
        }
        else
            state=EPlayerState.IDLE;
        switch (state)
        {
            
            case EPlayerState.IDLE:
                bGetInput=false;
                break;
            case EPlayerState.WALK:
                if(!bGetInput){
                    bGetInput=true;
                    InputResults.Clear();
                    InputResults.AddRange(parent.InputResults);
                }

                break;
        }
    }

    bool isTime(){
        return true;
    }

}
