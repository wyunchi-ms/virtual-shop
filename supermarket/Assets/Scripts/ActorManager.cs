using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Collections.Concurrent;
using UnityEngine.Networking;
using Pathfinding;

public class ActorManager : MonoBehaviour
{
    public static ActorManager Instance { get; private set; }

    private readonly Dictionary<ulong, Actor> actors = new();

    private readonly ConcurrentQueue<Actor> queue = new();

    private GameObject avatarContainer;

    public GameObject avatarPrefab;

    private float LastClearTime;

    private float ClearInterval = 10;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LastClearTime = Time.time;
        avatarContainer = GameObject.Find("AvatarContainer");
    }

    // Update is called once per frame
    void Update()
    {
        var counters = GameObject.FindGameObjectsWithTag("Area");
        int counterNum = counters.Length;
        while (queue.TryDequeue(out Actor actor))
        {
            float x = Random.Range(-9f, 9f);
            float y = Random.Range(-16f, 16f);
            actor.model = Instantiate(avatarPrefab, new Vector3(x, y, 0), Quaternion.identity);
            actor.model.transform.SetParent(avatarContainer.transform);
            AIDestinationSetter setter = actor.model.GetComponent<AIDestinationSetter>();
            setter.target = counters[Random.Range(0, counterNum)].GetComponent<Transform>();
            StartCoroutine(DownloadImage(actor, actor.avatar));
            actor.chatBubble = actor.model.GetComponent<ChatBubble>();
            actor.chatBubble.Init(actor);
        }
        float currentTime = Time.time;
        if (currentTime >= LastClearTime + ClearInterval)
        {
            ClearActors();
            LastClearTime = currentTime;
        }
    }

    private IEnumerator DownloadImage(Actor actor, string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            SpriteRenderer spriteRenderer = actor.model.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        }
    }

    public Actor GetActor(ulong uid)
    {
        if (actors.ContainsKey(uid))
        {
            return actors[uid];
        }
        else
        {
            return null;
        }
    }

    public void AddActor(ulong uid, string nickname, string avatar)
    {
        if (!actors.ContainsKey(uid))
        {
            Debug.Log("AddActor123: " + avatar);
            Debug.Log("AddActor456: " + actors.Count);
            Actor actor = new() { uid = uid, nickname = nickname, avatar = avatar };
            actors.Add(actor.uid, actor);
            queue.Enqueue(actor);
        }
    }

    public void RemoveActor(Actor actor)
    {
        ulong uid = actor.uid;
        if (actors.ContainsKey(uid))
        {
            actors.Remove(uid);
            actor.chatBubble.Destroy();
            Destroy(actor.model);
        }
    }

    public void ClearActors()
    {
        float currentTime = Time.time;

        List<ulong> uidList = new List<ulong>(actors.Keys);
        foreach (ulong uid in uidList)
        {
            if (actors.ContainsKey(uid))
            {
                Actor actor = actors[uid];
                if (currentTime >= actor.expireTime)
                {
                    RemoveActor(actor);
                }
            }
        }
    }

    public int CountActor()
    {
        return actors.Count;
    }
}
