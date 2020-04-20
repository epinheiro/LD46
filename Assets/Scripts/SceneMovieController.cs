using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SceneMovieController : MonoBehaviour
{
    public Transform initialPositionPlayer;
    public Transform initialPositionKnight;
    public Transform door;
    public Transform inside;
    public Animator animator;
    public PlayerController playerController;
    public KnightMovieController knightController;
    public PlayableDirector playableDirector;
    private bool wentInside = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PauseScene()
    {
        playableDirector.Pause();

    }

    public void Activate()
    {
        playerController.MoveTo(initialPositionPlayer.position.x, initialPositionPlayer.position.z);
        knightController.MoveTo(initialPositionKnight.position.x, initialPositionKnight.position.z);
    }

    public void LookAtEachOther()
    {
        playerController.LookAtVector3(knightController.transform.position);
        knightController.LookAtVector3(playerController.transform.position);
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
