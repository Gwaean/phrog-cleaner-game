using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeSlider : MonoBehaviour
{
    public string bank;
    public Slider slider;
    private Bus bus;

    void Start()
    {
        bus = RuntimeManager.GetBus(bank);
    }

    void OnEnable()
    {
        ChangeVolume(PlayerPrefs.GetFloat(bank, 1f));
        UpdateSlider();
    }

    public void ChangeVolume(float volume)
    {
        bus.setVolume(volume);
        PlayerPrefs.SetFloat(bank, volume);
    }
    
    void UpdateSlider()
    {
        slider.value = PlayerPrefs.GetFloat(bank, 1f);
    }
}
