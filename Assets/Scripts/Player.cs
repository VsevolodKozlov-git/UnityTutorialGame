using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float speedX = 2.5f;
    [SerializeField] private float speedY = 2.5f;
    
    [SerializeField] private GameObject laserPrefab;
    
    [SerializeField] private float offsetX = 0f;
    [SerializeField] private float offsetY = 0.8f;

    [SerializeField] private float cooldown = 0.3f;
    private float _cooldownTimeNow = 0;
    private float _canFireTime = -1;

    private SpawnHandler _spawnHandler;

    private Canvas _canvas;
    [SerializeField] private int _score;

    [SerializeField] private int lives = 3;

    void Start() {
        transform.position.Set(0, -1, 0);
        
        _spawnHandler = GameObject.FindWithTag("SpawnHandler").transform.GetComponent<SpawnHandler>();
        if (_spawnHandler == null) {
            Debug.LogError("SpawnHandler is null in Player.cs");
        }
        
        _canvas = GameObject.FindWithTag("Canvas").transform.GetComponent<Canvas>();
        if (_canvas == null) {
            Debug.LogError("object with tag Canvas or script didn't find");
        }

    }

    void Update() {
        CalculateMovement();
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFireTime) {
            FireLaser();
        }
    }

    void CalculateMovement() {
        //movement action
        var dX = Input.GetAxis("Horizontal") * speedX;
        var dY = Input.GetAxis("Vertical") * speedY;
        transform.Translate(new Vector3(dX, dY) * Time.deltaTime);
        //calculating movement
        var position = transform.position;
        SetX(RevClamp(position.x, _consts.MinBoundX, _consts.MaxBoundX));
        SetY(Mathf.Clamp(position.y, _consts.MinBoundY, _consts.MaxBoundY));
        
    }
    
    void SetX(float newX) {
        transform.position = new Vector3(newX, transform.position.y);
    }

    void SetY(float newY) {
        transform.position = new Vector3(transform.position.x, newY);
    }

   float RevClamp(float val, float min, float max) {
       if (val > max) {
            return min;
        }
        else if (val < min) {
            return max;
        }

        return val;
   }
   
    void FireLaser() {
        var posVector = transform.position + new Vector3(offsetX, offsetY);
        Instantiate(laserPrefab, posVector, Quaternion.identity);
        _canFireTime = Time.time + cooldown;
    }

    public void Damage() {
        lives--;
        if (lives < 1) {
            _spawnHandler.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void UpdateScore() {
        //you can add more logic here
        _score += 10;
        _canvas.UpdateScore(_score);
    }
}