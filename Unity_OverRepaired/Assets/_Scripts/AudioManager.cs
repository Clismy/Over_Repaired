using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [EventRef]
    public string robotMove;
    FMOD.Studio.EventInstance RobotMove;
    FMOD.Studio.ParameterInstance Stop;

    [EventRef]
    public string robotPickup;

    [EventRef]
    public string robotThrow;

    public void RobotMoveStart()
    {
        RobotMove = FMODUnity.RuntimeManager.CreateInstance(robotMove);
        RobotMove.getParameter("Stop", out Stop);
        RobotMove.start();
    }

    public void RobotMoveStop()
    {
        Stop.setValue(1.0f);
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

        }
        else if(type == "Hammer")
        {

        }
        else if(type == "Paint")
        {

        }
        else if(type == "Screw")
        {

        }
    }
}
