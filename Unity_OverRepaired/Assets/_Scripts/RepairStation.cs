using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    [SerializeField] string type;
    public float workTime;
    public float timer;
    public Transform placePosition;
    public WorkProgress ProgressBar;
    public Vector3 lookDirection;
    [SerializeField] AudioManager audioManager;
    bool playSoundOnce = false;
    public ParticleSystem system;

    public bool work(RobotPart part)
    {
        ProgressBar.Working(this);
        system.Play();
        if (part.getCurrentRepair() == type)
        {
            Debug.Log(timer);
            if(!playSoundOnce)
            {
                audioManager?.Workstation(type);
                playSoundOnce = true;
            }
            if (timer < workTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                part.currentRepairDone();
                system.Pause();
                playSoundOnce = false;
            }
            return true;
        }
        playSoundOnce = false;
        return false;
    }
    public void resetWork()
    {
        timer = 0;
    }
}
