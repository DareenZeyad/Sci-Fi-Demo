using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _ammoNum = 0;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _hitMarkerPrefab;
    [SerializeField] private AudioSource _shootClipPrefab;
    private CharacterController _controller;
    private UIManager _uiManager;
    private float _gravity = 9.81f;
    private bool _isReloding = false;
    public bool coinIsCollected = false;

    void Start() {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        // To hide The Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _ammoNum = 50;
    }

    void Update() {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButton(0) && _ammoNum > 0){
            Shoot();
        }
        else{
            _muzzleFlash.SetActive(false);
            _shootClipPrefab.Stop();
        }
        if (Input.GetKey(KeyCode.R) && _isReloding == false){
            _isReloding = true;
            ShootDelayOn();
        }
    }

    public void Weapon(){
        _weapon.SetActive(true);
    }

    private void Shoot(){
        _ammoNum--;
        _uiManager.UpdateAmmo(_ammoNum);
        if (_shootClipPrefab.isPlaying == false)
            _shootClipPrefab.Play();
        _muzzleFlash.SetActive(true);
        // Raycasting: Create a Ray from the origin of the main camera to the middle of the screen
        
        /// Here we get the center of the screen
        ///Ray rayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        /// It will be more reliable if we get the center of what our camera see "View Point"
        /// The View Point of the camera goes from 0 to 1
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo)){
            Debug.Log("I Hit The: " + hitInfo.transform.name);
            Quaternion rotation = Quaternion.LookRotation(hitInfo.normal);
            GameObject hitMarkerObject = Instantiate(_hitMarkerPrefab, hitInfo.point, rotation) as GameObject;
            Destroy(hitMarkerObject, 1.0f);
        }
        Destructable crate = hitInfo.transform.GetComponent<Destructable>();
        if (crate != null){
            crate.DestroyCrate();
        }
    }

    private void ShootDelayOn(){
        StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay(){
        yield return new WaitForSeconds(1.5f);
        _ammoNum = 50;
        _isReloding = false;
        _uiManager.UpdateAmmo(_ammoNum);
    }

    void CalculateMovement(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        
        // Convert Player's Local Direction to be Global Direction
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }
}