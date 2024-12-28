﻿using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;

    public PlayerHealthBehaviour health { get; private set; }
    public PlayerMove move { get; private set; }
    public PlayerAttackBehaviour attack { get; private set; }

    public PlayerJump jump { get; private set; }

    public PlayerDeflectBehaviour defend { get; private set; }

    public PlayerDie die { get; private set; }

    public Animator animator { get; private set; }

    public PlayerMovePosition movePosition { get; private set; }

    public PlayerWeaponView weaponView { get; private set; }

    public Transform flip;

    private void Awake()
    {
        instance = this;

        animator = GetComponentInChildren<Animator>();
        movePosition = GetComponent<PlayerMovePosition>();
        attack = GetComponent<PlayerAttackBehaviour>();
        jump = GetComponent<PlayerJump>();
        move = GetComponent<PlayerMove>();
        health = GetComponent<PlayerHealthBehaviour>();
        weaponView = GetComponent<PlayerWeaponView>();
        defend = GetComponent<PlayerDeflectBehaviour>();
    }

    public void Init()
    {
        weaponView.SetState(PlayerWeaponView.State.idle);
    }

    public void OnHit()
    {
        //Debug.Log("OnHit");
        if (defend.isDefending)
        {
            if (defend.isDeflecting)
            {
                defend.TriggerDeflect();
                return;
            }

            defend.TriggerDefend();
            return;
        }

        health.TakeDamage(1);
    }
}