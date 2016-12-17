using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    public bool moveLeft, moveRight, moveUp, moveDown,
        dashPressed, meleePressed, rangedPressed;

    void Start()
    {
        moveDown = moveLeft = moveRight = moveUp = false;
        dashPressed = meleePressed = rangedPressed = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
        }
        else if (Input.GetKeyUp(KeyCode.A)) moveLeft = false;

        if (Input.GetKey(KeyCode.D))
        {
            moveRight = true;
        }
        else if (Input.GetKeyUp(KeyCode.D)) moveRight = false;

        if (Input.GetKey(KeyCode.W))
        {
            moveUp = true;
        }
        else if (Input.GetKeyUp(KeyCode.W)) moveUp = false;

        if (Input.GetKey(KeyCode.S))
        {
            moveDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.S)) moveDown = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space)) dashPressed = false;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            meleePressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) meleePressed = false;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            rangedPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1)) rangedPressed = false;
    }
}
