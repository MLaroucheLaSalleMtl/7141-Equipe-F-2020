
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject PauseMenuUI;
  
    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {

            if (GameIsPaused)
            {
                Resume() ;
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Settings() { }
    public void MainMenu() { }

}
