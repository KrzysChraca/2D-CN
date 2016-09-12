using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Transform canvas;

	// Use this for initialization
	void Start () {
	
	}
	
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
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
