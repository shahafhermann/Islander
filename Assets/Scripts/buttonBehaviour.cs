using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonBehaviour : MonoBehaviour {
    
    public GameObject mainMenu;
    public GameObject instructionsMenu;
    
    public void playGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void instructions() {
        mainMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void backToMenu() {
        mainMenu.SetActive(true);
        instructionsMenu.SetActive(false);
    }
}
