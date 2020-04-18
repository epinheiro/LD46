using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPerceptionController : MonoBehaviour
{
    EnemyController enemy;
    Coroutine rescaleCoroutine;

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

    public void ChangeConeScale(Vector3 newScale){
        if(rescaleCoroutine != null){
            StopCoroutine(rescaleCoroutine);
        }
        rescaleCoroutine = StartCoroutine(GraduallyChangeConeScale(newScale));
    }

    IEnumerator GraduallyChangeConeScale(Vector3 endScale, float time = 1){
        float hardcodedTimeStep = 0.01f;

        Vector3 start = this.transform.localScale;
        Vector3 step = (endScale - start)/(time/hardcodedTimeStep);

        float timeCount = 0;

        while(timeCount < time){
            timeCount += hardcodedTimeStep;
            this.transform.localScale += step;
            yield return new WaitForSeconds(hardcodedTimeStep);
        }

        this.transform.localScale = endScale;
    }
}
