using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField] private int speedX;
    void Start() {
        
    }
    
    void Update() {
        CalculateMovement();
    }

    private void CalculateMovement() {
        transform.Translate(Vector3.up  * (Time.deltaTime * speedX));

        if (transform.position.y >= _consts.MaxBoundY) {
            Destroy(this.gameObject);

        }
    }
}
