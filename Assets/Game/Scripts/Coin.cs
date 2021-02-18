using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Player player;
    [SerializeField] private AudioClip _coinClip;

    public void OnTriggerStay(Collider other){
        if (other.tag == "Player"){
            player = other.GetComponent<Player>();
            UIManager uiManagr = GameObject.Find("Canvas").GetComponent<UIManager>();

            if (player != null && Input.GetKeyDown(KeyCode.E)){
                if (uiManagr != null){
                    uiManagr.CollectedCoin();
                }
                player.coinIsCollected = true;
                AudioSource.PlayClipAtPoint(_coinClip, transform.position, 1f);
                Destroy(this.gameObject);
            }
        }
    }
}
