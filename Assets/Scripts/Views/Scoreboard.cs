using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Scoreboard : MonoBehaviourPunCallbacks {
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPreb;
    [SerializeField] CanvasGroup canvasGroup;

    Dictionary<Player, ScoreboardItem> scoreboardItems = new Dictionary<Player, ScoreboardItem>();

    void Start() {
        foreach(Player player in PhotonNetwork.PlayerList) {
            AddScoreboardItem(player);
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            canvasGroup.alpha = 1;
        }
        else if(Input.GetKeyUp(KeyCode.Tab)) {
            canvasGroup.alpha = 0;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        AddScoreboardItem(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        RemoveScoreboardItem(otherPlayer);
    }

    void AddScoreboardItem(Player player) {
        ScoreboardItem item = Instantiate(scoreboardItemPreb, container).GetComponent<ScoreboardItem>();
        item.Initialize(player);
        scoreboardItems[player] = item;
    }

    void RemoveScoreboardItem(Player player) {
        Destroy(scoreboardItems[player].gameObject);
        scoreboardItems.Remove(player);
    }
}
