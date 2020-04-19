using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
    public GameObject roof;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        roof.SetActive(false);
    }

    void OnTriggerExit(Collider other)
    {
        roof.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
