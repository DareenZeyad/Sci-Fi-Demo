using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour{

    [SerializeField] private float _senstivityY = 1.0f;

    void Start(){

    }

    void Update(){
        float _mouseY = Input.GetAxis("Mouse Y");
        Vector3 _newRotation = transform.localEulerAngles;
        _newRotation.x += _mouseY * _senstivityY;
        transform.localEulerAngles = _newRotation;
    }
}
