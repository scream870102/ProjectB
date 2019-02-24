using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class IPlayer : MonoBehaviour {
    public RhythmTimer rhythmTimer = null;
    PlayerInput input;
    PlayerAnimation anim;
    public PlayerInputInfo[] InputResults { get { return input.InputResults; } }
    void Awake ( ) {
        input = GetComponent<PlayerInput> ( );
        input.Parent = this;
        anim=GetComponent<PlayerAnimation>();
        anim.Parent=this;
        input.PlayerInputString="Player1";
        rhythmTimer=GameObject.Find("AudioSource").GetComponent<RhythmTimer>();
    }
    void Start ( ) {

    }

    // Update is called once per frame
    void Update ( ) {

    }
    public void Init(Vector2Int startPos){

    }
}
