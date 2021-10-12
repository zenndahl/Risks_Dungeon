using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SCRUM : MonoBehaviour
{
    public static int sprints;
    public static int sprintLoops;

    public static void SetSprintRooms(Room room)
    {
        //if is the last room of the sprint, check if it was done at least 4 loops
        //if the loops are not done, the breakpoint room is disabled 
        if(room.isCheckpoint)
        {
            //if the minimum sprint loops is not achieved, disable the breakpoint room
            if(sprintLoops < 4)
            {
                Button checkpoint = room.nextRooms[0];
                checkpoint.gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
                checkpoint.interactable = false;
            }
            
        }

        //if is the breakpoint room, check if it was done at least 2 sprints
        //if it was not done, the last room is disabled 
        if(room.isBreakpoint)
        {
            if(sprints < 4)
            {
                Button breakpoint = room.nextRooms[0];
                breakpoint.gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
                breakpoint.interactable = false;
            }
        }
    }
}
