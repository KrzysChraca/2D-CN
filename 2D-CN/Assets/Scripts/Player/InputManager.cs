using UnityEngine;
using System.Collections;
using Rewired;

public class InputManager : MonoBehaviour {
    public int playerID;
    public bool dashPressed, meleePressed, rangedPressed, controllerActive;

    public Vector3 moveVector, controllerAttackDirection;
    Player rePlayer;
    public Controller mainController;

    void Start()
    {
        playerID = 0;
        rePlayer = ReInput.players.GetPlayer(playerID);
        dashPressed = meleePressed = rangedPressed = false;
        controllerAttackDirection = new Vector3();
    }

    void Update()
    {
        mainController = rePlayer.controllers.GetLastActiveController();
        
        moveVector.x = rePlayer.GetAxisRaw("Horizontal");
        moveVector.y = rePlayer.GetAxisRaw("Vertical");
        if(mainController != null)
        {
            if (mainController.type == ControllerType.Joystick)
            {
                controllerActive = true;
                controllerAttackDirection = moveVector;
            }
            else controllerActive = false;
        }

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

    public Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
