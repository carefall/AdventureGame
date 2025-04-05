using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string[] strings;
    void Start()
    {
        text.text = strings[0];
    }
}
