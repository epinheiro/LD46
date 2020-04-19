using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(1, 100)] public float moveSpeed = 40f;
    public float acceleration = 5f;
    public Rigidbody characterRigidBody;
    public Animator animator;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void FixedUpdate() {
        Move();
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            animator.Play("idle");
        } else
        {
            animator.Play("run");
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = characterRigidBody.transform.position + new Vector3(horizontal, 0, vertical);
        transform.Translate(CalculateSpeed(horizontal), 0, CalculateSpeed(vertical));
        characterRigidBody.transform.LookAt(direction);
    }

    private float CalculateSpeed(float direction) {
        return direction * moveSpeed * Time.deltaTime;
    }
}
