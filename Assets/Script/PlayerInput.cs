using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerInput : MonoBehaviour {
    //temp
    protected IPlayer parent = null;
    public IPlayer Parent { set { if (parent == null) parent = value; } }
    public string PlayerInputString { set { playerInputString = value; } }
    private string playerInputString;
    private List<PlayerInputInfo> inputs = new List<PlayerInputInfo> ( );
    private List<PlayerInputInfo> inputResults = new List<PlayerInputInfo> ( );
    public List<PlayerInputInfo> InputResults { get { return inputResults; } }
    private bool bReset;

    // Start is called before the first frame update
    void Start ( ) {
        bReset = false;
    }

    // Update is called once per frame
    void Update ( ) {
        if (Istime ( )) {

            GetPlayerInput ( );

        }
        else if (!Istime ( )) {
            bReset = false;
            inputResults.Clear ( );
            inputResults.AddRange (SendResult (inputs));
        }
    }

    void GetPlayerInput ( ) {
        if (!bReset) {
            inputs.Clear ( );
            bReset = true;
        }
        if (Input.GetButtonDown (playerInputString + "RED")) {
            inputs.Add (new PlayerInputInfo (EColor.RED, GetTime ( )));
        }
        else if (Input.GetButtonDown (playerInputString + "GREEN")) {
            inputs.Add (new PlayerInputInfo (EColor.GREEN, GetTime ( )));
        }
        else if (Input.GetButtonDown (playerInputString + "BLUE")) {
            inputs.Add (new PlayerInputInfo (EColor.BLUE, GetTime ( )));
        }
    }

    bool Istime ( ) {
        return true;
    }

    float GetTime ( ) {
        return .0f;
    }

    List<PlayerInputInfo> SendResult (List<PlayerInputInfo> inputs) {
        return inputs;
    }
}
