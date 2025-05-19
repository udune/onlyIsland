// using Photon.Pun;
// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.U2D;
// public enum Action_State
// {
//     None = 0,
//     InviteParty,
//     Trade,
//     InviteGuild
// }
// public class ActionHolder : MonoBehaviourPunCallbacks
// {
//     public static SpriteAtlas Atlas;
//     public static Dictionary<Action_State, Action> Actions = new Dictionary<Action_State, Action>();
//     public static PhotonView photonView;
//     public static int TargetPlayerIndex;
//     public static Sprite GetAtlas(string temp)
//     {
//         return Atlas.GetSprite(temp);
//     }
//
//     private void Start()
//     {
//         photonView = GetComponent<PhotonView>();
//
//         Atlas = Resources.Load<SpriteAtlas>("Atlas");
//
//         Actions[Action_State.InviteParty] = InviteParty;
//         Actions[Action_State.Trade] = Trade;
//         Actions[Action_State.InviteGuild] = InviteGuild;
//     }
//     #region Party
//     public static void InviteParty()
//     {
//         string Toast = string.Format("<color=#FFFF00>{0}</color>�Կ��� ��Ƽ�� �ʴ��Ͽ����ϴ�.",
//          PhotonHelper.GetPlayerNickName(TargetPlayerIndex));
//
//         ToastPopUPManager.instance.Initalize(Toast);
//
//         photonView.RPC("ReceivePartyInvite", PhotonHelper.GetPlayer(TargetPlayerIndex), 
//             PhotonNetwork.LocalPlayer.ActorNumber, TargetPlayerIndex);
//     }
//
//     [PunRPC]
//     public void ReceivePartyInvite(int inviterID, int targetPlayerID)
//     {
//         string temp = string.Format(
//             "<color=#FFFF00>{0}</color>�Բ��� ��Ƽ�� �ʴ��Ͽ����ϴ�.\n�����Ͻðڽ��ϱ�?", 
//             PhotonHelper.GetPlayerNickName(inviterID));
//
//         Action YES = () =>
//         {
//             Photon.Realtime.Player HOST = PhotonHelper.GetPlayer(inviterID);
//             Photon.Realtime.Player CLIENT = PhotonHelper.GetPlayer(targetPlayerID);
//
//             Party party = BaseManager.Party.GetParty(HOST);
//             if (party == null)
//             {
//                 BaseManager.Party.RequestCreateParty(HOST);
//                 party = BaseManager.Party.GetParty(HOST);
//             }
//
//             BaseManager.Party.RequestJoinParty(CLIENT, party.PartyID);
//         };
//
//         Action NO = () =>
//         {
//             ToastPopUPManager.instance.Initalize("��Ƽ �ʴ븦 �����Ͽ����ϴ�.");
//             photonView.RPC("IgnorePartyInvite", PhotonHelper.GetPlayer(inviterID), targetPlayerID);
//         };
//
//         PopUPManager.instance.Initalize(temp, YES, NO);
//     }
//
//     [PunRPC]
//     public void IgnorePartyInvite(int targetPlayerID)
//     {
//         string temp = string.Format(
//             "<color=#FFFF00>{0}</color>�Բ��� ��Ƽ �ʴ븦 �����Ͽ����ϴ�.",
//             PhotonHelper.GetPlayerNickName(targetPlayerID));
//
//
//         ToastPopUPManager.instance.Initalize(temp);
//     }
//     #endregion
//     #region Trade
//     public static void Trade()
//     {
//         string Toast = string.Format("<color=#FFFF00>{0}</color>�Կ��� �ŷ��� ��û�Ͽ����ϴ�.",
//         PhotonHelper.GetPlayerNickName(TargetPlayerIndex));
//
//         ToastPopUPManager.instance.Initalize(Toast);
//
//         photonView.RPC("ReceiveTradeInvite", PhotonHelper.GetPlayer(TargetPlayerIndex),
//             PhotonNetwork.LocalPlayer.ActorNumber, TargetPlayerIndex);
//     }
//
//     [PunRPC]
//     public void ReceiveTradeInvite(int inviterID, int targetPlayerID)
//     {
//         string temp = string.Format(
//            "<color=#FFFF00>{0}</color>�Բ��� �ŷ��� ��û�Ͽ����ϴ�.\n�����Ͻðڽ��ϱ�?",
//            PhotonHelper.GetPlayerNickName(inviterID));
//
//         BaseManager.Trade.StartTrade(PhotonHelper.GetPlayer(inviterID));
//
//         Action YES = () =>
//         {
//             Photon.Realtime.Player HOST = PhotonHelper.GetPlayer(inviterID);
//             Photon.Realtime.Player CLIENT = PhotonHelper.GetPlayer(targetPlayerID);
//
//             BaseManager.Trade.RequestTrade();
//         };
//
//         Action NO = () =>
//         {
//             ToastPopUPManager.instance.Initalize("��Ƽ �ʴ븦 �����Ͽ����ϴ�.");
//             photonView.RPC("IgnoreTradeInvite", PhotonHelper.GetPlayer(inviterID), targetPlayerID);
//         };
//
//         PopUPManager.instance.Initalize(temp, YES, NO);
//     }
//
//     [PunRPC]
//     public void IgnoreTradeInvite(int targetPlayerID)
//     {
//         string temp = string.Format(
//             "<color=#FFFF00>{0}</color>�Բ��� �ŷ� ��û�� �����Ͽ����ϴ�.",
//             PhotonHelper.GetPlayerNickName(targetPlayerID));
//         ToastPopUPManager.instance.Initalize(temp);
//     }
//     #endregion
//     #region Guild
//     public static async void InviteGuild()
//     {
//         string Toast = string.Format("<color=#FFFF00>{0}</color>�Կ��� ��� ������ ��û�Ͽ����ϴ�.",
//        PhotonHelper.GetPlayerNickName(TargetPlayerIndex));
//
//         ToastPopUPManager.instance.Initalize(Toast);
//         string guildID = await BaseManager.Guild.GetUserGuild();
//
//         photonView.RPC("ReceiveGuildInvite", PhotonHelper.GetPlayer(TargetPlayerIndex),
//             PhotonNetwork.LocalPlayer.ActorNumber, TargetPlayerIndex, guildID);
//     }
//     [PunRPC]
//     public void ReceiveGuildInvite(int inviterID, int targetPlayerID, string guildID)
//     {
//         string temp = string.Format(
//            "<color=#FFFF00>{0}</color>�Բ��� ��� ������ ��û�Ͽ����ϴ�.\n�����Ͻðڽ��ϱ�?",
//            PhotonHelper.GetPlayerNickName(inviterID));
//
//         Action YES = () =>
//         {
//             Photon.Realtime.Player HOST = PhotonHelper.GetPlayer(inviterID);
//             Photon.Realtime.Player CLIENT = PhotonHelper.GetPlayer(targetPlayerID);
//
//             BaseManager.Guild.JoinGuild(guildID);
//         };
//
//         Action NO = () =>
//         {
//             ToastPopUPManager.instance.Initalize("��� ���� ��û�� �����Ͽ����ϴ�.");
//             photonView.RPC("IgnoreGuildInvite", PhotonHelper.GetPlayer(inviterID), targetPlayerID);
//         };
//
//         PopUPManager.instance.Initalize(temp, YES, NO);
//     }
//
//     [PunRPC]
//     public void IgnoreGuildInvite(int targetPlayerID)
//     {
//         string temp = string.Format(
//             "<color=#FFFF00>{0}</color>�Բ��� �ŷ� ��û�� �����Ͽ����ϴ�.",
//             PhotonHelper.GetPlayerNickName(targetPlayerID));
//         ToastPopUPManager.instance.Initalize(temp);
//     }
//     #endregion
// }

