using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameObject player;
    private ActiveGunSelector gunSelector;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject welcomePanel;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject gunSelectPanel;
    private int playerHp;
    public static bool GameIsPaused = false;
    public bool GameIsWon;
    public bool GameIsLost;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] AudioSource audioWelcome;
    [SerializeField] AudioSource audioWin;
    [SerializeField] AudioSource audioDeath;
    [SerializeField] AudioSource audioLevel;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
       

        gameoverPanel.SetActive(false);
        victoryPanel.SetActive(false);
        GameIsLost = false;
        GameIsWon = false;
        Welcome();
        player = GameObject.FindGameObjectWithTag("player");
        gunSelector = GetComponent<ActiveGunSelector>();


    }

    // Update is called once per frame
    public void Welcome()
    {
        audioWelcome.Play();
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        welcomePanel.SetActive(true);
        PauseMenuUI.SetActive(false);
        Cursor.visible = true;
        Time.timeScale = 0;
       
    }
    public void StopPlaying()
    {
        audioWelcome.Stop();
        audioLevel.Stop();
    }
   
    public void NextLevel()
    {
        audioLevel.Play();
        gunSelectPanel.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3(400, 2, 400);
        player.transform.rotation = new Quaternion(0, 0, 0,0);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RailGun()
    {

        gunSelector.SelectRailGun();
        gunSelectPanel.SetActive(false);
        PauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void LaserRifle()
    {
        gunSelector.SelectLaserRifle();
        gunSelectPanel.SetActive(false);
        PauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Akimbo()
    {
        gunSelector.SelectAkimbo();
        gunSelectPanel.SetActive(false);
        PauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Handgun()
    {
        gunSelector.SelectHandgun();
        gunSelectPanel.SetActive(false);
        PauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!GameIsLost && !GameIsWon)
        {
            if (Input.GetButtonDown("Cancel"))
            {

                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void GameOver()
    {
        audioDeath.Play();
        Cursor.lockState = CursorLockMode.Confined;
        gameoverPanel.SetActive(true);
        Time.timeScale = 0;
        GameIsLost = true;
    }

    public void Victory()
    {
        audioWin.Play();
        Cursor.lockState = CursorLockMode.Confined;
        victoryPanel.SetActive(true);
        Time.timeScale = 0;
      
    }

    public void Retry()
    {
        audioDeath.Stop();
        audioWin.Stop();
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1;
        gameoverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
    private void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings() { }
    public void MainMenu() { SceneManager.LoadScene(0); }
}
