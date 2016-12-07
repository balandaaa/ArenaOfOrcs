using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public string startLevel;
    [SerializeField]
    private Toggle MuteUI;
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
    public void Mute()
    {

        if (MuteUI.isOn)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1f;
    }
    public void CloseOptions()
    {
        OptionsUI.SetActive(false);
    }
}
