using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks {
    [SerializeField] TMP_Text text;
    Player playerdata;
    public void Setup(Player player) {
        playerdata = player;
        text.text = player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        if (playerdata == otherPlayer) {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom() {
        Destroy(gameObject);
    }
}
