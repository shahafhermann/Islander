using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonBehaviour : MonoBehaviour {
    
    public GameObject mainMenu;
    public GameObject instructionsMenu;
    public Animator background;
    public float animationTime = 3f;
    public Animator transition;
    public float transitionTime = 1f;
    
    public void playGame() {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    IEnumerator loadLevel(int levelIndex) {
        background.SetTrigger("PlayTrigger");
        yield return new WaitForSeconds(animationTime);
        
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(levelIndex);
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
