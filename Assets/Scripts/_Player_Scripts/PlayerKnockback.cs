﻿using UnityEngine;
using System.Collections;

public class PlayerKnockback : MonoBehaviour {

    PlayerMovement_Ver2 PlayerP;
    Transform HazardT;
    Rigidbody PlayerRb;

    public bool collided, Inactive = false;
    public Vector3 FinalKnockBack;
	public float knockbackMultiplier, RecoverTime, KnockBackJumpForce;

    private float TimeLeft, currentKnockBackJumpForce;
    private Vector3 KnockBackOrientation, hitVector;

    // Use this for initialization
    void Start () {
        PlayerP = this.GetComponent<PlayerMovement_Ver2>();
        PlayerRb = this.GetComponent<Rigidbody>();
        collided = false;

        TimeLeft = RecoverTime;
    }

    // Update is called once per frame
    void Update() {
        //ThisIsATest();
        //Debug.DrawRay(PlayerRb.position, hitVector, Color.red);

        if (Inactive == false)
        {
            if (collided == true)//&& itStoppedMoving == false)
            {

                PlayerP.DontMove = true;
                ForceAdder();

                //JumpingC.setInitialSpeed(-15.0f,false);
                //StartCoroutine(KnockBackTime(0.4f));
            }
            else
            {
                PlayerP.DontMove = false;
                currentKnockBackJumpForce = KnockBackJumpForce;
            }
            //JumpingC.IsItGrounded();
        }

    }

    
     void OnTriggerEnter(Collider other){

           //check for collision with anything tagged "NPC_Collider"
           /*if (other.tag == "NPC_Collider") {
               collided = true;
               //Get NPC Rigidbody then reverse it's velocity
               npcRB = other.GetComponentInParent<Rigidbody>();
               npcRB.velocity = -npcRB.velocity;//*0.8f;
               KnockBackOrientation = npcRB.transform.rotation*Vector3.forward;
               //Debug.Log(KnockBackOrientation.magnitude);
           }*/
        if (other.tag == "EpicentralHazard") {
            collided = true;
            HazardT = other.GetComponent<Transform>();
            hitVector = new Vector3(HazardT.transform.position.x - PlayerRb.transform.position.x, 0.0f, HazardT.transform.position.z - PlayerRb.transform.position.z);
            KnockBackOrientation = hitVector*-1.0f;
            //KnockBackOrientation = HazardT.transform.rotation * Vector3.forward;
        }
        if (other.tag == "hazard")
        {
            collided = true;
            HazardT = other.GetComponent<Transform>();
            //hitVector = new Vector3(HazardT.transform.position.x - PlayerRb.transform.position.x, 0.0f, HazardT.transform.position.z - PlayerRb.transform.position.z);
            //KnockBackOrientation = hitVector * -1.0f;
            KnockBackOrientation = HazardT.transform.rotation * Vector3.forward;
        }

    }
    /*
      void OnTriggerExit(Collider other)
       {
           //check for collision with anything tagged "NPC_Collider"
           if (other.tag == "NPC_Collider")
           {
               collided = false;
           }
           if (other.tag == "hazard")
           {
               collided = false;
           }
       }
*/

    void ForceAdder() {
        TimeLeft -= Time.deltaTime;

        FinalKnockBack = KnockBackOrientation * TimeLeft* knockbackMultiplier;
        PlayerP.initialAirSpeed = currentKnockBackJumpForce;

        if (PlayerP.forKnockBack == true)
        {
            currentKnockBackJumpForce = 0.0f;
        }


        if (TimeLeft <= 0.0f) {
            TimeLeft = RecoverTime;
            collided = false;
        }
    }


}