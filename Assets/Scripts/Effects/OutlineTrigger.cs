using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineTrigger : MonoBehaviour
{
    GameObject[] outlines;

    void Start()
    {
        GameObject[] enemies  = GameObject.FindGameObjectsWithTag("EnemyOutline");
        GameObject[] peasants = GameObject.FindGameObjectsWithTag("PeasantOutilne"); // Sadly i made the tag wrong and will postpone the change
        GameObject[] doorways = GameObject.FindGameObjectsWithTag("DoorwayOutline");
        GameObject   player   = GameObject.FindGameObjectWithTag("PlayerOutline");

        outlines = new GameObject[1 + enemies.Length + peasants.Length + doorways.Length];

        enemies.CopyTo(outlines, 0);
        peasants.CopyTo(outlines, enemies.Length);
        doorways.CopyTo(outlines, enemies.Length + peasants.Length);
        outlines[outlines.Length-1] = player;

        SetActiveOutline(false);
    }

    public void SetActiveOutline(bool isActive){
        foreach(GameObject go in outlines){
            go.transform.GetComponent<cakeslice.Outline>().enabled = isActive;
        }
    }
}
