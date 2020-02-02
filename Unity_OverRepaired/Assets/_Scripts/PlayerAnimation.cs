﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement pM;
    PlayerPickUp pP;
    Rigidbody rb;



    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponentInParent<PlayerMovement>();
        pP = GetComponentInParent<PlayerPickUp>();
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float isMoving = 0;

        if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z) > 0)
        {
            isMoving = 1;
        }

        anim.SetFloat("IsMoving", isMoving);

        anim.SetBool("Throw", pP.GetIfThrow());
        anim.SetBool("Hold", pP.GetIfHolding());
        if(pP.animInteracting)
        {
            anim.SetBool("Interacting", pP.animInteracting);
            pP.animInteracting = false;
        }
        anim.SetBool("Interacting", pP.GetIfInteracting());
    }
}