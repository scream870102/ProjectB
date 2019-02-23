using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour {
    public int MAX_STEPS = 8;
    public float BASIC_CELL_SIZE = 50.0f;
    protected IPlayer parent = null;
    public IPlayer Parent { set { if (parent == null) parent = value; } }

    [System.Serializable]
    enum EPlayerState {
        IDLE,
        WALK,
        ANIM,
    }
    protected bool bGetInput;
    protected Animator anim;
    [SerializeField]
    private EPlayerState state;
    [SerializeField]
    private List<PlayerInputInfo> InputResults = new List<PlayerInputInfo> ( );
    private bool bFcolorFin;
    [SerializeField]
    protected List<Vector2Int> steps = new List<Vector2Int> ( );

    public Vector2Int playerPos;
    public Vector2Int tmpPlayerPos;
    public PlayerInputInfo [ ] testInputResults;
    private void Awake ( ) {
        anim = GetComponent<Animator> ( );
    }

    // Start is called before the first frame update
    void Start ( ) {
        bGetInput = false;
    }

    // Update is called once per frame
    void Update ( ) {
        if (!parent.rhythmTimer.IsInputTime) {
            tmpPlayerPos = playerPos;
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
                    //InputResults.AddRange (parent.InputResults);
                    InputResults.AddRange (testInputResults);
                    steps.Clear ( );
                }
                if (!bFcolorFin) {
                    Debug.Log ("A");
                    bFcolorFin = !Walk (InputResults [0].color);
                    Debug.Log ("B");
                }
                else if (bFcolorFin) {
                    Walk (InputResults [1].color);
                }
                state = EPlayerState.ANIM;
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
        if ((tmpPlayerPos.y > 0 ? true : false)  ) {
            Debug.Log("C");
            if((EColor) GameManager.instance.Maps [tmpPlayerPos.x, tmpPlayerPos.y - 1] == color){
                Debug.Log("D");
                steps.Add (new Vector2Int (0, -1));
                //playerPos += new Vector2Int (0, -1);
                bNextExist = FindNext (tmpPlayerPos + new Vector2Int (0, -1), tmpPlayerPos, color);
            }
            
        }
        else if ((tmpPlayerPos.x > 0 ? true : false) && (EColor) GameManager.instance.Maps [tmpPlayerPos.x - 1, tmpPlayerPos.y] == color) {
            steps.Add (new Vector2Int (-1, 0));
            //playerPos += new Vector2Int (-1, 0);
            bNextExist = FindNext (tmpPlayerPos + new Vector2Int (-1, 0), tmpPlayerPos, color);
        }
        else if ((tmpPlayerPos.y < 49 ? true : false) && (EColor) GameManager.instance.Maps [tmpPlayerPos.x, tmpPlayerPos.y + 1] == color) {
            steps.Add (new Vector2Int (0, 1));
            //playerPos += new Vector2Int (0, 1);
            bNextExist = FindNext (tmpPlayerPos + new Vector2Int (0, 1), tmpPlayerPos, color);
        }
        else if ((tmpPlayerPos.x < 49 ? true : false) && (EColor) GameManager.instance.Maps [tmpPlayerPos.x + 1, tmpPlayerPos.y] == color) {
            steps.Add (new Vector2Int (1, 0));
            //playerPos += new Vector2Int (1, 0);
            bNextExist = FindNext (tmpPlayerPos + new Vector2Int (1, 0), tmpPlayerPos, color);
        }
        return bNextExist;

    }

    bool FindNext (Vector2Int currentPos, Vector2Int beforePos, EColor color) {
        if ((EColor) GameManager.instance.Maps [currentPos.x, currentPos.y - 1] == color && new Vector2Int (currentPos.x, currentPos.y - 1) != beforePos)
            return true;

        else if ((EColor) GameManager.instance.Maps [currentPos.x - 1, currentPos.y] == color && new Vector2Int (currentPos.x - 1, currentPos.y) != beforePos)
            return true;

        else if ((EColor) GameManager.instance.Maps [currentPos.x, currentPos.y + 1] == color && new Vector2Int (currentPos.x, currentPos.y + 1) != beforePos)
            return true;

        else if ((EColor) GameManager.instance.Maps [currentPos.x + 1, currentPos.y] == color && new Vector2Int (currentPos.x + 1, currentPos.y) != beforePos)
            return true;

        else
            return false;
    }

}
