using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MainMenuMusic : MonoBehaviour
{
    public EventReference musicToPlay;
    private EventInstance eventInstance;


    void OnEnable()
    {
        eventInstance = RuntimeManager.CreateInstance(musicToPlay);
        eventInstance.start();
    }

    void OnDisable()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        eventInstance.release();
    }
}
