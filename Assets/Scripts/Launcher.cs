using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks {
    public static Launcher Instance;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to server");
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to server");
    }

    public override void OnJoinedLobby() {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
    }

    public void CreateRoom() {
        if (string.IsNullOrEmpty(roomNameInputField.text)) Debug.Log("Please enter room name");
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom() {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        errorText.text = "Room Creation failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom(){
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        foreach (Transform item in roomListContent) {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++) {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().Setup(roomList[i]);
        }
    }

    public void JoinRoom(RoomInfo info) {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }
}
