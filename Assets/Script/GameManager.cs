using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class GameManager : MonoBehaviour {
	//static ref for this class
	[HideInInspector]
	public static GameManager instance = null;
	public EColor[,] maps=new EColor[50,50];
	void Awake ( ) {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}
	protected virtual void Start ( ) {	
	}
	protected virtual void Update ( ) { }

}
