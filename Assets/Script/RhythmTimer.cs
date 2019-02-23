using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTimer : MonoBehaviour
{
    public float Deviation=0.1f; //誤差範圍
    public float Bpm = 100f;  //每分多少拍
    float SecPerBeat;  //1拍幾秒
    float SecRound;  //每回合幾秒
    float dspTimeSong;  //歌曲位置
    float songPosition;
    int conditionType;
    AudioSource audioSource;
    float [] conditionTime=new float[8];
    public bool IsInputTime { get {return( (conditionType == (int)EConditionType.Input) ? true : false); } }

    enum EConditionType{
        Ready=1,
        Input,
        Animation
    }
    // Start is called before the first frame update
    void Start()
    {
        SecPerBeat = 60f / Bpm;
        audioSource = gameObject.GetComponent<AudioSource> ();
        audioSource.Play();  //開始音樂
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = audioSource.time;
        Condition(songPosition);
        Debug.Log(songPosition/0.6f);
    }
    
    private void Condition(float songPosition)
    {
        float fTime = songPosition % SecRound;
        if (fTime >= 0f && fTime < (2.0f * SecPerBeat))
            conditionType = (int)EConditionType.Ready;
        else if (fTime >= (2.0f * SecPerBeat)- Deviation && fTime < (4.0f * SecPerBeat)+ Deviation)
            conditionType = (int)EConditionType.Input;
        else if (fTime >= (4.0f * SecPerBeat) && fTime < (8.0f * SecPerBeat))
            conditionType = (int)EConditionType.Animation;
    }
    

    public List<PlayerInputInfo> GetInputResult(List<PlayerInputInfo> playerInput) //判斷是否在輸入成功區間
    {
        List<PlayerInputInfo> result=new List<PlayerInputInfo>();
        for (int i = 0; i < playerInput.Count; i++)
        {
            float inputTime = playerInput[i].time % SecRound;
            if (inputTime>= (((2.0f+0.5f*i) * SecPerBeat) - Deviation) && inputTime <= (((2.0f + 0.5f * i) * SecPerBeat) + Deviation))
            {
                result.Add(playerInput[i]);
            }
        }
        return result;
    }
}
