using TMPro;
using UnityEngine;

public class QuestNotification : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI questtext;
    public static QuestNotification instance;
    private void Awake()
    {
        instance = this;
    }
    public void Show(string text)
    {
        questtext.text = text;
        anim.Play("QuestNotification");
    }
}
