using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryOptions : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}
