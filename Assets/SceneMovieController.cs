using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMovieController : MonoBehaviour
{
    public Transform initialPosition;
    public Animator animator;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController.MoveTo(initialPosition.position.x, initialPosition.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
