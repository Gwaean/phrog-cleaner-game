using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayMusic : MonoBehaviour
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

    public void ChangeParameter(string parameter, float progress, bool isProgress)
    {
        float adjustedProgress = progress;
        if (isProgress)
        {
            adjustedProgress = progress / 10;
        }

        eventInstance.setParameterByName(parameter, adjustedProgress);
    }
}
