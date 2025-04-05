using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueStage;
using static DialogueStage.Answer;

public class OptionUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Color selectedColor, nonSelectedColor;

    public int id;
    private Answer answer;

    public void Fill(int id, Answer answer)
    {
        this.id = id;
        text.text = answer.answer;
        this.answer = answer;

        
    //    PlayerButtons.OnNumberPressed += ProcessKeyboard;
    }
    public void F()
    {
        text.color = Color.black;
        GetComponent<Image>().color = selectedColor;
    }

    public void F2()
    {
        text.color = Color.white;
        GetComponent<Image>().color = nonSelectedColor;
    }

    private void OnDestroy()
    {
    //    PlayerButtons.OnNumberPressed -= ProcessKeyboard;
    }

    public void Process()
    {
        if (answer.action == AnswerAction.CLOSE_DIALOGUE)
        {
            DialogueUI.instance.CloseDialogue();
            return;
        }
        else if (answer.action == AnswerAction.GIVE_QUEST)
        {
            Quest q = QuestSystem.instance.GetQuestById(answer.givenQuestOnAnswer);
            PlayerData.instance.AddQuest(q);
            QuestNotification.instance.Show(q.Name);
            DialogueUI.instance.Next(answer.answer, answer.nextStageId);
        }
        else if (answer.action == AnswerAction.TURN_HOSTILE)
        {
           // DialogueWindow.instance.TurnHostile();
        }
        else if (answer.action == AnswerAction.HEAL_TARGET)
        {
           // DialogueWindow.instance.HealTarget();
        }
        else if (answer.action == AnswerAction.GIVE_ITEMS)
        {
           // DialogueWindow.instance.GiveItems(answer.answer, answer.nextStageId, answer.requiredItems);
        }
        else if (answer.action == AnswerAction.RECEIVE_ITEMS)
        {
          // DialogueWindow.instance.ReceiveItems(answer.answer, answer.nextStageId, answer.receivedItems);
        }
        else if (answer.action == AnswerAction.HEAL_PLAYER)
        {
          //  DialogueWindow.instance.Heal(answer.nextStageId);
        }
        else
        {
            DialogueUI.instance.Next(answer.answer, answer.nextStageId);
        }
    }


}
