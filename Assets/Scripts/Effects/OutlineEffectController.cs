using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class OutlineEffectController : MonoBehaviour
{
    GameObject[] agents;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] aux;
        aux = GameObject.FindGameObjectsWithTag("EnemyOutline");
        agents = new GameObject[aux.Length+1];
        for( int i=0; i<aux.Length; i++){
            agents[i] = aux[i];
        }
        agents[aux.Length] = GameObject.FindGameObjectsWithTag("PlayerOutline")[0];
        
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject go in agents){
            try{
                if(IsVisibleFrom(GetRenderer(go), cam)){
                    RaycastHit hit;
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    
                    Vector3 camPosition = cam.transform.position;
                    Vector3 agentPosition = go.transform.position;

                    Vector3 rayDirection = agentPosition - camPosition;
                    float distance = Vector3.Distance(camPosition, agentPosition);


                    if (Physics.Raycast(camPosition, rayDirection, out hit, distance)) {
                        if(hit.collider.gameObject.tag == go.gameObject.tag){
                            go.transform.GetComponent<cakeslice.Outline>().enabled = false;
                        }else{
                            go.transform.GetComponent<cakeslice.Outline>().enabled = true;
                        }
                    }
                }
            }catch(System.Exception e){}

        }
    }

    Renderer GetRenderer(GameObject tested){

        SkinnedMeshRenderer sMRenderer = this.GetComponent<SkinnedMeshRenderer>();
        if(sMRenderer != null) return sMRenderer;

        Renderer renderer = this.GetComponent<Renderer>();
        if(renderer != null) return renderer;

        int childCount = tested.transform.childCount;

        if(childCount != 0){
            for( int i=0; i<childCount; i++){
                Renderer innerRenderer =  GetRenderer(tested.transform.GetChild(i).gameObject);
                if(innerRenderer!=null) return innerRenderer;
            }
            return null;
        }else{
            throw new System.Exception(string.Format("Object {0} does not have any renderer in all components and childs", this.name));
        }
    }

    // http://wiki.unity3d.com/index.php?title=IsVisibleFrom&_ga=2.234151766.913360.1587213094-1189076746.1553032315
    bool IsVisibleFrom(Renderer renderer, Camera camera){
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}
