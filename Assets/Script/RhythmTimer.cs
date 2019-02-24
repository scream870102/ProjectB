using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTimer : MonoBehaviour
{
    public float Deviation = 0.1f; //誤差範圍
    public float Bpm = 100.0f;  //每分多少拍
    float SecPerBeat;  //1拍幾秒
    float SecRound;  //每回合幾秒
    float dspTimeSong;  //歌曲位置
    AudioSource audioSource;
    float songPosition;
    int conditionType;
    float[] InputTime = new float[4];
    public bool IsInputTime { get { return ((conditionType == (int)EConditionType.Input) ? true : false); } }
    public float GetSongPosition{ get { return songPosition; } }
    public bool IsSongPlaying{ get { return audioSource.isPlaying; } }
    enum EConditionType
    {
        Ready = 1,
        Input,
        Animation
    }
    // Start is called before the first frame update
    void Start()
    {
        SecPerBeat = 60.0f / Bpm;
        SecRound=8.0f*SecPerBeat;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();  //開始音樂
        for(int i=0;i<4;i++){
            InputTime[i]=((2.0f + 0.5f * i) * SecPerBeat);  //輸入的時間
        }
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = audioSource.time;
        Condition(songPosition);   //判斷狀態
        Debug.Log(conditionType);
    }

    private void Condition(float songPosition)
    {
        float fTime = songPosition % SecRound;
        if (fTime >= 0f && fTime < 2.0f*SecPerBeat- Deviation)
            conditionType = (int)EConditionType.Ready;
        else if (fTime >= 2.0f*SecPerBeat- Deviation && fTime < (4.0f * SecPerBeat+ Deviation) )
            conditionType = (int)EConditionType.Input;
        else if (fTime >= (4.0f * SecPerBeat- Deviation)&& fTime < (8.0f * SecPerBeat))
            conditionType = (int)EConditionType.Animation;
    }


    public PlayerInputInfo[] GetInputResult(List<PlayerInputInfo> playerInput) //判斷是否在輸入成功區間
    {
        
        PlayerInputInfo [] result=new PlayerInputInfo[2]{new PlayerInputInfo(EColor.NONE,.0f),new PlayerInputInfo(EColor.NONE,.0f)}; 
        bool [] bsuccess=new bool[4];
        //List<PlayerInputInfo> result = new List<PlayerInputInfo>();
        for (int i = 0; i < playerInput.Count; i++)
        {
            float playerInputTime = playerInput[i].time % SecRound;
            for(int j = 0; j < 4; j++){
                Debug.Log("j="+j);
                Debug.Log("bsuccess[0]="+bsuccess[0]);
                if(!bsuccess[j]&&playerInputTime >= (InputTime[j] - Deviation) && playerInputTime <= (InputTime[j] + Deviation)){
                    
                    bsuccess[j]=true;
                    if(j==0){
                        result[0]=playerInput[i];
                        Debug.Log(result[0]);
                        Debug.Log(playerInput[i]);}
                    else if(j==1&&bsuccess[0]){
                        result[0].color=MixColor(playerInput[i].color,playerInput[i-1].color);
                        Debug.Log("a");}
                    else if(j==2)
                        result[1]=playerInput[i];
                    else if(j==3&&bsuccess[2]){
                        result[1].color=MixColor(playerInput[i].color,playerInput[i-1].color);
                        Debug.Log("b");}
                }
            }

        }
        return result;
        
    }
    EColor colorResult;
    EColor MixColor(EColor color1,EColor color2){
        switch(color1){ //判斷顏色組合
            case EColor.RED:
                if(color2==EColor.BLUE)
                    colorResult=EColor.PURPLE;
                else if (color2==EColor.YELLOW)
                    colorResult=EColor.ORANGE;
                else 
                    colorResult=color1;
                break;
            case EColor.YELLOW:
                if(color2==EColor.BLUE)
                    colorResult=EColor.GREEN;
                else if (color2==EColor.RED)
                    colorResult=EColor.ORANGE;
                else 
                    colorResult=color1;
                break;
            case EColor.BLUE:
                if(color2==EColor.YELLOW)
                    colorResult=EColor.GREEN;
                else if (color2==EColor.RED)
                    colorResult=EColor.PURPLE;
                else 
                    colorResult=color1;
                break;
            default:
                break;
        }
        return colorResult;
    } 
}
