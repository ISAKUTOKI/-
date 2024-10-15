﻿using System.Collections;
using UnityEngine;

public class PlayerAnimationEventReceiver : MonoBehaviour
{

    public void OnCheckCombo()
    {
          PlayerBehaviour.instance.attack.OnCheckCombo();
    }
    public void OnAttacked()
    {
        //Debug.Log("PlayerAnimationEventReceiver OnAttacked");
        PlayerBehaviour.instance.attack.OnAttacked(false);
    }
     public void OnAttacked_2()
    {
        //Debug.Log("PlayerAnimationEventReceiver OnAttacked");
        PlayerBehaviour.instance.attack.OnAttacked(true);
    }
    public void StartIdleAnime()
    {
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
    }

    public void StartRunAnime()
    {
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.run);
    }
}