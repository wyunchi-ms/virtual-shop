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

    public GameObject avatarPrefab;

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
        //AddActor(3595505949, "”√***", "https://p3.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_85253e1ab05045bc9b20036767238524.jpeg");
        //AddActor(3702882800, "èà***", "https://p3.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_d3ac730120f04a829d74a75fa7044957.jpeg");
        //AddActor(77707764054, "”√***", "https://p3.douyinpic.com/aweme/100x100/aweme-avatar/mosaic-legacy_3793_3114521287.jpeg");
        //AddActor(151562171, "??***", "https://p3.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_31b0f7f97b7a45c09d1ca91182bc2271.jpeg");
        //AddActor(3931216667, "∏∂***", "https://p3.douyinpic.com/aweme/100x100/aweme-avatar/mosaic-legacy_3795_3044413937.jpeg");
        //AddActor(3476851497, "«·***", "https://p3.douyinpic.com/aweme/100x100/aweme-avatar/mosaic-legacy_318e30006e4fad5af028b.jpeg");
        //AddActor(2274433369, "¥˙***", "https://p3.douyinpic.com/aweme/100x100/aweme-avatar/mosaic-legacy_30fd90006b65c6f8e6beb.jpeg");
        //AddActor(1167221675, "“«***", "https://p3.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_b244f51c86264219936d023a02c6a9f9.jpeg");
        //AddActor(1872898192, "Œı***", "https://p3.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-avt-0015_16dd3699811ffbcccd5a307a399a60f9.jpeg");
        //AddActor(1263525190, "—æ***", "https://p3.douyinpic.com/aweme/100x100/aweme-avatar/mosaic-legacy_f8c60009732acc7746c0.jpeg");
    }

    // Update is called once per frame
    void Update()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        while (queue.TryDequeue(out Actor actor))
        {
            float x = Random.Range(-9f, 9f);
            float y = Random.Range(-16f, 16f);
            actor.model = Instantiate(avatarPrefab, new Vector3(x, y, 0), Quaternion.identity);
            AIDestinationSetter setter = actor.model.GetComponent<AIDestinationSetter>();
            setter.target = gameObjects[0].GetComponent<Transform>();
            StartCoroutine(DownloadImage(actor, actor.avatar));
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
}
