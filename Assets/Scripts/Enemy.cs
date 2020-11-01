using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {
    [SerializeField] private float speedY = 2.5f;

    [SerializeField] private float minSpawnX = 2f;
    [SerializeField] private float maxSpawnX = _consts.MaxBoundX;

    [SerializeField] private float minSpawnY = _consts.MinBoundY;
    [SerializeField] private float maxSpawnY = _consts.MaxBoundY;

    
  
    private Player _player;
    
    public SpawnHandler spawnHandler;

    void Start() {
        float spawnX = Random.Range(minSpawnX, maxSpawnX);
        float spawnY = Random.Range(minSpawnY, maxSpawnY);
        transform.position = new Vector3(spawnX, spawnY);
        

        _player = GameObject.FindWithTag("Player").transform.GetComponent<Player>();
        if (_player == null) {
            Debug.LogError("object with tag Player or script didn't find");
        }
    }

    void Update() {
        CalculatePosition();

    }

    private void CalculatePosition() {
        transform.Translate(Vector3.down * (speedY * Time.deltaTime));

        if (transform.position.y < -5f) {
            float newX = Random.Range(minSpawnX, maxSpawnX);
            transform.position = new Vector3(newX, maxSpawnY);
        }
    
    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        string tag = other.tag;
        //if enemy destroying
        if ((tag == "Laser") || (tag == "Player")){
            if (tag == "Laser") {
                _player.UpdateScore();
                Destroy(other.gameObject);
                
            }

            if (tag == "Player") {
                _player.Damage();

            }
            Destroy(this.gameObject);
        }
        
    }
}