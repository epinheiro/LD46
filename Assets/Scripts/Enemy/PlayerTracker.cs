using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public bool DebugLines;

    public GameObject player;

    bool isPlayerDetected = false;
    public bool PlayerDetected{
        get { return isPlayerDetected; }
        set { isPlayerDetected = value; }
    }

    public Vector3 GetPlayerPosition(){
        return player.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
       // player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
