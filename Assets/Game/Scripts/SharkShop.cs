using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour{
    private Player player;
    private AudioSource _youWinClip;

    private void OnTriggerStay(Collider other){
        Player player = other.GetComponent<Player>();
        UIManager uiManagr = GameObject.Find("Canvas").GetComponent<UIManager>();
        _youWinClip = GetComponent<AudioSource>();

        if (other.tag == "Player"){
            Debug.Log("Welcome, Player");
            if (Input.GetKeyDown(KeyCode.E) && player != null){
                if (player.coinIsCollected == true){
                    player.coinIsCollected = false;
                    player.Weapon();
                    if(uiManagr != null)
                        uiManagr.RemoveCoin();
                    _youWinClip.Play();
                }
                else Debug.Log("Get Out Of Here!");
            }
        }
    }
}
