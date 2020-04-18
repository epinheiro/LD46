using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Range(1, 100)] public float moveSpeed = 30f;
    [Range(1, 360)] public float angularSpeed = 180;

    // Object hierarchy from ROUTE POINTS
    public RouteController route;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        route = route.GetComponent<RouteController>();
        route.SetupRoute();

        SetupNavMeshAgent();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Mathf.Abs( Vector3.Distance(this.transform.position, route.GetCurrentRoutePoint() ) ) > 0.1f ) {
            agent.destination = route.GetCurrentRoutePoint();
            // transform.position = Vector3.MoveTowards(this.transform.position, route.GetCurrentRoutePoint(), moveSpeed * Time.deltaTime);
        } else {
            route.NextRoutePoint();
        }
    }

    void SetupNavMeshAgent(){
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = angularSpeed;
    }
}
