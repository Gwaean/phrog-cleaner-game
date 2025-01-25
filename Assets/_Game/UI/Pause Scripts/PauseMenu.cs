using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] public static bool GameIsPaused = false;
    [SerializeField] public GameObject pauseMenuUI;
    
    [SerializeField] public PauseInput inputActions;
    private InputAction pause;

    private void Awake(){
        inputActions = new PauseInput();
    }


    private void OnEnable(){
        pause = inputActions.Pause.Pause;
        pause.Enable();
    }

    private void OnDisable(){
        pause.Disable();
    }
    
    // Update is called once per frame
    void Update()
    {
        if( PressedPause() ){
            if(GameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    bool PressedPause(){
        return pause.triggered;
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