using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.U2D;

public enum ActionState
{
    None = 0,
    InviteParty,
    Trade, 
    InviteGuild
}

public class ActionHolder : MonoBehaviourPunCallbacks
{
    public static SpriteAtlas atlas;
    public static Dictionary<ActionState, Action> actions = new Dictionary<ActionState, Action>();

    private static PhotonView pv;

    public static int TargetPlayerID { get; set; }

    public static Sprite GetAtlas(string temp)
    {
        return atlas.GetSprite(temp);
    }
    
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        
        atlas = Resources.Load<SpriteAtlas>("Atlas");
        
        actions[ActionState.InviteParty] = InviteParty;
        actions[ActionState.Trade] = Trade;
        actions[ActionState.InviteGuild] = InviteGuild;
    }

    public static void InviteParty()
    {
        string toast = $"<color=#FFFF00>{PhotonHelper.GetPlayerNickname(TargetPlayerID)}</color>님에게 파티를 초대하였습니다.";
        ToastPopUPManager.instance.Initialize(toast);
        
        pv.RPC("ReceivePartyInvite", PhotonHelper.GetPlayer(TargetPlayerID), PhotonNetwork.LocalPlayer.ActorNumber, TargetPlayerID);
    }

    [PunRPC]
    public void ReceivePartyInvite(int inviterID, int targetPlayerID)
    {
        string temp = $"<color=#FFFF00>{PhotonHelper.GetPlayerNickname(inviterID)}</color>님께서 파티를 초대하였습니다.";
        
        Action yesAction = () =>
        {
            Player host = PhotonHelper.GetPlayer(inviterID);
            Player client = PhotonHelper.GetPlayer(targetPlayerID);

            Party party = BaseManager.Party.GetParty(host);
            if (party == null)
            {
                BaseManager.Party.RequestCreateParty(host);
                party = BaseManager.Party.GetParty(host);
            }

            BaseManager.Party.RequestJoinParty(targetPlayerID, party.PartyID);
        };

        Action noAction = () =>
        {
            ToastPopUPManager.instance.Initialize("파티 초대를 거절하였습니다");
            pv.RPC("IgnorePartyInvite", PhotonHelper.GetPlayer(inviterID), targetPlayerID);
        };
        
        PopUPManager.instance.Initialize(temp, yesAction, noAction);
    }

    [PunRPC]
    public void IgnorePartyInvite(int targetPlayerID)
    {
        string temp = $"<color=#FFFF00>{PhotonHelper.GetPlayerNickname(targetPlayerID)}</color>님께서 파티를 거절하였습니다.";
        
        ToastPopUPManager.instance.Initialize(temp);
    }

    public static void Trade()
    {
        
    }

    public static void InviteGuild()
    {
        
    }
}