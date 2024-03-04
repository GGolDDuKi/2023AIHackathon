using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using OpenAI;

public class NPC : MonoBehaviour
{
    public string npcJob;
    [SerializeField] private InputField inputField;
    [SerializeField] private Button button;
    [SerializeField] private ScrollRect scroll;

    [SerializeField] private RectTransform sent;
    [SerializeField] private RectTransform received;
    public bool sendMessage = false;

    public List<RectTransform> texts = new List<RectTransform>();

    public float height;
    private OpenAIApi openai = new OpenAIApi("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");

    private List<ChatMessage> messages = new List<ChatMessage>();
    private string prompt;

    public void SetPrompt(string name)
    {
        npcJob = name;
        prompt = $"Act as a {npcJob} in a chat room and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";
    }

    private void AppendMessage(ChatMessage message)
    {
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
        texts.Add(item);
        item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
        item.anchoredPosition = new Vector2(0, -height);
        LayoutRebuilder.ForceRebuildLayoutImmediate(item);
        height += item.sizeDelta.y;
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        scroll.verticalNormalizedPosition = 0;
    }

    public async void SendReply(ChatMessage cm)
    {
        AppendMessage(cm);

        if (messages.Count == 0) cm.Content = prompt + "\n" + cm.Content;

        messages.Add(cm);

        button.enabled = false;
        inputField.text = "";
        inputField.enabled = false;

        // Complete the instruction
        var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo-0613",
            Messages = messages
        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();

            messages.Add(message);
            AppendMessage(message);
        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }

        button.enabled = true;
        inputField.enabled = true;
    }
}
