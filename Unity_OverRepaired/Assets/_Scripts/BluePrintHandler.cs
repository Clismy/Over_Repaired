﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BluePrintHandler : MonoBehaviour
{
    public List<GameObject> bluePrintHolder;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bluePrintHolder.Count; i++)
        {
            if (bluePrintHolder[i].activeSelf)
            {
                bluePrintHolder[i].SetActive(false);
            }
        }
    }
    public void CheckBluePrint(RobotPart[] robotPart)
    {
        for (int i = 0; i < bluePrintHolder.Count; i++)
        {
            if (!bluePrintHolder[i].activeSelf)
            {
                bluePrintHolder[i].SetActive(true);
                for (int j = 0; j < robotPart.Length; j++)
                {
                    if (robotPart[j].isBroken)
                    {
                        List<GameObject> templimb = bluePrintHolder[i].GetComponent<LimbCheck>().limbs;
                        for (int k = 0; k < templimb.Count; k++)
                        {
                            if (templimb[k].name == robotPart[j].whatPart.ToString())
                            {
                                templimb[j].GetComponent<Image>().color = Color.red;
                                templimb[j].GetComponent<SetChildActive>().setTrue();
                            }
                        }
                    }
                }
            }
        }
            
    }

}
