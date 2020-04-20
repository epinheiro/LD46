using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovieController : MonoBehaviour
{
    private Vector3 moveToPosition;
    private bool moveScene = false;
    private Animator animator;
    [Range(1, 100)] public float moveSpeed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
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
        }
    }

    public void MoveTo(float x, float z)
    {
        moveToPosition = new Vector3(x, 0, z);
        moveScene = true;
    }

    public void LookAtVector3(Vector3 look)
    {
        look.y = 0;
        transform.LookAt(look);
    }

    public void Move(float horizontal, float vertical)
    {
        Vector3 direction = transform.position + new Vector3(horizontal, 0, vertical);
        transform.Translate(CalculateSpeed(horizontal), 0, CalculateSpeed(vertical), Space.World);
        transform.LookAt(direction);
        //Debug.Log("Horitzontal: " +  horizontal + "Vertical :" + vertical);
        if (horizontal == 0 && vertical == 0)
        {
            animator.Play("idle");
        }
        else
        {
            animator.Play("run");
        }
    }

    private float CalculateSpeed(float direction)
    {
        return direction * moveSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
