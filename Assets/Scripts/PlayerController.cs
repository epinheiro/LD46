using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(1,15)] public float moveSpeed = 2.0f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    private void Move() {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(CalculateSpeed(horizontal), 0, CalculateSpeed(vertical));
    }

    private float CalculateSpeed(float direction) {
        return direction * moveSpeed * Time.deltaTime;
    }
}
