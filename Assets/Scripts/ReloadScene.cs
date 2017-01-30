using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {

    public void Reload() {
   Application.LoadLevel(Application.loadedLevel);
          //   SceneManager.LoadScene(0); 
    }
}
