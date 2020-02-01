using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    [SerializeField] string type;
    public float workTime;
    private float timer;
    public Transform placePosition;
    public bool work(RobotPart part)
    {
        if (part.getCurrentRepair() == type)
        {
            Debug.Log(timer);
            if (timer < workTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                part.currentRepairDone();
            }
            return true;
        }
        return false;
    }
    public void resetWork()
    {
        timer = 0;
    }
}
