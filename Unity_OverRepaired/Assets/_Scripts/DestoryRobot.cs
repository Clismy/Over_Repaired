using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryRobot : MonoBehaviour
{
    public ScoreBored sb;
    private BrokenRobot br;
    public BluePrintHandler bph;
    private void OnTriggerEnter(Collider collision)
    {
        var robot = collision.gameObject.GetComponent<BrokenRobot>();
        br = robot;
        if (robot != null)
        {
            UpdateScore();
            Destroy(robot.gameObject,4);
            collision.GetComponent<Collider>().enabled = false;
        }
    }

    private void UpdateScore()
    {
        int numberOfBrokenParts = 0;
        for (int i = 0; i < br.parts.Length; i++)
        {
            if (br.transform.GetChild(i).childCount > 0)
            {
                if (!br.transform.GetChild(i).GetChild(0).GetComponent<RobotPart>().isBroken)
                {
                    sb.IncreaseScore(10);
                    continue;
                }
            }
            numberOfBrokenParts++;
        }
        bph.removeOldBluePrints(br);
    }
}
