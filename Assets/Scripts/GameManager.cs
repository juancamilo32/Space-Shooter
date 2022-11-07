using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;
    [SerializeField]
    GameObject pauseMenu;

    Animator pauseMenuAnimator;

    private void Start()
    {
        pauseMenuAnimator = pauseMenu.GetComponent<Animator>();
        if (!pauseMenuAnimator)
        {
            Debug.LogError("Pause Menu Animator is null");
        }
        pauseMenuAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    private void Update()
    {
        ReloadGame();
        PauseGame();
    }

    void ReloadGame()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isGameOver)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            pauseMenuAnimator.SetBool("isPaused", true);
        }
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseMenuAnimator.SetBool("isPaused", false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

 }
