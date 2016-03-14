using UnityEngine;
using System.Collections;

public class WinLoseScreenButtons : MonoBehaviour {

    public void MainMenu() {
        Application.LoadLevel(0);
    }

    public void RestartGame() {
        Application.LoadLevel(1);
    }

}
