using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour{

    [SerializeField] private float _senstivityX = 1.0f;

    void Start(){
        
    }

    void Update(){
        float _mouseX = Input.GetAxis("Mouse X");
        Vector3 _newRotation = transform.localEulerAngles;
        _newRotation.y += _mouseX * _senstivityX;
        transform.localEulerAngles = _newRotation;
    }
}
