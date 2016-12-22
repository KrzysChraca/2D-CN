using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager _GM = null;
    public static GameManager GetInstance
    {
        get { return _GM; }
    }

    public Text energyText;

    private int energyAmount;
    public int enAmount
    {
        get { return energyAmount; }
        set { energyAmount = value; }
    }

    void Awake()
    {
        _GM = this;
    }

    void Update()
    {
        energyText.text = "Energy " + energyAmount;
    }
    
}
