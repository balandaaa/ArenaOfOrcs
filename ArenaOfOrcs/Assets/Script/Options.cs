using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options : MonoBehaviour
{
    [SerializeField]
    private Toggle MuteUI;
    [SerializeField]
    private Slider VolumeSlider;
   

    // Use this for initialization
    void Start()
    {
        AudioListener.volume = VolumeSlider.value / 2;
    }
    void Update()
    {

    }

    public void Volume()
    {
        MuteUI.isOn = false;
        AudioListener.volume = VolumeSlider.value;
        if (VolumeSlider.value == 0)
            MuteUI.isOn = true;

    }
  
    public void Mute()
    {

        if (MuteUI.isOn)
            AudioListener.volume = 0;
        else
            AudioListener.volume = VolumeSlider.value;
    }
}
