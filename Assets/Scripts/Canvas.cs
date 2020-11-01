using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Canvas : MonoBehaviour {
    
    [SerializeField] private Text _score;

    [SerializeField] private Player _player;
    void Start() {
        UpdateScore(0);
    }

    public void UpdateScore(int value) {
        _score.text = $"Score: {value}";
    }
}
