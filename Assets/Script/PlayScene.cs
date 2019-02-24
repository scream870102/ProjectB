using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    bool gamefinish;
    public GameObject[] player1Image=new GameObject[6];
    public GameObject[] player2Image=new GameObject[6];
    //public GameObject result;
    public CatchThingManeger catchThingManeger;
    // Start is called before the first frame update
    void Start()
    {
        gamefinish=true;
        //catchThingManeger = GameObject.Find ("catchthing").GetComponent<CatchThingManeger> ( );
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.Winner!="NONE")
            gamefinish=true;
        if(gamefinish){
            ShowWiner();
            
        }
    }
    void ShowWiner(){
        if(true){
            player1Image[catchThingManeger.rightThing].SetActive(true);
            player2Image[5].SetActive(true);
        }
        else if(GameManager.instance.Winner=="Player2"){
            player1Image[catchThingManeger.rightThing].SetActive(true);
            player2Image[5].SetActive(true);
        }
    }
}
