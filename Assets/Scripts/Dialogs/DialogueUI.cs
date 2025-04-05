using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI instance;
    [SerializeField] TextMeshProUGUI entityName;
    [SerializeField] Subtitles subtitles;
    private Dialogue dialogue;
    private Entity entity;
    private DialogueStage stage;
    [SerializeField] OptionUI optionPrefab;
    private List<OptionUI> options = new(); 

    void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    public void StartDialogue (Entity e, Dialogue d)
    {
        PlayerData.instance.viewingDialogue = true;
        gameObject.SetActive(true);
        entity = e;
        dialogue = d;
        stage = d.GetStage(0);
        entityName.text = e.displayName;
        subtitles.Show($"[{e.displayName}] {stage.text}");
        entity.voice.clip = stage.clip;
        entity.voice.Play();
        for (int i = 0; i < stage.answers.Length; i++)
        {
            options.Add(Instantiate(optionPrefab, transform));
            options[i].Fill(i, stage.answers[i]);
        }

    }

    internal void Next(string answer, int nextStageId)
    {
        for (int i = options.Count - 1; i>=0; i--) 
        {
            Destroy(options[i].gameObject);
            options.RemoveAt(i);
        }
        stage = dialogue.GetStage(nextStageId);
        subtitles.Show($"[{entity.displayName}] {stage.text}");
        entity.voice.clip = stage.clip;
        entity.voice.Play();
        for (int i = 0; i < stage.answers.Length; i++)
        {
            options.Add(Instantiate(optionPrefab, transform));
            options[i].Fill(i, stage.answers[i]);
        }
    }

    internal void CloseDialogue()
    {
        for (int i = options.Count - 1; i >= 0; i--)
        {
            Destroy(options[i].gameObject);
            options.RemoveAt(i);
        }
        PlayerData.instance.viewingDialogue = false;
        subtitles.Stop();
        gameObject.SetActive(false);
    }
}
