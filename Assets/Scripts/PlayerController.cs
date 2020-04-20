using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(1, 100)] public float moveSpeed = 40f;
    public float acceleration = 5f;
    public Transform characterTransform;
    private Animator animator;
    private Vector3 moveToPosition;
    private bool moveScene = false;
    private bool characterActive = false;
    

    // Start is called before the first frame update
    void Start() {
       // characterRigidBody = GetComponentInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (moveScene)
        {
            Vector3 vec = new Vector3(moveToPosition.x - transform.position.x, transform.position.y, moveToPosition.z - transform.position.z).normalized;
            Move(vec.x, vec.z);
            //Debug.Log("X: " + Mathf.Abs(transform.position.x - moveToPosition.x) + " Z: " + Mathf.Abs(transform.position.z - moveToPosition.z));
            if (Mathf.Abs(transform.position.x - moveToPosition.x) < 1 && Mathf.Abs(transform.position.z - moveToPosition.z) < 1)
            {
                moveScene = false;
                animator.Play("idle");
            }
        } else if (characterActive)
        {
            Move(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));
        }


        //transform.position = characterRigidBody.transform.position;
        //theCamera.transform.position = new Vector3(transform.position.x, theCamera.transform.position.y, transform.position.z);
    }

    public void activateCharacter ()
    {
        characterActive = true;
    }

    public void deactivateCharacter()
    {
        characterActive = false;
        animator.Play("idle");
    }

    public bool MoveSceneValue()
    {
        return moveScene;
    }

    public void MoveTo(float x, float z)
    {
        moveToPosition = new Vector3(x, 0, z);
        moveScene = true;
    }

    public void LookAtVector3(Vector3 look)
    {
        look.y = 0;
        characterTransform.LookAt(look);
    }

    public void Move(float horizontal, float vertical)
    {
        Vector3 direction = characterTransform.position + new Vector3(horizontal, 0, vertical);
        transform.Translate(CalculateSpeed(horizontal), 0, CalculateSpeed(vertical));
        characterTransform.LookAt(direction);
        if (horizontal == 0 && vertical == 0)
        {
            animator.Play("idle");
        }
        else
        {
            animator.Play("run");
        }
    }

    private float CalculateSpeed(float direction) {
        return direction * moveSpeed * Time.deltaTime;
    }
}
