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
            if(CheckPlayerVisibility(other)){
                enemy.SetPlayerDetection(true);
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            enemy.SetPlayerDetection(false);
        }
    }

    bool CheckPlayerVisibility(Collider player){
        this.GetComponent<MeshCollider>().enabled = false;

        RaycastHit hit;

        Vector3 direction = player.transform.position - this.transform.position;
        Physics.Raycast(this.transform.position, direction, out hit, Mathf.Infinity);

        this.GetComponent<MeshCollider>().enabled = true;

        if(hit.collider.tag == player.tag){
            Debug.DrawLine(this.transform.position, player.transform.position, Color.blue);
            return true;
        }

        return false;
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
