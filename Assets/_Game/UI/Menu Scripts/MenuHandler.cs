    using UnityEngine;
    using UnityEngine.SceneManagement;
public class MenuHandler : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
 }
