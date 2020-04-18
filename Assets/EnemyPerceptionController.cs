using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPerceptionController : MonoBehaviour
{
    EnemyController enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            enemy.SetPlayerDetection(true);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            enemy.SetPlayerDetection(false);
        }
    }
}
