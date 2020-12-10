using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Game has been Quit!");
        Application.Quit();
    }
}
