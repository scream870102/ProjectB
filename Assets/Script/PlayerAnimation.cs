using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour {
    public int MAX_STEPS = 8;
    public float BASIC_CELL_SIZE = 50.0f;
    protected IPlayer parent = null;
    public IPlayer Parent { set { if (parent == null) parent = value; } }
    enum EPlayerState {
        IDLE,
        WALK,
        ANIM,
    }
    protected bool bGetInput;
    protected Animator anim;
    private EPlayerState state;
    private List<PlayerInputInfo> InputResults = new List<PlayerInputInfo> ( );
    private bool bFcolorFin;

    protected int currentSteps;

    public Vector2Int playerPos;
    private void Awake ( ) {
        anim = GetComponent<Animator> ( );
    }

    // Start is called before the first frame update
    void Start ( ) {
        bGetInput = false;
    }

    // Update is called once per frame
    void Update ( ) {
        if (isTime ( )) {
            state = EPlayerState.WALK;
        }
        else
            state = EPlayerState.IDLE;
        switch (state) {

            case EPlayerState.IDLE:
                bGetInput = false;
                break;
            case EPlayerState.WALK:
                if (!bGetInput) {
                    bGetInput = true;
                    InputResults.Clear ( );
                    InputResults.AddRange (parent.InputResults);
                    currentSteps = 0;
                }
                if (!bFcolorFin) {
                    Walk (InputResults [1].color);

                }
                else if (bFcolorFin) {
                    Walk (InputResults [1].color);
                }

                break;
            case EPlayerState.ANIM:
                break;
        }
    }

    bool isTime ( ) {
        return true;
    }

    bool Walk (EColor color) {
        bool bNextExist = false;
        if (GameManager.instance.maps [playerPos.x, playerPos.y - 1] == color) {
            currentSteps++;
            playerPos += new Vector2Int (0, -1);
            bNextExist=FindNext(playerPos,new Vector2Int(playerPos.x,playerPos.y+1),color);
        }
        else if (GameManager.instance.maps [playerPos.x - 1, playerPos.y] == color) {
            currentSteps++;
            playerPos += new Vector2Int (-1, 0);
            bNextExist=FindNext(playerPos,new Vector2Int(playerPos.x+1,playerPos.y),color);
        }
        else if (GameManager.instance.maps [playerPos.x, playerPos.y + 1] == color) {
            currentSteps++;
            playerPos += new Vector2Int (0, 1);
            bNextExist=FindNext(playerPos,new Vector2Int(playerPos.x,playerPos.y-1),color);
        }
        else if (GameManager.instance.maps [playerPos.x + 1, playerPos.y] == color) {
            currentSteps++;
            playerPos += new Vector2Int (1, 0);
            bNextExist=FindNext(playerPos,new Vector2Int(playerPos.x-1,playerPos.y),color);
        }
        return bNextExist;

    }

    bool FindNext (Vector2Int currentPos, Vector2Int beforePos, EColor color) {
        if (GameManager.instance.maps [currentPos.x, currentPos.y - 1] == color && new Vector2Int (currentPos.x, currentPos.y - 1) != beforePos)
            return true;

        else if (GameManager.instance.maps [currentPos.x - 1, currentPos.y] == color && new Vector2Int (currentPos.x - 1, currentPos.y) != beforePos)
            return true;

        else if (GameManager.instance.maps [currentPos.x, currentPos.y + 1] == color && new Vector2Int (currentPos.x, currentPos.y + 1) != beforePos)
            return true;

        else if (GameManager.instance.maps [currentPos.x + 1, currentPos.y] == color && new Vector2Int (currentPos.x + 1, currentPos.y) != beforePos)
            return true;

        else
            return false;
    }





}
