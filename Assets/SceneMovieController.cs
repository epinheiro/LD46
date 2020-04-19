using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMovieController : MonoBehaviour
{
    public Transform initialPositionPlayer;
    public Transform initialPositionKnight;
    public Transform door;
    public Transform inside;
    public Animator animator;
    public PlayerController playerController;
    public KnightMovieController knightController;
    private bool wentInside = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController.MoveTo(initialPositionPlayer.position.x, initialPositionPlayer.position.z);
        knightController.MoveTo(initialPositionKnight.position.x, initialPositionKnight.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.MoveSceneValue() && wentInside)
        {
            playerController.MoveTo(inside.position.x, inside.position.z);
            //GameObject.Destroy(this);
            this.enabled = false;
        }
        
        
    }
    public void Inside()
    {
        playerController.MoveTo(door.position.x, door.position.z);
        wentInside = true;

    }
}
