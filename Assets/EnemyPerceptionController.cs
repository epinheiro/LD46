using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPerceptionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        Debug.Log(string.Format("Collision with {0} detected", other.gameObject.name));
    }
}
