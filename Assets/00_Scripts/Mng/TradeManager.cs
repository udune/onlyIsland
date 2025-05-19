using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviourPunCallbacks
{
    // public Dictionary<string, int> playerItems = new Dictionary<string, int>(); // �� �ŷ� ������
    // public Dictionary<string, int> otherPlayerItems = new Dictionary<string, int>(); // ������ �ŷ� ������
    //
    // private bool isComfirmed = false;
    // private bool otherConfirmed = false;
    //
    // private Player otherPlayer;
    //
    // public void StartTrade(Player targetPlayer)
    // {
    //     if (targetPlayer == null) return;
    //     otherPlayer = targetPlayer;
    //
    //     photonView.RPC("RPC_ReceivePlayerRequest", targetPlayer, PhotonNetwork.LocalPlayer);
    // }
    // [PunRPC]
    // public void RPC_ReceivePlayerRequest(Player requester)
    // {
    //     otherPlayer = requester;
    //     Debug.Log(otherPlayer);
    // }
    // public void RequestTrade()
    // {
    //     TradeUI.instance.gameObject.SetActive(true);
    //     photonView.RPC("RPC_ReceiveTradeRequest", otherPlayer);
    // }
    //
    // [PunRPC]
    // public void RPC_ReceiveTradeRequest()
    // {
    //     TradeUI.instance.gameObject.SetActive(true);
    // }
    //
    // public void AddItemToTrade(string itemName, int count)
    // {
    //     if (!BaseManager.Inventory.inventory.ContainsKey(itemName))
    //     {
    //         Debug.Log("�����ϰ����� ���� �������Դϴ�.");
    //     }
    //
    //     if(!playerItems.ContainsKey(itemName))
    //         playerItems[itemName] = 0;
    //
    //     playerItems[itemName] += count;
    //     TradeUI.instance.SetHolderData();
    //     photonView.RPC("RPC_UpdateTradeItems", otherPlayer, playerItems);
    // }
    //
    // public void ConfirmTrade()
    // {
    //     isComfirmed = true;
    //     photonView.RPC("RPC_ConfirmTrade", otherPlayer);
    //
    //     CheckTradeCompletion();
    // }
    //
    // [PunRPC]
    // public void RPC_ConfirmTrade()
    // {
    //     otherConfirmed = true;
    //     CheckTradeCompletion();
    // }
    //
    // private void CheckTradeCompletion()
    // {
    //     if(isComfirmed)
    //     {
    //         TradeUI.instance.GetComfirm(true);
    //     }
    //     if(otherConfirmed)
    //     {
    //         TradeUI.instance.GetComfirm(false);
    //     }
    //
    //     if (isComfirmed && otherConfirmed)
    //     {
    //         Debug.Log("�ŷ� �Ϸ�!");
    //
    //         ExchangeItems();
    //
    //         ResetTrade();
    //     }
    // }
    //
    // private void ExchangeItems()
    // {
    //     foreach(var item in playerItems)
    //     {
    //         BaseManager.Inventory.RemoveItem(item.Key, item.Value);
    //     }
    //
    //     foreach(var item in otherPlayerItems)
    //     {
    //         BaseManager.Inventory.AddItem(item.Key, item.Value);
    //     }
    //
    //     ToastPopUPManager.instance.Initalize("�ŷ��� ���������� �Ϸ��Ͽ����ϴ�!");
    // }
    //
    // [PunRPC]
    // public void RPC_UpdateTradeItems(Dictionary<string, int> updatedItems)
    // {
    //     otherPlayerItems = updatedItems;
    //     TradeUI.instance.SetHolderData();
    //     Debug.Log("������ �ŷ� �������� ������Ʈ�Ǿ����ϴ�.");
    // }
    //
    // public void CancelTrade()
    // {
    //     ResetTrade();
    //     photonView.RPC("RPC_CancelTrade", otherPlayer);
    //     ToastPopUPManager.instance.Initalize("�ŷ��� ��ҵǾ����ϴ�.");
    // }
    //
    // [PunRPC]
    // public void RPC_CancelTrade()
    // {
    //     ToastPopUPManager.instance.Initalize("�ŷ��� ��ҵǾ����ϴ�.");
    //     ResetTrade();
    // }
    //
    // private void ResetTrade()
    // {
    //     playerItems.Clear();
    //     otherPlayerItems.Clear();
    //     isComfirmed = false;
    //     otherConfirmed = false;
    //     TradeUI.instance.GetComponent<Animator>().SetTrigger("Hide");
    //     Debug.Log("�ŷ� �ʱ�ȭ �Ϸ�!");
    // }
}
