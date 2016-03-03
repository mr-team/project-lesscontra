using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void StartGame() {
        Debug.Log("Starting game");
        Application.LoadLevel(1);
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
