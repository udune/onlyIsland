using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// public class PartyManager : MonoBehaviourPunCallbacks
// {
//     private Dictionary<int, Party> activeParties = new Dictionary<int, Party>();
//
//     public bool HasParty(Player player)
//     {
//         foreach (var party in activeParties.Values)
//         {
//             if (party.IsMember(player))
//                 return true;
//         }
//         return false;
//     }
//
//     public Party GetParty(Player player)
//     {
//         foreach (var party in activeParties.Values)
//         {
//             if (party.IsMember(player))
//                 return party;
//         }
//         return null;
//     }
//     [PunRPC]
//     public void RPC_CreateParty(int partyID, int leaderID)
//     {
//         if (activeParties.ContainsKey(partyID)) return;
//
//         //Player leader = PhotonHelper.GetPlayer(leaderID);
//         //if (leader == null) return;
//
//         //Party newParty = new Party(partyID, leader);
//         //activeParties.Add(partyID, newParty);
//         PartyUI.instance.Initalize();
//     }
//     public void RequestCreateParty(Player leader)
//     {
//         if (HasParty(leader)) return;
//
//         int partyID = activeParties.Count + 1;
//         photonView.RPC("RPC_CreateParty", RpcTarget.AllBuffered, partyID, leader.ActorNumber);
//     }
//
//     [PunRPC]
//     public void RPC_JoinParty(int playerID, int partyID)
//     {
//         if (!activeParties.ContainsKey(partyID)) return;
//
//         //Player player = PhotonHelper.GetPlayer(playerID);
//         //if (player == null || HasParty(player)) return;
//
//         //activeParties[partyID].AddMember(player);
//         PartyUI.instance.Initalize();
//     }
//
//     public void RequestJoinParty(Player player, int partyID)
//     {
//         photonView.RPC("RPC_JoinParty", RpcTarget.AllBuffered, player.ActorNumber, partyID);
//     }
//
//     [PunRPC]
//     public void RPC_LeaveParty(Player player)
//     {
//         if (player == null) return;
//
//         Party party = GetParty(player);
//         if(party != null)
//         {
//             party.RemoveMember(player);
//             PartyUI.instance.Initalize();
//
//             if(party.Members.Count == 0)
//             {
//                 activeParties.Remove(party.PartyID);
//             }
//             else
//             {
//                 for(int i = 0; i < party.Members.Count; i++)
//                 {
//                     photonView.RPC("RPC_NotifyPartyMemberLeft", party.Members[i], player);
//                 }
//             }
//         }
//     }
//
//     [PunRPC]
//     public void RPC_NotifyPartyMemberLeft(Player player)
//     {
//         string temp = string.Format("{0}�Բ��� ��Ƽ�� Ż���Ͽ����ϴ�.", player.NickName);
//         ToastPopUPManager.instance.Initalize(temp);
//     }
//
//     public void RequestLeaveParty(Player player)
//     {
//         photonView.RPC("RPC_LeaveParty", RpcTarget.AllBuffered, player);
//     }
//
//     public override void OnPlayerLeftRoom(Player otherPlayer)
//     {
//         if(HasParty(otherPlayer))
//         {
//             photonView.RPC("RPC_LeaveParty", RpcTarget.AllBuffered, otherPlayer);
//         }
//     }
//
// }

public class PartyManager : MonoBehaviourPunCallbacks
{
    private Dictionary<int, Party> activeParties = new Dictionary<int, Party>();

    public bool HasParty(Player player)
    {
        foreach (var party in activeParties.Values)
        {
            if (party.IsMember(player))
            {
                return true;
            }
        }

        return false;
    }

    public Party GetParty(Player player)
    {
        foreach (var party in activeParties.Values)
        {
            if (party.IsMember(player))
            {
                return party;
            }
        }

        return null;
    }

    [PunRPC]
    public void RPC_CreateParty(int partyID, int leaderID)
    {
        if (activeParties.ContainsKey(partyID))
        {
            return;
        }

        Player leader = PhotonHelper.GetPlayer(leaderID);
        if (leader == null)
        {
            return;
        }
        
        Party newParty = new Party(partyID, leader);
        activeParties.Add(partyID, newParty);
        
        PartyUI.instance.Initialize();
    }
    
    public void RequestCreateParty(Player leader)
    {
        if (HasParty(leader))
        {
            return;
        }
        
        int partyID = activeParties.Count + 1;
        photonView.RPC("RPC_CreateParty", RpcTarget.AllBuffered, partyID, leader.ActorNumber);
    }

    // public Party CreateParty(Player leader)
    // {
    //     if (HasParty(leader))
    //     {
    //         return null;
    //     }
    //
    //     int partyID = activeParties.Count + 1;
    //     Party newParty = new Party(partyID, leader);
    //     activeParties.Add(partyID, newParty);
    //     
    //     PartyUI.instance.Initialize();
    //
    //     return newParty;
    // }

    [PunRPC]
    public void RPC_JoinParty(int playerID, int partyID)
    {
        if (!activeParties.ContainsKey(partyID))
        {
            return;
        }
        
        Player player = PhotonHelper.GetPlayer(playerID);
        if (player == null || HasParty(player))
        {
            return;
        }
        
        activeParties[partyID].AddMember(player);
        PartyUI.instance.Initialize();
    }

    public void RequestJoinParty(int playerID, int partyID)
    {
        photonView.RPC("RPC_JoinParty", RpcTarget.AllBuffered, playerID, partyID);
    }
    
    // public bool JoinParty(Player player, int partyID)
    // {
    //     if (HasParty(player) || !activeParties.ContainsKey(partyID))
    //     {
    //         return false;
    //     }
    //
    //     PartyUI.instance.Initialize();
    //     return activeParties[partyID].AddMember(player);
    // }

    [PunRPC]
    public void RPC_LeaveParty(Player player)
    {
        if (player == null)
        {
            return;
        }
        
        Party party = GetParty(player);
        if (party != null)
        {
            party.RemoveMember(player);
            PartyUI.instance.Initialize();

            if (party.Members.Count == 0)
            {
                activeParties.Remove(party.PartyID);
            }
            else
            {
                for (int i = 0; i < party.Members.Count; i++)
                {
                    photonView.RPC("RPC_NotifyPartyMemberLeft", party.Members[i], player);
                }
            }
        }
    }

    [PunRPC]
    public void RPC_NotifyPartyMemberLeft(Player player)
    {
        string temp = $"{player.NickName}님께서 파티를 탈퇴하였습니다.";
        ToastPopUPManager.instance.Initialize(temp);
    }
    
    public void RequestLeaveParty(Player player)
    {
        photonView.RPC("RPC_LeaveParty", RpcTarget.AllBuffered, player);
    }
    
    // public void LeaveParty(Player player)
    // {
    //     Party party = GetParty(player);
    //     if (party != null)
    //     {
    //         party.RemoveMember(player);
    //         PartyUI.instance.Initialize();
    //         if (party.Members.Count.Equals(0))
    //         {
    //             activeParties.Remove(party.PartyID);
    //         }
    //     }
    // }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        if (HasParty(otherPlayer))
        {
            photonView.RPC("RPC_LeaveParty", RpcTarget.AllBuffered, otherPlayer);
        }
    }
}