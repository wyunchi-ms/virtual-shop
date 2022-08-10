using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour
{
    public GameObject targetActor;

    private GameObject go;

    public GameObject chatBubblePrefab;

    private TextMeshProUGUI text;
    //private Text text;

    private Camera camera;

    public void Init(Actor actor)
    {
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        Canvas UI = GameObject.Find("UI").GetComponent<Canvas>();
        go = Instantiate(chatBubblePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        text = go.GetComponent<TextMeshProUGUI>();
        go.transform.SetParent(UI.transform);

        ShowChatMessage("中文");

        targetActor = actor.model;
    }

    void Start()
    {
    }

    void Update()
    {
        if (text != null)
        {
            text.transform.position = camera.WorldToScreenPoint(targetActor.transform.position) + new Vector3(10, 20, 0);
        }
    }

    public void ShowChatMessage(string message)
    {
        text.text = message;
        float width = text.preferredWidth;
        float height = text.preferredHeight;
        text.rectTransform.sizeDelta = new Vector2(width, height);
    }

    public void Destroy()
    {
        Debug.Log("Destroy");
        if (text != null)
        {
            Destroy(text);
        }
        targetActor = null;
        Destroy(go);
        if (camera != null)
        {
            camera = null;
        }
    }
}
