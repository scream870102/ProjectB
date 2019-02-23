using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    public int MAX_STEPS=8;
    public float BASIC_CELL_SIZE=50.0f;
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
    
    protected int currentSteps;
    
    public Vector2Int playerPos;
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
                    currentSteps=0;
                }
                foreach (PlayerInputInfo info in InputResults)
                {
                    Walk(info.color);
                }
                break;
        }
    }

    bool isTime(){
        return true;
    }

    void Walk(EColor color){
        if(GameManager.instance.maps[playerPos.x,playerPos.y-1]==color){
            currentSteps++;
            playerPos+=new Vector2Int(0,-1);
        }
        else if(GameManager.instance.maps[playerPos.x,playerPos.y-1]==color){
            currentSteps++;
            playerPos+=new Vector2Int(0,-1);
        }
    }

}
