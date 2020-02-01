using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    public bool isBroken = false;

    [SerializeField]
    public string[] repairOrder = new string[4];
    public Stack<string> repairOrderStack = new Stack<string>();
    public bool randomAmount = true;

    [Serializable]
    public enum parts
    {
        Head,
        Chest,
        RightLeg,
        RightArm,
        LeftArm,
        LeftLeg
    }
    public parts whatPart;

    public void CreatePart()
    {
        for (int i = repairOrder.Length - 1; i >= 0; i--)
        {
            repairOrderStack.Push(repairOrder[i]);
        }
        if (randomAmount)
        {
            int rand = UnityEngine.Random.Range(0, 4);
            for (int u = 0; u < rand; u++)
            {
                repairOrderStack.Pop();
            }
        }
    }

    public string getCurrentRepair()
    {
        if (repairOrderStack.Count > 0)
        {
            return repairOrderStack.Peek();
        }
        else
        {
            return "";
        }
    }

    public void currentRepairDone()
    {
        if (repairOrderStack.Count > 0)
        {
            Debug.Log("FINISHED");

            if (repairOrderStack.Count == 1)
            {
                isBroken = false;
            }
            repairOrderStack.Pop();
        }
    }
}
