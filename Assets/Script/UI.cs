using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public int whichPlayer;
    public GameObject[] colorImageright=new GameObject[6];
    public GameObject[] colorImageleft=new GameObject[6];
    EColor aa;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aa=EColor.RED;
        if(whichPlayer==0){
            int k=0;
            for(int i=0;i<2;i++) {
                //aa=GameManager.instance.player1.InputResults[i].color;
                if(aa!=EColor.NONE){
                    if(k==0){
                        colorImageright[(int)aa].SetActive(true);
                        k++;}
                    else if(k==1){
                        colorImageleft[(int)aa].SetActive(true);
                        k++;
                    }
                }
            }
        }
        
    }
}
