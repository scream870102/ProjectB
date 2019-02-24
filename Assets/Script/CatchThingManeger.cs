using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchThingManeger : MonoBehaviour
{
    public int rightThing{ get { return i; }} 
    int i;
    public GameObject[] thingImage=new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        i = Random.Range( 0, 5 );
        thingImage[rightThing].SetActive(true);//產生UI
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
