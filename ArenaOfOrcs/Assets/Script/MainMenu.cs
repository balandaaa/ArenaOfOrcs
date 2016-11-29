using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public string startLevel;
	// Use this for initialization
	public void NewGame(){
		Application.LoadLevel (startLevel); 
	}
	public void Options(){
		Debug.Log ("Options");
	}
	public void ExitGame(){
		Application.Quit ();
	}
}
