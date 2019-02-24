using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour {
    public float BASIC_CELL_SIZE = 50.0f;
    public float ANIMATE_TIME = 2.0f;
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
    private bool bFcolorFin = false;
    private bool bScolorFin = false;
    [SerializeField]
    protected List<Vector2Int> steps = new List<Vector2Int> ( );

    public Vector2Int playerPos;
    protected Vector2Int tmpPlayerPos;
    protected Vector2Int tmpBeforePos;
    protected Vector2 direction;
    protected Vector2Int destination;
    protected Vector2 destinationPos;
    protected Vector2 playerWorldPos;
    protected float timer;
    private void Awake ( ) {
        anim = GetComponent<Animator> ( );
    }

    // Start is called before the first frame update
    void Start ( ) {
        bGetInput = false;
    }

    // Update is called once per frame
    void Update ( ) {
        if (!parent.rhythmTimer.IsInputTime && state == EPlayerState.IDLE) {

            state = EPlayerState.WALK;
        }
        else if (parent.rhythmTimer.IsInputTime)
            state = EPlayerState.IDLE;
        switch (state) {

            case EPlayerState.IDLE:
                bGetInput = false;
                break;
            case EPlayerState.WALK:
                if (!bGetInput) {
                    tmpPlayerPos = playerPos;
                    bGetInput = true;
                    InputResults.Clear ( );
                    bFcolorFin = false;
                    bScolorFin = false;
                    InputResults.AddRange (parent.InputResults);
                    //InputResults.AddRange (testInputResults);
                    steps.Clear ( );
                }
                else if (!bFcolorFin) {
                    bFcolorFin = !Walk (InputResults [0].color);
                }
                else if (bFcolorFin && !bScolorFin) {
                    bScolorFin = !Walk (InputResults [1].color);
                }
                if (bFcolorFin && bScolorFin) {
                    state = EPlayerState.ANIM;
                    destination = CountDestination ( );
                    playerWorldPos = new Vector2 (playerPos.x * BASIC_CELL_SIZE, -playerPos.y * BASIC_CELL_SIZE);
                    destinationPos = new Vector2 (destination.x * BASIC_CELL_SIZE, -destination.y * BASIC_CELL_SIZE);
                    transform.position = playerWorldPos;
                    timer = .0f;
                    playerPos=destination;
                }

                break;
            case EPlayerState.ANIM:
                transform.position = Vector2.Lerp (transform.position, destinationPos, timer);
                timer += Time.deltaTime * 0.5f;
                break;
        }
    }

    bool isTime ( ) {
        return true;
    }

    bool Walk (EColor color) {
        bool bNextExist = false;
        if (tmpPlayerPos.y > 0 && tmpBeforePos != new Vector2Int (0, 1) && (EColor) GameManager.instance.Maps [tmpPlayerPos.y - 1, tmpPlayerPos.x] == color) {
            steps.Add (new Vector2Int (0, -1));
            tmpPlayerPos = new Vector2Int (tmpPlayerPos.x + 0, tmpPlayerPos.y - 1);
            bNextExist = FindNext (tmpPlayerPos, new Vector2Int (tmpPlayerPos.x + 0, tmpPlayerPos.y + 1), color);
            tmpBeforePos = new Vector2Int (0, -1);
        }
        else if (tmpPlayerPos.x > 0 && tmpBeforePos != new Vector2Int (1, 0) && (EColor) GameManager.instance.Maps [tmpPlayerPos.y, tmpPlayerPos.x - 1] == color) {
            steps.Add (new Vector2Int (-1, 0));
            tmpPlayerPos = new Vector2Int (tmpPlayerPos.x - 1, tmpPlayerPos.y + 0);
            bNextExist = FindNext (tmpPlayerPos, new Vector2Int (tmpPlayerPos.x + 1, tmpPlayerPos.y + 0), color);
            tmpBeforePos = new Vector2Int (-1, 0);
        }
        else if (tmpPlayerPos.y < 49 && tmpBeforePos != new Vector2Int (0, -1) && (EColor) GameManager.instance.Maps [tmpPlayerPos.y + 1, tmpPlayerPos.x] == color) {
            steps.Add (new Vector2Int (0, 1));
            tmpPlayerPos = new Vector2Int (tmpPlayerPos.x + 0, tmpPlayerPos.y + 1);
            bNextExist = FindNext (tmpPlayerPos, new Vector2Int (tmpPlayerPos.x + 0, tmpPlayerPos.y - 1), color);
            tmpBeforePos = new Vector2Int (0, 1);
        }
        else if (tmpPlayerPos.x < 49 && tmpBeforePos != new Vector2Int (-1, 0) && (EColor) GameManager.instance.Maps [tmpPlayerPos.y, tmpPlayerPos.x + 1] == color) {
            steps.Add (new Vector2Int (1, 0));
            tmpPlayerPos = new Vector2Int (tmpPlayerPos.x + 1, tmpPlayerPos.y + 0);
            bNextExist = FindNext (tmpPlayerPos, new Vector2Int (tmpPlayerPos.x - 1, tmpPlayerPos.y + 0), color);
            tmpBeforePos = new Vector2Int (1, 0);
        }
        return bNextExist;

    }

    bool FindNext (Vector2Int currentPos, Vector2Int beforePos, EColor color) {
        if (currentPos.y > 0 && new Vector2Int (currentPos.x, currentPos.y - 1) != beforePos && (EColor) GameManager.instance.Maps [currentPos.y - 1, currentPos.x] == color)
            return true;

        else if (currentPos.x > 0 && new Vector2Int (currentPos.x - 1, currentPos.y) != beforePos && (EColor) GameManager.instance.Maps [currentPos.y, currentPos.x - 1] == color)
            return true;

        else if (currentPos.y < 49 && new Vector2Int (currentPos.x, currentPos.y + 1) != beforePos && (EColor) GameManager.instance.Maps [currentPos.y + 1, currentPos.x] == color) {
            return true;
        }
        else if (currentPos.x < 49 && new Vector2Int (currentPos.x + 1, currentPos.y) != beforePos && (EColor) GameManager.instance.Maps [currentPos.y, currentPos.x + 1] == color)
            return true;

        else
            return false;
    }
    Vector2Int CountDestination ( ) {
        Vector2Int goal = new Vector2Int (0, 0);
        foreach (Vector2Int item in steps) {
            goal = new Vector2Int (goal.x + item.x, goal.y + item.y);
        }
        return new Vector2Int (playerPos.x + goal.x, playerPos.y + goal.y);
    }
}
