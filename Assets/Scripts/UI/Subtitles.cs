using TMPro;
using UnityEngine;

public class Subtitles : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subs;
    public void Show(string text) 
    {
        subs.text = text;
    }
    public void Stop()
    {
        subs.text = "";
    }
}
