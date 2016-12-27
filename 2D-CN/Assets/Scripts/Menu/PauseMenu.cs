using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Transform canvas;
    public Transform player;
	
	// Update is called once per frame
	void Update () {
        ScanForKeyStroke();
    }

    void ScanForKeyStroke()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  
            TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        if(canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        if(canvas.gameObject.activeInHierarchy == true)
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
