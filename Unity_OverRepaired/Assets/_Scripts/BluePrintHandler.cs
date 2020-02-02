using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BluePrintHandler : MonoBehaviour
{
    public List<GameObject> bluePrintHolder;
    public string name;
    public Sprite[] images = new Sprite[4];
    public List<Sprite> robotSprites;
    private Dictionary<string, Sprite> lookUpTable = new Dictionary<string, Sprite>();
    private int index = 0;

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
    /*public void ChangeRobotIcon(string type, GameObject bluePrintHolder)
    {
  
            if (type == "LeftArm" || type == "RightArm")
            {
                bluePrintHolder.GetComponent<Icons>().robotPart.sprite = robotSprites[0];
            }
            if (type == "LeftLeg" || type == "RightLeg")
            {
                bluePrintHolder.GetComponent<Icons>().robotPart.sprite = robotSprites[1];
            }
            if (type == "Head")
            {
                bluePrintHolder.GetComponent<Icons>().robotPart.sprite = robotSprites[2];
            }
    }*/

    public void removeOldBluePrints(BrokenRobot br) {
        for (int y = 0; y < br.howManyBrokenParts; y++) {
            for (int i = 0; i < (bluePrintHolder.Count - 1); i++)
            {
                var tempParentRight = bluePrintHolder[i + 1].transform.parent;
                var tempParentLeft = bluePrintHolder[i].transform.parent;
                Destroy(bluePrintHolder[i].gameObject);
                var temp = Instantiate(bluePrintHolder[i + 1], tempParentLeft);
                temp.name = "index" + i;
                bluePrintHolder[i] = temp;
                
            }
        }
        for (int i = 0; i < br.howManyBrokenParts; i++)
        {
            bluePrintHolder[(bluePrintHolder.Count - 1) - i].SetActive(false);
        }
        index -= br.howManyBrokenParts;
    }

    public void CreateBluePrints(RobotPart[] robotPart)
    {
        for (int j = 0; j < robotPart.Length; j++)
        {
            if (robotPart[j].isBroken) {

                int i = index;
                index++;
                for (int u = 0; u < robotPart[j].repairOrder.Length; u++)
                {
                    bluePrintHolder[i].GetComponent<Icons>().icons[u].gameObject.SetActive(true);
                    if (u < robotPart[j].repairOrderStack.Count)
                    {
                        int offset = robotPart[j].repairOrder.Length - robotPart[j].repairOrderStack.Count;
                        //ChangeRobotIcon(robotPart[j].whatPart.ToString(), bluePrintHolder[i]);
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
