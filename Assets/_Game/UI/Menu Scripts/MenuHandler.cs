using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    public List<ChangeVolumeSlider> volumeSlider = new(); // MainMenu sets the volume of all sliders to the saved values in PlayerPrefs

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }

    public void Start()
    {
        volumeSlider.ForEach(volume => volume.UpdateSlider());
    }
}
