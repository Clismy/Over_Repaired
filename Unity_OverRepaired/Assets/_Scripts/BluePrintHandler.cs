using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BluePrintHandler : MonoBehaviour
{
    public List<GameObject> bluePrintHolder;
    public string name;
    public Sprite[] images = new Sprite[4];
    private Dictionary<string, Sprite> lookUpTable = new Dictionary<string, Sprite>();
    private int index  = 0;

    // Start is called before the first frame update
    void Start()
    {

        for (int k = 0; k < 4; k++)
        {
            lookUpTable.Add(images[k].name, images[k]);
        }


        for (int i = 0; i < bluePrintHolder.Count; i++)
        {
            if (bluePrintHolder[i].activeSelf)
            {
                bluePrintHolder[i].SetActive(false);
            }
        }
    }
    private int getBluePrintHolderIndex()
    {
        index++;
        return index-1 % 6;
    }

    public void CreateBluePrints(RobotPart[] robotPart)
    {
        for (int j = 0; j < robotPart.Length; j++)
        {
            if (robotPart[j].isBroken) {
                int i = getBluePrintHolderIndex();
                for (int u = 0; u < robotPart[j].repairOrder.Length; u++)
                {
                    bluePrintHolder[i].GetComponent<Icons>().icons[u].gameObject.SetActive(true);
                    if (u < robotPart[j].repairOrderStack.Count)
                    {
                        int offset = robotPart[j].repairOrder.Length - robotPart[j].repairOrderStack.Count;
                        bluePrintHolder[i].GetComponent<Icons>().icons[u].sprite = lookUpTable[robotPart[j].repairOrder[u + offset]];
                    }
                    else
                    {
                        bluePrintHolder[i].GetComponent<Icons>().icons[u].gameObject.SetActive(false);
                    }
                }

                bluePrintHolder[i].SetActive(true);
            }
        }
    }
    /*public void CheckBluePrint(RobotPart[] robotPart)
    {
        for (int i = 0; i < bluePrintHolder.Count; i++)
        {
            if (!bluePrintHolder[i].activeSelf)
            {
                for (int j = 0; j < robotPart.Length; j++)
                {
                    if (robotPart[j].isBroken)
                    {
                        Debug.Log(robotPart[j].whatPart.ToString());
                        List<GameObject> templimb = bluePrintHolder[i].GetComponent<LimbCheck>().limbs;
                        Debug.Log(templimb.Count);
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
                bluePrintHolder[i].SetActive(true);
                return;
            }
        }
            
    }*/

}
