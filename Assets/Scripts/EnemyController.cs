using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Range(1, 100)] public float moveSpeed = 15f;

    // Object hierarchy from ROUTE POINTS
    public RouteController route;


    
    // Start is called before the first frame update
    void Start()
    {
        route = route.GetComponent<RouteController>();
        route.SetupRoute();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Mathf.Abs( Vector3.Distance(this.transform.position, route.GetCurrentRoutePoint() ) ) > 0.1f ) {
            transform.position = Vector3.MoveTowards(this.transform.position, route.GetCurrentRoutePoint(), moveSpeed * Time.deltaTime);
        } else {
            route.NextRoutePoint();
        }
    }
}
