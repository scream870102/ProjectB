using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSheet : MonoBehaviour
{
    RhythmTimer rhythmTimer;
    public float speed=180f;
    public float turnto,startturn;
    private float songtime;
    bool isSongPlaying;
    public Transform musicSheet;
    // Start is called before the first frame update
    void Start()
    {
        musicSheet=gameObject.GetComponent<Transform>();
        rhythmTimer=GameObject.Find("AudioSource").GetComponent<RhythmTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        isSongPlaying=rhythmTimer.IsSongPlaying;
        if(rhythmTimer.IsSongPlaying){
            musicSheet.position+=new Vector3(-speed*Time.deltaTime,0f,0f);
            if(musicSheet.position.x<=startturn)
                musicSheet.position=new Vector3(turnto,musicSheet.position.y,musicSheet.position.z);
        }
    }
}
