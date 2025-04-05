using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Intro : MonoBehaviour
{
    [SerializeField] AudioSource Lichaudio;
    [SerializeField] TextMeshProUGUI LichSub;
    [SerializeField] AudioClip[] Clips;
    [SerializeField] string[] Subs;
    [SerializeField] Animator UI;
    [SerializeField] PlayerInput input;
    private void Start()
    {
        UI.Play("unFade");
        Invoke(nameof(Talk), 2);
    }
    private void Talk()
    {
        Lichaudio.clip = Clips[0];
        LichSub.text = Subs[0];
        Lichaudio.Play();
        int i = 1;
        while (i < Clips.Length)
        {
            if (Lichaudio.isPlaying) continue;
            Lichaudio.clip = Clips[i];
            LichSub.text = Subs[i];
            i++;
        }
        while (true)
        {
            if (Lichaudio.isPlaying) continue;
            break;
        }
        input.enabled = true;
        LichSub.text = "";
    }
}
