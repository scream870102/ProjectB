using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene: MonoBehaviour
{
    public GameObject teachImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStartBtn(){
        teachImage.SetActive(true);
    }
    public void OnClickGoBtn(){
        SceneManager.LoadScene(1);
    }
    
}
