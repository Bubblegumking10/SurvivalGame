using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        FindFirstObjectByType<GameManager>()?.StartGame();
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game called!");
        Application.Quit();
    }
}
