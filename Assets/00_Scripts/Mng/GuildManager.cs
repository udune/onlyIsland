using Firebase.Extensions;
using Firebase.Firestore;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using WebSocketSharp;

public class GuildManager : MonoBehaviour
{

    // public void CreateGuild(string guildName, string playerId)
    // {
    //     if (string.IsNullOrWhiteSpace(guildName))
    //     {
    //         Debug.LogError("������ �Է��ϼ���!");
    //         return;
    //     }
    //     if (guildName.Length < 3 || guildName.Length > 10)
    //     {
    //         Debug.LogError("������ 3�� �̻� 10�� ���Ͽ��� �մϴ�!");
    //         return;
    //     }
    //     if (!System.Text.RegularExpressions.Regex.IsMatch(guildName, "^[a-zA-Z0-9��-�R]+$"))
    //     {
    //         Debug.LogError("���� Ư�� ���ڴ� ����� �� �����ϴ�!");
    //         return;
    //     }
    //
    //     CheckGuildNameExists(guildName, exists =>
    //     {
    //         if (exists)
    //         {
    //             Debug.LogError("�̹� �����ϴ� �г����Դϴ�.");
    //         }
    //         else
    //         {
    //             SaveGuildNameToFirestore(guildName, playerId);
    //         }
    //     });
    // }
    //
    // public async Task JoinGuild(string guildID)
    // {
    //     DocumentReference guildRef = BaseManager.Firebase.db.Collection("GUILDS").Document(guildID);
    //
    //     DocumentSnapshot guildSnapshot = await guildRef.GetSnapshotAsync();
    //     if(!guildSnapshot.Exists)
    //     {
    //         Debug.LogError("��� ��ȸ�� �����Ͽ����ϴ�.");
    //         return;
    //     }
    //
    //     List<string> members = guildSnapshot.GetValue<List<string>>("members");
    //     if(!members.Contains(BaseManager.Firebase.NickName))
    //     {
    //         members.Add(BaseManager.Firebase.NickName);
    //
    //         await guildRef.UpdateAsync(new Dictionary<string, object>
    //         {
    //             {"members", members }
    //         });
    //
    //         UpdateGuildID(guildID);
    //     };
    //     ToastPopUPManager.instance.Initalize("��忡 �����Ͽ����ϴ�.");
    // }
    //
    // public async Task<bool> KickMemberFromGuild(string targetMemberNickName)
    // {
    //     string guildID = await GetUserGuild();
    //
    //     DocumentReference guildRef = BaseManager.Firebase.db.Collection("GUILDS").Document(guildID);
    //     DocumentSnapshot guildSnapshot = await guildRef.GetSnapshotAsync();
    //
    //     string guildMaster = guildSnapshot.GetValue<string>("guildMaster");
    //     if(guildMaster != BaseManager.Firebase.NickName)
    //     {
    //         ToastPopUPManager.instance.Initalize("��� �����͸� �ش� ����� �̿��� �� �ֽ��ϴ�.");
    //         return false;
    //     }
    //     List<string> members = guildSnapshot.GetValue<List<string>>("members");
    //
    //     if (!members.Contains(targetMemberNickName))
    //     {
    //         Debug.Log("�ش� ������ ��忡 �������� �ʽ��ϴ�.");
    //         return false;
    //     }
    //
    //     members.Remove(targetMemberNickName);
    //     await guildRef.UpdateAsync(new Dictionary<string, object>
    //     {
    //         {"members", members }
    //     });
    //
    //     UpdateGuildIDByNickName(targetMemberNickName, "");
    //     return true;
    //     Debug.Log($"���� {targetMemberNickName}�� ��忡�� �߹�Ǿ����ϴ�.");
    // }
    //
    // public async void SaveGuildNameToFirestore(string guildName, string playerId)
    // {
    //     DocumentReference guildRef = BaseManager.Firebase.db.Collection("GUILDS").Document();
    //     Dictionary<string, object> guildData = new Dictionary<string, object>
    //     {
    //         {"guildName", guildName },
    //         {"guildMaster", playerId },
    //         {"members", new List<string> { playerId } },
    //         {"announcement", "" },
    //         {"createdAt", FieldValue.ServerTimestamp }
    //     };
    //
    //     await guildRef.SetAsync(guildData);
    //     
    //     ToastPopUPManager.instance.Initalize("��带 ���������� �����Ͽ����ϴ�.");
    //     UpdateGuildID(guildRef.Id);
    //     GuildUI.instance.ResetGuildUI();
    // }
    //
    // public async void UpdateGuildID(string guildID)
    // {
    //     DocumentReference userRef = BaseManager.Firebase.db.Collection("USERS").Document(BaseManager.Firebase.UserID);
    //
    //     await userRef.UpdateAsync(new Dictionary<string, object>
    //     {
    //         {"GUILDID", guildID }
    //     });
    // }
    //
    // public async void UpdateGuildIDByNickName(string userNickName, string newGuildID)
    // {
    //     CollectionReference usersRef = BaseManager.Firebase.db.Collection("USERS");
    //     QuerySnapshot snapshot = await usersRef.WhereEqualTo("NICKNAME", userNickName).GetSnapshotAsync();
    //
    //     foreach(DocumentSnapshot userDoc in snapshot.Documents)
    //     {
    //         DocumentReference userRef = usersRef.Document(userDoc.Id);
    //
    //         await userRef.UpdateAsync(new Dictionary<string, object>
    //         {
    //             {"GUILDID", newGuildID }
    //         });
    //
    //         Debug.Log($"���� '{userNickName}'�� ��� ID�� '{newGuildID}'�� ������Ʈ�Ǿ����ϴ�.");
    //     }
    //
    // }
    //
    // private void CheckGuildNameExists(string guildName, System.Action<bool> callback)
    // {
    //     BaseManager.Firebase.db.Collection("GUILDS") // Firestore���� "USERS"�÷����� �����մϴ�.
    //         .WhereEqualTo("guildName", guildName) // "NICKNAME"�ʵ��� ���� nickName�� ������ ��ġ�ϴ� ������ �˻� WHERE NICKNAME = "Alice"
    //         .Limit(1) // �˻� ������� �ִ� 1���� ������ ������
    //         .GetSnapshotAsync() // �񵿱������� ����� �����´�.
    //         .ContinueWithOnMainThread(task => // ���� �����忡�� �ļ� �۾��� �����Ѵ�.
    //         {
    //             if (task.IsCompleted && task.Result.Count > 0)
    //             {
    //                 callback.Invoke(true);
    //             }
    //             else
    //             {
    //                 callback.Invoke(false);
    //             }
    //         });
    // }
    //
    // public async void SetAnnouncement(string announcement)
    // {
    //     string guildID = await GetUserGuild();
    //     DocumentReference guildRef = BaseManager.Firebase.db.Collection("GUILDS").Document(guildID);
    //
    //     await guildRef.UpdateAsync(new Dictionary<string, object>
    //     {
    //         {"announcement", announcement }
    //     });
    // }
    //
    // public async Task<Dictionary<string, object>> GetUserGuildInfo()
    // {
    //     string guildID = await GetUserGuild();
    //     Debug.Log(guildID);
    //     if (string.IsNullOrEmpty(guildID))
    //     {
    //         Debug.Log("User is not in any guild");
    //         return null;
    //     }
    //
    //     DocumentReference guildRef = BaseManager.Firebase.db.Collection("GUILDS").Document(guildID);
    //     DocumentSnapshot snapshot = await guildRef.GetSnapshotAsync();
    //
    //     if(snapshot.Exists)
    //     {
    //         Dictionary<string, object> guildData = snapshot.ToDictionary();
    //         return guildData;
    //     }
    //
    //     Debug.Log("Guild not Found");
    //     return null;
    // }
    //
    // public async Task<string> GetUserGuild()
    // {
    //     DocumentReference userRef = BaseManager.Firebase.db.Collection("USERS").Document(BaseManager.Firebase.UserID);
    //     DocumentSnapshot snapshot = await userRef.GetSnapshotAsync();
    //
    //     if (snapshot.Exists && snapshot.ContainsField("GUILDID"))
    //     {
    //         string guildID = snapshot.GetValue<string>("GUILDID");
    //     
    //         if(!string.IsNullOrEmpty(guildID))
    //         {
    //             Debug.Log($"�÷��̾ ��忡 ���ԵǾ��ֽ��ϴ� : {guildID}");
    //             return guildID;
    //         }
    //     }
    //
    //     Debug.Log("�÷��̾ ��忡 ���ԵǾ����� �ʽ��ϴ�.");
    //     return "";
    // }
    //
    // public async Task LeaveGuild()
    // {
    //     string userNickName = BaseManager.Firebase.NickName;
    //     string guildID = await GetUserGuild();
    //
    //     if(string.IsNullOrEmpty(guildID))
    //     {
    //         Debug.Log("������ ������ ��尡 �����ϴ�.");
    //         return;
    //     }
    //
    //     DocumentReference guildRef = BaseManager.Firebase.db.Collection("GUILDS").Document(guildID);
    //     DocumentSnapshot guildSnapshot = await guildRef.GetSnapshotAsync();
    //
    //     if(!guildSnapshot.Exists)
    //     {
    //         Debug.Log("�ش� ��尡 �������� �ʽ��ϴ�.");
    //         return;
    //     }
    //
    //     List<string> members = guildSnapshot.GetValue<List<string>>("members");
    //
    //     if(!members.Contains(userNickName))
    //     {
    //         Debug.Log("������ �ش� ����� ����� �ƴմϴ�.");
    //         return;
    //     }
    //
    //     members.Remove(userNickName);
    //
    //     if(members.Count == 0)
    //     {
    //         await guildRef.DeleteAsync();
    //         Debug.Log("��尡 �����Ǿ����ϴ�.");
    //     }
    //     else
    //     {
    //         await guildRef.UpdateAsync(new Dictionary<string, object> 
    //         {
    //             {"members", members }
    //         });
    //         Debug.Log("������ ��带 Ż���߽��ϴ�.");
    //     }
    //
    //     UpdateGuildID("");
    // }
    //
    // public async Task DisbandGuild()
    // {
    //     string guildID = await GetUserGuild();
    //
    //     if(string.IsNullOrEmpty(guildID))
    //     {
    //         Debug.Log("������ ���� ��尡 �����ϴ�.");
    //         return;
    //     }
    //
    //     DocumentReference guildRef = BaseManager.Firebase.db.Collection("GUILDS").Document(guildID);
    //     DocumentSnapshot guildSnapshot = await guildRef.GetSnapshotAsync();
    //
    //     if(!guildSnapshot.Exists)
    //     {
    //         Debug.Log("��� ������ ã�� �� �����ϴ�.");
    //         return;
    //     }
    //
    //     string guildMaster = guildSnapshot.GetValue<string>("guildMaster");
    //     if(guildMaster != BaseManager.Firebase.NickName)
    //     {
    //         Debug.Log("��� �����͸� ��带 ��ü�� �� �ֽ��ϴ�.");
    //         return;
    //     }
    //
    //     List<string> members = guildSnapshot.GetValue<List<string>>("members");
    //
    //     foreach(string memberId in members)
    //     {
    //         UpdateGuildIDByNickName(memberId, "");
    //     }
    //
    //     await guildRef.DeleteAsync();
    // }
    //
    // public async Task<List<Dictionary<string, object>>> GetAllGuilds()
    // {
    //     CollectionReference guildsRef = BaseManager.Firebase.db.Collection("GUILDS");
    //     QuerySnapshot snapshot = await guildsRef.GetSnapshotAsync();
    //
    //     var guildList = new List<Dictionary<string, object>>();
    //
    //     foreach(var doc in snapshot.Documents)
    //     {
    //         Dictionary<string, object> guildData = doc.ToDictionary();
    //         guildData["guildID"] = doc.Id;
    //         guildList.Add(guildData);
    //     }
    //
    //     return guildList;
    // }
    //
    // public async Task<string> GetGuildName(string guildID)
    // {
    //     DocumentReference guildRef = BaseManager.Firebase.db.Collection("GUILDS").Document(guildID);
    //     DocumentSnapshot snapshot = await guildRef.GetSnapshotAsync();
    //
    //     if(snapshot.Exists && snapshot.ContainsField("guildName"))
    //     {
    //         return snapshot.GetValue<string>("guildName");
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"��� ID {guildID}���� �������� �ʽ��ϴ�.");
    //         return null;
    //     }
    // }
}
