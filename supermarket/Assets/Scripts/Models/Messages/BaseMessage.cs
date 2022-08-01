using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMessage
{
    #region Common
    public string Platform;
    public ulong MessageId;
    public ulong RoomId;
    public uint TimeStamp;
    public string ActionType;
    #endregion

    #region UserInfo
    public ulong UserId;
    public string Nickname;
    public string Avatar;
    public int Level;
    public int Birthday;
    public int Gender;
    public string City;
    public int FansclubLevel;
    public int UserVipLevel;
    #endregion
}
