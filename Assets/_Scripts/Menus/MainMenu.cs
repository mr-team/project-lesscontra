using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame() {
        Debug.Log("Starting game");
        SceneManager.LoadScene("Prototype_allTogethers");
    }

    public void LoadGame() {
        Debug.Log("Loading game");
    }

    public void Options() {
        Debug.Log("Loading Options");
    }

    public void QuitGame() {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
