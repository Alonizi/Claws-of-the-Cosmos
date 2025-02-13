using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void onStart(){
        SceneManager.LoadScene("Game");
    }

    public void onPlay(){
        SceneManager.LoadScene("Game");
    }

    public void onReturn(){
        SceneManager.LoadScene("Start");
    }
    public void onTutorial(){
        SceneManager.LoadScene("Tutorial");
    }
    public void onCridite(){
        SceneManager.LoadScene("Cridite");
    }
     public void onEnd(){
        SceneManager.LoadScene("End");
    }
    public void onExit(){
        //UnityEditor.EditorApplication.isPlaying = false; // only use in editor , when bild delete it ##
       Application.Quit();
    }
}
