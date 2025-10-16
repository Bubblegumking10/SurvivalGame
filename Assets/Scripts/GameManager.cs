using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    public GameObject mainMenu;

    private bool isPaused = false;
    private bool gameStarted = false;

    void Start()
    {
        // Show main menu, hide others
        mainMenu.SetActive(true);
        gameUI.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);

        // Freeze the game while in main menu
        Time.timeScale = 0f;
        UnlockCursor();
    }

    void Update()
    {
        if (gameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);

        Time.timeScale = 1f;
        gameStarted = true;
        LockCursor();
    }

    public void TogglePause()
    {
        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            UnlockCursor();
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            LockCursor();
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0f;
        UnlockCursor();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        LockCursor();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game called!");
        Application.Quit();
    }

    // ---- Cursor Controls ----
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
