using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    [SerializeField] string type;
    public float workTime;
    private float timer;
    public Transform placePosition;
    public void work(RobotPart part)
    {
        if (part.getCurrentRepair() == type)
        {
            if (timer < workTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                part.currentRepairDone();
            }
        }
    }
    public void resetWork()
    {
        timer = 0;
    }
}
