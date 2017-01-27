using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior : Bolt.EntityBehaviour<IPlayerState> {

    PlayerController playControl;
    InputSupervisor inputMan;
    public GameObject playerCamera;

    public override void Attached()
    {
        playControl = gameObject.GetComponent<PlayerController>();
        inputMan = gameObject.GetComponent<InputSupervisor>();
        if(playControl != null)
            playControl.Networked = true;
        state.SetTransforms(state.PlayerTransform, transform);
        if (entity.isOwner)
        {
            state.PlayerColor = new Color(Random.value, Random.value, Random.value);
            playControl.FollowCamera = GameObject.FindGameObjectWithTag("MainCamera");
            playControl.FollowCamera.GetComponent<CameraMovement>().target = this.gameObject.transform;
        }
            
    }

    public override void SimulateOwner()
    {
        playControl.Movement();
        if (playControl.currentEnergy < playControl.energyMax && !playControl.energyRegain)
            StartCoroutine(playControl.EnergyRepletion());
        if (playControl.dashing)
            playControl.Dash();
    }
}
