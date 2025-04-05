using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueStage;
using static DialogueStage.Answer;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] Transform content, options;
    [SerializeField] ReplicaUI replicaPrefab;
    [SerializeField] OptionUI optionPrefab;
    [SerializeField] Image target, player;
    [SerializeField] TextMeshProUGUI targetName, playerName;
    [SerializeField] Sprite system;
    [SerializeField] QuestSystem questSystem;
    [Multiline]
    [SerializeField] string questNotification, healNotification;
    private ScrollRect scrollRect;
    [HideInInspector]
    public Dialogue dialogue;
    private Entity entity;

    public static DialogueWindow instance;

    public static Action<string, Dialogue, int> OnDialogueStageChange;

    private void Start()
    {
        instance = this;
        scrollRect = GetComponent<ScrollRect>();
        gameObject.SetActive(false);
    }

    public void StartDialogue(Entity entity, Dialogue dialogue)
    {
        gameObject.SetActive(true);
        this.dialogue = dialogue;
        this.entity = entity;
        targetName.text = entity.displayName;
        target.sprite = entity.sprite;
        if (dialogue.GetStage(0).text == null || dialogue.GetStage(0).text.Length == 0)
        {
            for (int i = 0; i < dialogue.GetStage(0).answers.Length; i++)
            {
                Answer answer = dialogue.GetStage(0).answers[i];
                if (answer.FitsRequirements(PlayerData.instance))
                {
                    OptionUI option = Instantiate(optionPrefab, options);
                    option.Fill(i + 1, answer);
                }
            }
            return;
        }
        ReplicaUI replica = Instantiate(replicaPrefab, content);
        replica.Fill(entity.sprite, entity.displayName, dialogue.GetStage(0).text);
        for (int i = 0; i < dialogue.GetStage(0).answers.Length; i++)
        {
            Answer answer = dialogue.GetStage(0).answers[i];
            if (answer.FitsRequirements(PlayerData.instance))
            {
                OptionUI option = Instantiate(optionPrefab, options);
                option.Fill(i + 1, answer);
            }
        }
        OnDialogueStageChange.Invoke(entity.uniqueName, dialogue, 0);
    }

    public void ShowQuestNotification(string text, int id, int questId)
    {
        if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
        Quest quest = questSystem.GetQuestById(questId);
        PlayerData.instance.AddQuest(quest);
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", string.Format(questNotification, quest.Name, quest.Description));
        Next(id);
    }

    public void ReceiveItems(string text, int id, Item[] items)
    {
        if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
      //  InventoryWindow.instance.AddItems(items);
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", ""/*message format*/);
        Next(id);
    }

    public void GiveItems(string text, int id, Item[] items)
    {
        if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
      //  InventoryWindow.instance.RemoveItems(items);
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", ""/*message format*/);
        Next(id);
    }

    public void HealTarget()
    {
     //   InventoryWindow.instance.RemoveWeakestMedkit();
        entity.Heal();
        CloseDialogue();
    }

    public void TurnHostile()
    {
        entity.TurnHostile(PlayerData.instance);
        CloseDialogue();
    }

    public void Heal(int id)
    {
    //    PlayerData.instance.Heal();
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", healNotification);
        Next(id);
    }

    public void Next(int id)
    {
        OnDialogueStageChange.Invoke(entity.uniqueName, dialogue, id);
        for (int i = options.childCount - 1; i >= 0; i--)
        {
            Destroy(options.GetChild(i).gameObject);
        }
        if (id < 0 || dialogue.GetStage(id) == null)
        {
            CloseDialogue();
            return;
        }
        if (dialogue.GetStage(id).text != null && dialogue.GetStage(id).text.Length != 0)
        {
            ReplicaUI replica = Instantiate(replicaPrefab, content);
            replica.Fill(target.sprite, targetName.text, dialogue.GetStage(id).text);
        }
        for (int i = 0; i < dialogue.GetStage(id).answers.Length; i++)
        {
            Answer answer = dialogue.GetStage(id).answers[i];
            OptionUI option = Instantiate(optionPrefab, options);
            option.Fill(i + 1, answer);
        }
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    public void Next(string text, int id)
    {
        if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
        Next(id);
    }

    public void CloseDialogue()
    {
        PlayerData.instance.viewingDialogue = false;
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        for (int i = options.childCount - 1; i >= 0; i--)
        {
            Destroy(options.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
    }
}
