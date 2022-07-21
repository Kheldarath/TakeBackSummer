using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]float loadTime = 1f;    

    private void OnTriggerEnter2D(Collider2D coll)
    {
        StartCoroutine(ExitLevel());       

    }

    

    IEnumerator ExitLevel()
    {
        yield return new WaitForSecondsRealtime(loadTime);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        //Change the below once you have a full game loop with game over screen
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
