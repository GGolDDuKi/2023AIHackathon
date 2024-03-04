using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        public NPC npc;
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi("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");

        private List<ChatMessage> messages = new List<ChatMessage>();
        [SerializeField] private string prompt;

        private void Start()
        {
            if(Managers.Instance.System.language == SystemManager.Language.English)
            {
                prompt = "You are an English teacher. From now on, if the sentence I am entering has a grammatical mistake or an incorrect word, please tell me what the mistake is and what is the correct sentence like 'correct sentence is \"Correct Sentence\"'. If it is not wrong, just answer \"Good\". Just answer \"Good\" without any other explanation." +
                         "If I say something threatening to you, just answer \"Don't use slang\". Just answer \"Don't use slang\" without any other explanation.";
            }
            else
            {
                prompt = "당신은 한국인입니다. 이제부터 제가 입력하는 문장이 한국 문법과 맞지 않거나 틀린 단어가 있으면 다음과 같이 알맞은 문장을 알려주세요. '올바른 문장은 \"올바른 문장\"입니다.' 틀린 것이 아니라면 그냥 \"잘했어요\"라고 대답해주세요.다른 설명 없이 그냥 \"잘했어요\"라고 대답하세요." +
                        "내가 입력하는 문장에 비속어가 포함되어 있으면 \"비속어를 사용하지 마세요\"라고 대답하세요. 다른 설명 없이 \"비속어를 사용하지 마세요\"라고 대답하세요.";

            }
 
            button.onClick.AddListener(CheckSentence);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                CheckSentence();
            }
        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.GetChild(0).GetChild(0).GetComponent<Text>().fontSize = 80;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        public void CheckSentence()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            SendReply(newMessage);
            //npc.SendReply(newMessage);
        }

        private async void SendReply(ChatMessage cm)
        {
            var tmpMessage = new ChatMessage()
            {
                Role = "user",
                Content = cm.Content
            };

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
                if(message.Content.Contains("Good") || message.Content.Contains("잘했어요"))
                {
                    Managers.Instance.Money.coin++;
                    CoinValue.SetCoin();
                    npc.SendReply(tmpMessage);
                }
                else if(message.Content.Contains("slang") || message.Content.Contains("비속어"))
                {
                    Managers.Instance.Money.reputation--;
                    ReputationValue.SetReputation();
                }
                else if(message.Content != null || message.Content != "")
                {
                    try
                    {
                        string rightSentence = message.Content.Substring(message.Content.IndexOf('"'), message.Content.LastIndexOf('"') - message.Content.IndexOf('"'));
                        var newMessage = new ChatMessage()
                        {
                            Role = "user",
                            Content = rightSentence
                        };
                        npc.SendReply(newMessage);
                    }
                    catch(Exception ex)
                    {
                        Debug.Log(ex);
                        message.Content = "다시 말씀해주세요.";
                    }
                }
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
}
