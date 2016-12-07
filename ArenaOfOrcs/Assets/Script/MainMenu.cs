using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public string startLevel;
    [SerializeField]
    private GameObject OptionsUI;
    // Use this for initialization
    public void NewGame(){
		Application.LoadLevel (startLevel); 
	}
	public void Options(){
        OptionsUI.SetActive(true);
	}
	public void ExitGame(){
		Application.Quit ();
	}
    public void CloseOptions()
    {
        OptionsUI.SetActive(false);
    }


}
