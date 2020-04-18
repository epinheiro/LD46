using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Range(1, 15)] public float moveSpeed = 15f;

    // Object hierarchy from ROUTE POINTS
    public GameObject routeGameObject;

    Vector3[] route;
    int currentRoutePoint;
    
    // Start is called before the first frame update
    void Start()
    {
        SetRoutePoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(this.transform.position, route[currentRoutePoint])) > 0.1f) {
            transform.position = Vector3.MoveTowards(this.transform.position, route[currentRoutePoint], moveSpeed * Time.deltaTime);
        } else {
            IncreaseRoutePointCount();
        }
    }

    void SetRoutePoints() {
        int childCount = routeGameObject.transform.childCount;

        route = new Vector3[childCount];
        
        for ( int i=0; i<childCount; i++ ) {
            route[i] = routeGameObject.transform.GetChild(i).transform.position;
        }

        currentRoutePoint = 0;
    }
    
    void IncreaseRoutePointCount() {
        int childCount = routeGameObject.transform.childCount;

        currentRoutePoint = (currentRoutePoint + 1) % childCount;
    }
}
