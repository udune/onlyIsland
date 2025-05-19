// using Photon.Pun;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class PartyUI : MonoBehaviour
// {
//     public static PartyUI instance = null;
//
//     private void Awake()
//     {
//         if(instance == null)
//         {
//             instance = this;
//         }
//     }
//
//     public GameObject MemberUI;
//     public Transform Content;
//     List<GameObject> Gorvage = new List<GameObject>();
//
//     public void Initalize()
//     {
//         if(Gorvage.Count > 0)
//         {
//             for (int i = 0; i < Gorvage.Count; i++) Destroy(Gorvage[i]);
//             Gorvage.Clear();
//         }
//
//         //Party party = BaseManager.Party.GetParty(PhotonNetwork.LocalPlayer);
//         // if (party == null) return;
//         //
//         // for (int i = 0; i < party.Members.Count; i++)
//         // {
//         //     var go = Instantiate(MemberUI, Content);
//         //     go.SetActive(true);
//         //
//         //     go.transform.Find("LeaderMark").gameObject.SetActive(party.Leader == party.Members[i]);
//         //     go.transform.GetComponentInChildren<Text>().text = party.Members[i].NickName;
//         //
//         //     GameObject leaveObject = go.transform.Find("LeaveParty").gameObject;
//         //     leaveObject.SetActive(
//         //         party.Members[i].NickName == PhotonNetwork.LocalPlayer.NickName);
//         //     leaveObject.GetComponent<Button>().onClick.AddListener(() => LeavePartyButton());
//         //
//         //     Gorvage.Add(go);
//         // }
//     }
//
//     private void LeavePartyButton()
//     {
//         //BaseManager.Party.RequestLeaveParty(PhotonNetwork.LocalPlayer);
//     }
// }

using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PartyUI : MonoBehaviour
{
    public static PartyUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject memberUI;
    public Transform content;
    private List<GameObject> garbage = new List<GameObject>();

    public void Initialize()
    {
        if (garbage.Count > 0)
        {
            for (int i = 0; i < garbage.Count; i++)
            {
                Destroy(garbage[i]);
            }
            garbage.Clear();
        }
        
        Party party = BaseManager.Party.GetParty(PhotonNetwork.LocalPlayer);
        if (party == null)
        {
            return;
        }

        for (int i = 0; i < party.Members.Count; i++)
        {
            GameObject go = Instantiate(memberUI, content);
            go.SetActive(true);
            go.transform.Find("Leader").gameObject.SetActive(party.Leader.Equals(party.Members[i]));
            go.transform.GetComponentInChildren<TMP_Text>().text = party.Members[i].NickName;
            
            GameObject leaveObject = go.transform.Find("LeaveParty").gameObject;
            leaveObject.SetActive(party.Members[i].NickName.Equals(PhotonNetwork.LocalPlayer.NickName));
            leaveObject.GetComponent<Button>().onClick.AddListener(LeavePartyButton);
            
            garbage.Add(go);
        }
    }

    private void LeavePartyButton()
    {
        BaseManager.Party.RequestLeaveParty(PhotonNetwork.LocalPlayer);
    }
}