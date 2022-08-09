using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour
{
    public GameObject targetActor;

    private GameObject go;

    private TextMeshProUGUI text;

    private Camera camera;

    public void Init(Actor actor)
    {
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        Canvas UI = GameObject.Find("UI").GetComponent<Canvas>();
        go = new GameObject(actor.nickname, typeof(TextMeshProUGUI));
        text = go.GetComponent<TextMeshProUGUI>();
        text.raycastTarget = false;
        text.color = Color.black;
        go.transform.SetParent(UI.transform);

        text.text = "123";
        targetActor = actor.model;
    }

    void Start()
    {
    }

    private void Follow()
    {
        
    }

    void Update()
    {
        if (text != null)
        {
            text.transform.position = camera.WorldToScreenPoint(targetActor.transform.position);
        }
    }

    void Destroy()
    {
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
