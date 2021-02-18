using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour{
    [SerializeField] private GameObject _woodenCratePrefab;

    public void DestroyCrate(){
        Instantiate(_woodenCratePrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
