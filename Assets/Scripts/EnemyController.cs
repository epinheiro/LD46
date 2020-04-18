using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Dictionary<EnemyState.State, EnemyState> statesDict;
    EnemyState.State currentState;
    public EnemyState.State State{
        get { return currentState; }
        set {
            if(value != currentState){
                currentState = value;
                ChangeStateAttributes(value);
            }
        }
    }

    [Range(1, 100)] public float moveSpeed = 30f;
    [Range(1, 360)] public float angularSpeed = 360;

    // Object hierarchy from ROUTE POINTS
    public RouteController route;

    NavMeshAgent agent;
    PlayerTracker playerTracker;
    EnemyPerceptionController perception;
    bool isPursuing = false;
    Coroutine currentBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        perception = transform.Find("VisionCone").GetComponent<EnemyPerceptionController>();

        statesDict = EnemyState.GetStatesDictionary();
        currentState = EnemyState.State.Patrol;

        route = route.GetComponent<RouteController>();
        route.SetupRoute();

        SetupNavMeshAgent();

        playerTracker = this.GetComponent<PlayerTracker>();
    }

    // Update is called once per frame
    void Update(){
        if(!isPursuing){
            if(!playerTracker.PlayerDetected){
                // Patrolling
                State = EnemyState.State.Patrol;
                if ( Mathf.Abs( Vector3.Distance(this.transform.position, route.GetCurrentRoutePoint() ) ) > 1f ) {
                    agent.destination = route.GetCurrentRoutePoint();
                } else {
                    route.NextRoutePoint();
                }
            }else{
                // Begin pursuit
                isPursuing = true;
            }
        }else{ // Pursuit!
            if(playerTracker.PlayerDetected){
                if(currentBehaviour != null){
                    // Continue Pursuit case
                    StopCoroutine(currentBehaviour);
                    currentBehaviour = null;
                }
                State = EnemyState.State.Pursuit;
                agent.destination = playerTracker.GetPlayerPosition();
            }else{
                if(currentBehaviour == null){
                    // Begin close search
                    agent.destination = this.transform.position;
                    State = EnemyState.State.CloseSearch;
                    currentBehaviour = StartCoroutine(CloseSearchPlayerBehaviour());
                }
            }
        }
    }

    IEnumerator CloseSearchPlayerBehaviour(float searchAngle = 90, float time = 1.5f){
        // Return a random integer number between min [inclusive] and max [EXCLUSIVE] (Read Only).
        int startingSide = Random.Range(0,2) == 0 ? -1 : 1; // Random between 0 and 1

        float hardcodedTimeStep = 0.01f;

        float rotateStep = (3*searchAngle*startingSide)/(time/hardcodedTimeStep);

        float timeCount = 0;

        while(timeCount < time/3){
            yield return CloseSearchRotationLogic(ref timeCount, hardcodedTimeStep, rotateStep);
        }

        while(timeCount < time){
            yield return CloseSearchRotationLogic(ref timeCount, hardcodedTimeStep, -rotateStep);
        }

        isPursuing = false;
    }

    WaitForSeconds CloseSearchRotationLogic(ref float timeCount, float timeStep, float rotateStep){
        timeCount += timeStep;

        this.transform.Rotate(Vector3.up, -rotateStep, Space.Self);

        return new WaitForSeconds(timeStep);
    }

    void ChangeStateAttributes(EnemyState.State nextState){
        EnemyState data;
        statesDict.TryGetValue(nextState, out data);
        moveSpeed = data.moveSpeed;
        angularSpeed = data.angularSpeed;

        perception.ChangeConeScale(data.visibilityConeScale);
    }

    void SetupNavMeshAgent(){
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = angularSpeed;
    }

    public void SetPlayerDetection(bool isDetected){
        playerTracker.PlayerDetected  = isDetected;
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Time.timeScale = 0;
            CaughtPlayerEvent();
        }
    }

    void CaughtPlayerEvent(){
        Debug.Log(string.Format("Enemy {0} caught player", this.name));
    }
}
