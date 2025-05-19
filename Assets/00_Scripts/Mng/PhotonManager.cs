// using UnityEngine;
// using Photon.Pun;
// using Photon.Realtime;
// using ExitGames.Client.Photon;
// public class PhotonManager : MonoBehaviourPunCallbacks
// {
//     public const byte BID_EVENT = 1;
//     public const byte AUCTION_COMPLETE_EVENT = 3;
//     private void Start()
//     {
//         // Photon ���� ����
//         PhotonNetwork.ConnectUsingSettings();
//     }
//
//     public override void OnConnectedToMaster()
//     {
//         Debug.Log("���� ������ ������ �����Ͽ����ϴ�.");
//         // ���� �뿡 �����ϰų� ���ο� ���� ����
//         PhotonNetwork.JoinRandomRoom();
//     }
//
//     public override void OnJoinRandomFailed(short returnCode, string message)
//     {
//         Debug.Log("�� ������ �����Ͽ����ϴ�. ���� ���� ����ϴ�.");
//         PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
//     }
//
//     public override void OnJoinedRoom()
//     {
//         Debug.Log("�뿡 �����Ͽ����ϴ�.");
//         SpawnPlayer();
//         ChatManager.instance.Initalize();
//         BubbleUIManager.instance.InitalzieBubble();
//     }
//     // Instantiate - ������
//     // Destroy - �ı���
//     void SpawnPlayer()
//     {
//         Vector3 spawnPosition = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f));
//         GameObject playerObject = PhotonNetwork.Instantiate("PlayerPrefab", spawnPosition, Quaternion.identity);
//
//         int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
//         playerObject.GetComponent<PlayerController>().Initalize(actorNumber);
//         Camera.main.GetComponent<CameraController>().Initalize(playerObject.transform);
//
//         PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
//     }
//     // RPC -> Remote Procedure call 
//     // RaiseEvent
//     // ���� -> RPC = ���� �ش� ��ü�� PhotonView�� ���� ȣ��
//     // ���� -> RaiseEvent = �̺�Ʈ ��� ( Photon ������ ���� ���޵� )
//     public static void NotifyAuctionCompleted(string auctionID, string winnberNick)
//     {
//         object[] content = new object[] { auctionID, winnberNick };
//         RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
//         PhotonNetwork.RaiseEvent(AUCTION_COMPLETE_EVENT, content, options, SendOptions.SendReliable);
//     }
//
//     public static void NotifyBidPlaced(string auctionId, string bidderNick, int currentPrice)
//     {
//         object[] content = new object[] { auctionId, bidderNick, currentPrice };
//         // Photon������ RaiseEvent()�� object[] Ÿ���� �����͸� ������ �� ����.
//         RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
//         PhotonNetwork.RaiseEvent(BID_EVENT, content, options, SendOptions.SendReliable);
//     }
//
//     public override void OnDisable()
//     {
//         PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
//     }
//
//     private void OnEvent(EventData photonEvent)
//     {
//         if(photonEvent.Code == BID_EVENT)
//         {
//             object[] data = (object[])photonEvent.CustomData;
//             string auctionID = (string)data[0];
//             string bidderNick = (string)data[1];
//             string currentPrice = ((int)data[2]).ToString();
//
//             UpdateAuctionUI(auctionID, bidderNick, currentPrice);
//         }
//
//         if(photonEvent.Code == AUCTION_COMPLETE_EVENT)
//         {
//             object[] data = (object[])photonEvent.CustomData;
//             string auctionID = (string)data[0];
//             string winnerNick = (string)data[1];
//             RemoveAuctionFromUI(auctionID);
//         }
//     }
//     private void RemoveAuctionFromUI(string auctionID)
//     {
//         ToastPopUPManager.instance.Initalize(
//             string.Format("'{0}'�������� �ǸŵǾ����ϴ�.", auctionID));
//     }
//
//     private void UpdateAuctionUI(string auctionID, string bidderNick, string currentPrice)
//     {
//         ToastPopUPManager.instance.Initalize(
//             string.Format("'{1}'���� ������ ���� �Ͽ����ϴ�. ������:{2}",
//             auctionID, bidderNick, currentPrice));
//     }
// }

using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Transform startPos;
    
    private void Start()
    {
        // Photon 서버 연결
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("포톤 마스터 서버에 연결하였습니다.");
        // 랜덤 룸에 참가하거나 새로운 룸을 생성
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방 참가에 실패하였습니다. 방을 새로 만듭니다.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("룸에 접속했습니다.");
        SpawnPlayer();
        ChatManager.instance.Initialize();
        BubbleUIManager.instance.InitializeBubble();
    }

    // Instantiate - 생성자
    // Destroy - 파괴자
    void SpawnPlayer()
    {
        Vector3 spawnPos = new Vector3(Random.Range(startPos.localPosition.x - 5.0f, startPos.localPosition.x + 5.0f), startPos.localPosition.y, Random.Range(startPos.localPosition.z - 5.0f, startPos.localPosition.z + 5.0f));
        GameObject playerObj = PhotonNetwork.Instantiate("PlayerPrefab", spawnPos, Quaternion.identity);
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        playerObj.GetComponent<PlayerController>().Initialize(actorNumber);
    }
}