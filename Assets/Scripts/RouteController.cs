using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : MonoBehaviour
{
    Vector3[] route;
    int currentRoutePoint;

    public Vector3 GetCurrentRoutePoint(){
        return route[currentRoutePoint];
    }

    public void SetupRoute() {
        int childCount = this.transform.childCount;

        route = new Vector3[childCount];
        
        for ( int i=0; i<childCount; i++ ) {
            route[i] = this.transform.GetChild(i).transform.position;
        }

        currentRoutePoint = 0;
    }
    
    public Vector3 NextRoutePoint() {
        int childCount = this.transform.childCount;

        currentRoutePoint = (currentRoutePoint + 1) % childCount;

        return GetCurrentRoutePoint();
    }
}
