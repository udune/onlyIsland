using UnityEngine;

public class AuctionData
{
    public string auctionID;
    public string itemName;
    public string sellerID;
    public string sellerNickName;
    public string startingPrice;
    public string currentPrice;
    public string buyOutPrice;
    public string highestBidder;
    public string highestBidderID;
    public string EndTime;
}
public class AuctionManager : MonoBehaviour
{
    // private async void GrantItemToBuyer(string buyerID, string itemName)
    // {
    //     DocumentReference itemRef = BaseManager.Firebase.db.Collection("USERS").Document(buyerID)
    //           .Collection("INVENTORY").Document(itemName);
    //     DocumentSnapshot snapshot = await itemRef.GetSnapshotAsync();
    //
    //     int countValue = 1;
    //     if (snapshot.Exists)
    //     {
    //         countValue = snapshot.GetValue<int>("count");
    //         countValue++;
    //     }
    //     Dictionary<string, object> itemData = new Dictionary<string, object>
    //      {
    //          {"name", itemName },
    //          {"count", countValue},
    //          {"acquiredAt", FieldValue.ServerTimestamp}
    //      };
    //     // MergeAll - ���� �����ʹ� �����ϰ�, ���ο� �ʵ常 ������Ʈ
    //     // MergeFields - (string) <- �Ű������� ���� ���� ����
    //     await itemRef.SetAsync(itemData, SetOptions.MergeAll);
    // }
    // public async Task<bool> CompleteAuction(string auctionID)
    // {
    //     DocumentReference auctionRef = BaseManager.Firebase.db.Collection("AUCTIONS").Document(auctionID);
    //
    //     return await BaseManager.Firebase.db.RunTransactionAsync(transaction =>
    //     {
    //         DocumentSnapshot snapshot = transaction.GetSnapshotAsync(auctionRef).Result;
    //         if (!snapshot.Exists)
    //         {
    //             Debug.LogError("���� ó�� ����");
    //             return Task.FromResult(false);
    //         }
    //         string sellerID = snapshot.GetValue<string>("seller_id");
    //         string sellerNick = snapshot.GetValue<string>("seller_nickname");
    //         string highestBidder = snapshot.GetValue<string>("highest_bidder");
    //         string highestBidderID = snapshot.GetValue<string>("highest_bidder_ID");
    //         int finalPrice = snapshot.GetValue<int>("current_price");
    //         string itemName = snapshot.GetValue<string>("item_name");
    //
    //         transaction.Delete(auctionRef);
    //
    //         if (!string.IsNullOrEmpty(highestBidder))
    //         {
    //             BaseManager.Firebase.m_SendMessage(
    //             highestBidder,
    //              $"{itemName} �������� {finalPrice}�� �����Ͽ����ϴ�!",
    //                 "Admin");
    //
    //             GrantItemToBuyer(highestBidderID, itemName);
    //
    //             BaseManager.Firebase.m_SendMessage(
    //            sellerNick,
    //            $"{itemName} �������� {finalPrice}�� �ǸŵǾ����ϴ�!",
    //            "Admin");
    //         }
    //         else
    //         {
    //             BaseManager.Firebase.m_SendMessage(
    //                       sellerNick,
    //                       $"{itemName} �������� �Ǹŵ��� ���Ͽ����ϴ�.",
    //                       "Admin");
    //
    //             GrantItemToBuyer(sellerID, itemName);
    //         }
    //
    //
    //         return Task.FromResult(true);
    //     });
    // }
    // public async Task<bool> BuyOutItem(string auctionID, string buyerID, string buyerNick)
    // {
    //     DocumentReference auctionRef = BaseManager.Firebase.db.Collection("AUCTIONS").Document(auctionID);
    //
    //     return await BaseManager.Firebase.db.RunTransactionAsync(transaction =>
    //     {
    //         DocumentSnapshot snapshot = transaction.GetSnapshotAsync(auctionRef).Result;
    //         if (!snapshot.Exists)
    //         {
    //             Debug.LogError("���ǿ� Exists�� ������� �ʾҽ��ϴ�.");
    //             return Task.FromResult(false);
    //         }
    //         int buyoutPrice = snapshot.GetValue<int>("buyout_price");
    //         transaction.Update(auctionRef, new Dictionary<string, object>
    //         {
    //             {"current_price",  buyoutPrice},
    //             {"highest_bidder", buyerNick },
    //             {"highest_bidder_ID" ,buyerID}
    //         });
    //
    //         Debug.Log($"���� {auctionID}ǰ���� �ǸŰ� �Ϸ�Ǿ����ϴ�.");
    //         return Task.FromResult(true);
    //     });
    // }
    //
    // public async Task<bool> PlaceBid(string auctionID, string bidderNick, string bidderID,int currentPrice)
    // {
    //     DocumentReference auctionRef = BaseManager.Firebase.db.Collection("AUCTIONS").Document(auctionID);
    //     // GetSnapshotAsync -> �浹 ���ɼ� ����.
    //     // RunTransactionAsync -> ���� ���� ����, ������ ���ռ� ����
    //     return await BaseManager.Firebase.db.RunTransactionAsync(transaction =>
    //     {
    //         DocumentSnapshot snapshot = transaction.GetSnapshotAsync(auctionRef).Result;
    //         if (!snapshot.Exists)
    //         {
    //             Debug.LogError("���� Exists�� �����Ͽ����ϴ�.");
    //             return Task.FromResult(false);
    //         }
    //
    //         int currentprice = snapshot.GetValue<int>("current_price");
    //
    //         if(currentPrice <= currentprice)
    //         {
    //             Debug.Log("�������� �ֽ� ���������� �����ϴ�.");
    //             return Task.FromResult(false);
    //         }
    //
    //         transaction.Update(auctionRef, new Dictionary<string, object>
    //         {
    //             {"current_price", currentPrice },
    //             {"highest_bidder", bidderNick },
    //             {"highest_bidder_ID",  bidderID}
    //         });
    //         Debug.Log($"���������� {auctionID}ǰ���� ������ �Ϸ��Ͽ����ϴ�.");
    //         return Task.FromResult(true);
    //     });
    // }
    // public async Task<List<AuctionData>> LoadAuctionItems()
    // {
    //     List<AuctionData> auctionlist = new List<AuctionData>();
    //     QuerySnapshot snapshot = await BaseManager.Firebase.db.Collection("AUCTIONS").GetSnapshotAsync();
    //
    //     List<string> expiredAuctions = new List<string>();
    //     foreach(DocumentSnapshot doc in snapshot.Documents)
    //     {
    //         DateTime endTime = doc.GetValue<Timestamp>("end_time").ToDateTime().ToLocalTime();
    //         DateTime now = DateTime.Now;
    //
    //         if(endTime <= now)
    //         {
    //             expiredAuctions.Add(doc.Id);
    //             continue;
    //         }
    //
    //         AuctionData auction = new AuctionData
    //         {
    //             auctionID = doc.Id,
    //             itemName = doc.GetValue<string>("item_name"),
    //             sellerID = doc.GetValue<string>("seller_id"),
    //             sellerNickName = doc.GetValue<string>("seller_nickname"),
    //             startingPrice = doc.GetValue<int>("starting_price").ToString(),
    //             currentPrice = doc.GetValue<int>("current_price").ToString(),
    //             buyOutPrice = doc.GetValue<int>("buyout_price").ToString(),
    //             highestBidder = doc.GetValue<string>("highest_bidder"),
    //             highestBidderID = doc.GetValue<string>("highest_bidder_ID"),
    //             EndTime = doc.GetValue<Timestamp>("end_time").ToDateTime().ToLocalTime().
    //             ToString("yyyy-MM-dd HH:mm:ss")
    //         };
    //         auctionlist.Add(auction);
    //     }
    //
    //     if (expiredAuctions.Count > 0)
    //     {
    //         await ProcessExpiredAuctions(expiredAuctions);
    //     }
    //
    //     return auctionlist;
    // }
    //
    // private async Task ProcessExpiredAuctions(List<string> expiredAuctions)
    // {
    //     foreach(string auctionId in expiredAuctions)
    //     {
    //         bool completed = await CompleteAuction(auctionId);
    //     }
    // }
    // public async void CreateAuctionItem(string itemName, string sellerNickName,string sellerID, int buyoutPrice)
    // {
    //     // UTC - ���� �����
    //     // UTC+9
    //     string auctionId = Guid.NewGuid().ToString();
    //     DateTime endTime = DateTime.UtcNow.AddSeconds(10);
    //     int startingPrice = buyoutPrice / 2;
    //     Dictionary<string, object> auctionData = new Dictionary<string, object>
    //     {
    //         {"auction_id",  auctionId},
    //         {"item_name", itemName },
    //         {"seller_id", sellerID },
    //         {"seller_nickname",  sellerNickName},
    //         {"starting_price", startingPrice },
    //         {"current_price", startingPrice },
    //         {"buyout_price",  buyoutPrice},
    //         {"highest_bidder", "" },
    //         {"highest_bidder_ID","" },
    //         {"end_time", Timestamp.FromDateTime(endTime) },
    //         {"created_at", Timestamp.FromDateTime(DateTime.UtcNow) }
    //     };
    //
    //     DocumentReference auctionRef = BaseManager.Firebase.db.Collection("AUCTIONS").Document(auctionId);
    //     await auctionRef.SetAsync(auctionData);
    //
    //     Debug.Log($"����忡 �������� ��ϵǾ����ϴ�: {auctionId}");
    // }
}
