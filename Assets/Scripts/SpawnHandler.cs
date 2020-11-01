using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour {
  [SerializeField] private float spawnDecay;
  [SerializeField] private int enemyLimit;
  private int _enemyOnBoard = 0;
  
  
  [SerializeField] private GameObject enemyObj;
  [SerializeField] private GameObject enemyContainerObj;
  
  private Coroutine _spawnCoroutine;
  void Update() {
    
  }

  void Start() {
    _spawnCoroutine = StartCoroutine(Spawn());
  }

  public void OnPlayerDeath() {
    StopCoroutine(_spawnCoroutine);
  }

  private IEnumerator Spawn() {
    while (true) {
      if (_enemyOnBoard < enemyLimit) {
        Instantiate(enemyObj, enemyContainerObj.transform, true);
        _enemyOnBoard++;
        yield return new WaitForSeconds(spawnDecay);
      }
      else {
        yield return new WaitForSeconds(0.5f);
      }
    }
  }



  //   [SerializeField] private static int defaultEnemy = 3;
  //   
  //   [HideInInspector] public int enemyOnScene = 0;
  //   
  //   [SerializeField] private GameObject enemy;
  //   
  // //be sure to assign this in the inspector to your main camera
  //
  // void Start() {
  //     spawn();
  // }
  //
  //   void Update() {
  //       
  //       if (enemyOnScene == 0) {
  //           spawn();
  //       }
  //   }
  //
  //   void spawn() {
  //       for (int i = 0; i < defaultEnemy; i++) {
  //           GameObject enemyObj = Instantiate(enemy);
  //           enemyObj.GetComponent<Enemy>().spawnHandler = this;
  //           enemyOnScene++;
  //           print(enemyOnScene);
  //       }
  //       
  //   }
    
}
