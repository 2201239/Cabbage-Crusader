using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f; //in animation is the set time for the animation to play
                                      //currently set to 1 second
    // Update is called once per frame
    void Update()
    { 
        //when you left click the mouse it starts the transition to the next scene
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }
    public void LoadNextLevel()
    {
        //instead of having to input each scene into the Scene Manager, you can use the build index to increment through the scenes
        //down side is you can not go back to a prior scene without restarting
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); 
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start"); //Play Animation

        yield return new WaitForSeconds(transitionTime); //Wait

        SceneManager.LoadScene(levelIndex); //Load the next scene

    }

}
