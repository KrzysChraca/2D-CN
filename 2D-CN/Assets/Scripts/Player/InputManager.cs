using UnityEngine;
using System.Collections;
using Rewired;

public class InputManager : MonoBehaviour {
    public int playerID;
    public bool  dashPressed, meleePressed, rangedPressed;

    public Vector3 moveVector;
    Player rePlayer;

    void Start()
    {
        playerID = 0;
        rePlayer = ReInput.players.GetPlayer(playerID);
        dashPressed = meleePressed = rangedPressed = false;
    }

    void Update()
    {
        moveVector.x = rePlayer.GetAxisRaw("Horizontal");
        moveVector.y = rePlayer.GetAxisRaw("Vertical");

        if (rePlayer.GetButtonDown(2)) //Dash Button pressed
            dashPressed = true;
        else dashPressed = false;

        if (rePlayer.GetButtonDown(3)) //Melee Button pressed
            meleePressed = true;
        else meleePressed = false;

        if (rePlayer.GetButton(4)) //Ranged Button pressed
            rangedPressed = true;
        else rangedPressed = false;
    }
}
