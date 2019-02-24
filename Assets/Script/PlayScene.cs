using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    bool gamefinish;
    public GameObject[] winnerImage=new GameObject[4];
    public GameObject result;
    CatchThingManeger catchThingManeger;
    // Start is called before the first frame update
    void Start()
    {
        gamefinish=false;
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
        result.SetActive(true);
        winnerImage[catchThingManeger.rightThing].SetActive(true);
        //判斷哪邊贏設定位置480-480
    }
}
