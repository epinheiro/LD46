using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
    public GameObject roof;
    private int collided = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)        
    {
        if (other.tag != "Player") return;
        //Debug.Log("enter collider");
        collided += 1;
        roof.SetActive(false);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        //Debug.Log("exit collider");
        collided -= 1;
        if (collided <= 0)
            roof.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
