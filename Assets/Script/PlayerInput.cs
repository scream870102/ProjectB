using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerInput : MonoBehaviour {
    //temp
    protected IPlayer parent = null;
    public IPlayer Parent { set { if (parent == null) parent = value; } }
    public string PlayerInputString { set { playerInputString = value; } }
    private string playerInputString;
    [SerializeField]
    private List<PlayerInputInfo> inputs = new List<PlayerInputInfo> ( );
    private PlayerInputInfo[] inputResults = new PlayerInputInfo[2];
    public PlayerInputInfo[] InputResults { get { return inputResults; } }
    private bool bReset;

    // Start is called before the first frame update
    void Start ( ) {
        bReset = false;
    }

    // Update is called once per frame
    void Update ( ) {
        //Debug.Log(parent.rhythmTimer.IsInputTime);
        if (parent.rhythmTimer.IsInputTime) {

            GetPlayerInput ( );

        }
        else if (!parent.rhythmTimer.IsInputTime) {
            bReset = false;
            inputResults=SendResult (inputs);
        }
    }

    void GetPlayerInput ( ) {
        if (!bReset) {
            inputs.Clear ( );
            bReset = true;
        }
        if (Input.GetButtonDown (playerInputString + "RED")) {
            inputs.Add (new PlayerInputInfo (EColor.RED, parent.rhythmTimer.GetSongPosition));
        }
        else if (Input.GetButtonDown (playerInputString + "YELLOW")) {
            inputs.Add (new PlayerInputInfo (EColor.GREEN, parent.rhythmTimer.GetSongPosition));
        }
        else if (Input.GetButtonDown (playerInputString + "BLUE")) {
            inputs.Add (new PlayerInputInfo (EColor.BLUE, parent.rhythmTimer.GetSongPosition));
        }
    }

    PlayerInputInfo[] SendResult (List<PlayerInputInfo> inputs) {
        return parent.rhythmTimer.GetInputResult(inputs);;
    }
}
