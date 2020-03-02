using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject victoryPanel;
    private int playerHp;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        gameoverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Victory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void retry()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        gameoverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
