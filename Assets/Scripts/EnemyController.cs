using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Range(1, 100)] public float moveSpeed = 30f;
    [Range(1, 360)] public float angularSpeed = 360;

    // Object hierarchy from ROUTE POINTS
    public RouteController route;

    NavMeshAgent agent;
    PlayerTracker playerTracker;

    // Start is called before the first frame update
    void Start()
    {
        route = route.GetComponent<RouteController>();
        route.SetupRoute();

        SetupNavMeshAgent();

        playerTracker = this.GetComponent<PlayerTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTracker.PlayerDetected){
            agent.destination = playerTracker.GetPlayerPosition();
        }else{
            if ( Mathf.Abs( Vector3.Distance(this.transform.position, route.GetCurrentRoutePoint() ) ) > 0.1f ) {
                agent.destination = route.GetCurrentRoutePoint();
            } else {
                route.NextRoutePoint();
            }
        }
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
