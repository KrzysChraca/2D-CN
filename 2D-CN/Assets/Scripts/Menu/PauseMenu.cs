using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    private static PauseMenu _pm = null;
    public static PauseMenu GetInstance
    {
        get { return _pm; }
    }

    void Awake()
    {
        _pm = this;
    }

    public Transform canvas;
    public Transform player;

    public void TogglePauseMenu()
    {
        if(canvas.gameObject.activeInHierarchy == false)
        {
            Debug.Log("Pause Menu toggled");
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else if(canvas.gameObject.activeInHierarchy == true)
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
