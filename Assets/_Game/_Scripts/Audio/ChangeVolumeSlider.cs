using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeSlider : MonoBehaviour
{
    public string bank;
    public Slider slider;
    private Bus bus;

    void Awake()
    {
        bus = RuntimeManager.GetBus(bank);
    }

    void OnEnable()
    {
        UpdateSlider();
    }

    public void ChangeVolume(float volume)
    {
        bus.setVolume(volume);
        PlayerPrefs.SetFloat(bank, volume);
    }

    public void UpdateSlider()
    {
        if (!bus.isValid())
            bus = RuntimeManager.GetBus(bank);

        slider.value = PlayerPrefs.GetFloat(bank, 1f);
        ChangeVolume(PlayerPrefs.GetFloat(bank, 1f));
    }
}
