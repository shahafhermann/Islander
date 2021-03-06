﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void replay() {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }
    
    IEnumerator loadLevel(int levelIndex) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(levelIndex);
    }
}
