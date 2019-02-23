using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTimer : MonoBehaviour
{
    float ftimer;
    public float fDeviation=0.1f; //誤差範圍
    public float fbpm = 60f;  //每分多少拍
    float fsecperbeat;  //1拍幾秒
    float fsecround;  //每回合幾秒
    int iconditiontype;

    public bool IsInputTime { get {return( (iconditiontype == (int)ConditionType.Input) ? true : false); } }
    enum ConditionType
    {
        Ready=100, //準備區間
        Input,  //輸入區間
        Animation,  //動畫區間
    }
    // Start is called before the first frame update
    void Start()
    {
        ftimer = 0.0f;
        fsecperbeat = 60f / fbpm;
        fsecround = 8.0f * fsecperbeat; 
    }

    // Update is called once per frame
    void Update()
    {
        //ftimer += Time.deltaTime;
        //Debug.Log((int)ftimer);
        //Condition();
        //Debug.Log(iconditiontype);
    }
    
    private void Condition(float ftime)
    {
        ftime = ftime % fsecround;
        if (ftime >= 0f && ftime < (2.0f * fsecperbeat))
            iconditiontype = (int)ConditionType.Ready;
        else if (ftime >= (2.0f * fsecperbeat)- fdeviation && ftime < (4.0f * fsecperbeat)+ fdeviation)
            iconditiontype = (int)ConditionType.Input;
        else if (ftime >= (4.0f * fsecperbeat) && ftime < (8.0f * fsecperbeat))
            iconditiontype = (int)ConditionType.Animation;
    }
    

    public void GetInputResult(List<PlayerInputInfo> playerInputInfo) //判斷是否在輸入成功區間，回傳哪些成功1234
    {
        for (int i = 0; i < playerInputInfo.Count; i++)
        {
            //float ff = ftime[i] % fsecround;
            //if (ff>= (((2.0f+0.5f*i) * fsecperbeat) - fdeviation) && ff <= (((2.0f + 0.5f * i) * fsecperbeat) + fdeviation))
            //{
            //    inputfinish[i] = true;
            //}
        }
    }
}
