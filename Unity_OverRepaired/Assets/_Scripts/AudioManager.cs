using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public PlayerMovement playerMovement1;
    public PlayerMovement playerMovement2;
    public StudioEventEmitter robotMove1;
    public StudioEventEmitter robotMove2;

    [EventRef]
    public string robotPickup;
    [EventRef]
    public string robotThrow;
    [EventRef]
    public string hammer;
    [EventRef]
    public string weld;
    [EventRef]
    public string screw;
    [EventRef]
    public string paint;

    bool moveStart1 = false;
    bool moveStart2 = false;

    void Update()
    {
        if (playerMovement1.isMoving == true && moveStart1 == false)
        {
            moveStart1 = true;
            RobotMove1();
        }

        else if (playerMovement1.isMoving == false)
        {
            moveStart1 = false;
            robotMove1.SetParameter("Stop", 1.0f);
        }

        if (playerMovement2.isMoving == true && moveStart2 == false)
        {
            moveStart2 = true;
            RobotMove2();
        }

        else if (playerMovement2.isMoving == false)
        {
            moveStart2 = false;
            robotMove2.SetParameter("Stop", 1.0f);
        }
    }

    void RobotMove1()
    {
        robotMove1.SetParameter("Stop", 0.0f);
        robotMove1.Play();
    }

    void RobotMove2()
    {
        robotMove2.SetParameter("Stop", 0.0f);
        robotMove2.Play();
    }

    public void RobotPickup()
    {
        RuntimeManager.PlayOneShot(robotPickup, transform.position);
    }

    public void RobotThrow()
    {
        RuntimeManager.PlayOneShot(robotThrow, transform.position);
    }

    public void Workstation(string type)
    {
        if(type == "Weld")
        {
            RuntimeManager.PlayOneShot(weld, transform.position);
        }
        else if(type == "Hammer")
        {
            RuntimeManager.PlayOneShot(hammer, transform.position);
        }
        else if(type == "Paint")
        {
            RuntimeManager.PlayOneShot(paint, transform.position);
        }
        else if(type == "Screw")
        {
            RuntimeManager.PlayOneShot(screw, transform.position);
        }
    }
}
