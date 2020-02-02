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
    public bool isRepaired = false;
    public GameObject bluePrint = null;

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
                isRepaired = true;
                isBroken = false;
            }
            repairOrderStack.Pop();
        }
    }

    public void UpdateHighlightBluePrint()
    {
        if (bluePrint != null)
        {

            for (int i = 0; i < repairOrder.Length; i++)
            {
                var colortest = bluePrint.GetComponent<Icons>().icons[3 - (repairOrderStack.Count - 1)].color;
                colortest = new Color(colortest.r, colortest.g, colortest.b, 0.3f);
                bluePrint.GetComponent<Icons>().icons[i].color = colortest;
            }
            var color = bluePrint.GetComponent<Icons>().icons[3 - (repairOrderStack.Count - 1)].color;
            color = new Color(color.r, color.g, color.b, 1);
            bluePrint.GetComponent<Icons>().icons[3 - (repairOrderStack.Count - 1)].color = color;
        }
    }

    public void CreateHighlightBluePrint()
    {
        if (bluePrint != null)
        {

            for (int i = 0; i < repairOrder.Length; i++)
            {
                var colortest = bluePrint.GetComponent<Icons>().icons[3 - (repairOrderStack.Count - 1)].color;
                colortest = new Color(colortest.r, colortest.g, colortest.b, 0.3f);
                bluePrint.GetComponent<Icons>().icons[i].color = colortest;
            }
            var color = bluePrint.GetComponent<Icons>().icons[0].color;
            color = new Color(color.r, color.g, color.b, 1);
            bluePrint.GetComponent<Icons>().icons[0].color = color;
        }
    }
}
