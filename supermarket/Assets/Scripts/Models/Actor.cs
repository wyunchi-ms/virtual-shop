using Pathfinding;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor
{
    public ulong uid;
    public string nickname;
    public string avatarUrl;

    private GameObject _avatar;

    public GameObject avatar
    {
        get => _avatar;
        set 
        {
            _avatar = value;
            controller = _avatar.GetComponent<ActorController>();
        }
    }

    public ChatBubble chatBubble;

    public float expireTime;

    private ActorController controller;

    public void Chat(string content)
    {
        if (chatBubble != null)
        {
            chatBubble.ShowChatMessage(content);
        }
    }
}
