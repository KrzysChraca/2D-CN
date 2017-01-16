using UnityEngine;
using System.Collections;
using Rewired;

public class InputSupervisor : MonoBehaviour {
    public int playerID;
    public bool dashPressed, meleePressed, rangedPressed, controllerActive;

    public Vector3 moveVector, controllerAttackDirection, lastInputDirection, lastControllerDirection;
    Player rePlayer;
    public Controller mainController;

    void Start()
    {
        playerID = 0;
        rePlayer = ReInput.players.GetPlayer(playerID);
        dashPressed = meleePressed = rangedPressed = false;
        controllerAttackDirection = new Vector3();
        moveVector = new Vector3(1, 0, 0);
        lastInputDirection = moveVector;
    }

    void Update()
    {
        mainController = rePlayer.controllers.GetLastActiveController();

        moveVector.x = rePlayer.GetAxisRaw("Horizontal");
        moveVector.y = rePlayer.GetAxisRaw("Vertical");


        if (mainController != null)
        {
            if (mainController.type == ControllerType.Joystick)
            {
                controllerActive = true;
                if (rePlayer.GetAxisRaw("Target Horizontal") > 0 || rePlayer.GetAxisRaw("Target Vertical") > 0)
                    controllerAttackDirection = new Vector3(rePlayer.GetAxisRaw("Target Horizontal"), rePlayer.GetAxisRaw("Target Vertical"), 0);
                else if (controllerAttackDirection == Vector3.zero)
                {
                    if (moveVector == Vector3.zero)
                        controllerAttackDirection = moveVector;
                    else lastInputDirection = moveVector;
                }
                else
                {
                    lastInputDirection = controllerAttackDirection;
                    controllerAttackDirection = Vector3.zero;
                }
            }
            else controllerActive = false;
        }

        if (rePlayer.GetButtonDown("Dash0")) //Dash Button pressed
            dashPressed = true;
        else dashPressed = false;

        if (rePlayer.GetButtonDown("Melee")) //Melee Button pressed
            meleePressed = true;
        else meleePressed = false;

        if (rePlayer.GetButton("Ranged")) //Ranged Button pressed
            rangedPressed = true;
        else rangedPressed = false;

        if (rePlayer.GetButtonDown("Pause Menu"))
            PauseMenu.GetInstance.TogglePauseMenu();
    }

    public Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
