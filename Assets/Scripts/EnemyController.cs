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
    void Update()
    {
        if(playerTracker.PlayerDetected){
            State = EnemyState.State.Pursuit;
            agent.destination = playerTracker.GetPlayerPosition();
        }else{
            State = EnemyState.State.Patrol;
            if ( Mathf.Abs( Vector3.Distance(this.transform.position, route.GetCurrentRoutePoint() ) ) > 0.1f ) {
                agent.destination = route.GetCurrentRoutePoint();
            } else {
                route.NextRoutePoint();
            }
        }
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
}
