using UnityEngine;
using System.Collections;

public class MenuTab : MonoBehaviour {

    public Transform MenuTabSelectable;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
	}

   

    public void ToggleMenuTab()
    {
        if (MenuTabSelectable.gameObject.activeInHierarchy == false)
        {
            MenuTabSelectable.gameObject.SetActive(true);
        }
        else
        {
            MenuTabSelectable.gameObject.SetActive(false);
        }
    }
    
}
