using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inworld.Runtime;
using Inworld.Sample.UI;
using Inworld.Util;
using System.Linq;
using TMPro;
using Inworld;


public enum prompt
{
    joke,sad,intro,outro
}

public class ShowManager : MonoBehaviour
{

    public prompt current_prompt;

    public float timer_waitingForPlayer;

    public InworldCharacter host;


    void Start()
    {
        if (host) { host.Event.AddListener(DoneTalking); }
        
    }

    void Update()
    {
        if (timer_waitingForPlayer != -1)
        { timer_waitingForPlayer -= Time.deltaTime; }
    }


    public void DoneTalking(InteractionStatus status, List<HistoryItem> historyItems)
    {
        Debug.Log(status.ToString());
    }

    public void PromptResponse(int _response)
    {
        if ((prompt)_response == current_prompt)
        {
            AskFor_PositiveResponse();
        }
        else
        {
            AskFor_NegativeResponse();
        }

    }


    public void AskFor_NegativeResponse()
    {
        string newtext = "Negative Response";
        InworldController.Instance.CurrentCharacter.SendText(newtext);
    }

    public void AskFor_PositiveResponse()
    {
        string newtext = "Positive Response";
        InworldController.Instance.CurrentCharacter.SendText(newtext);
    }

    public void SendText()
    {
        //  if (string.IsNullOrEmpty(m_InputField.text))
        //    return;
        string newtext = "tell me a joke";
        InworldController.Instance.CurrentCharacter.SendText(newtext);
        //  m_InputField.text = null;
    }



    public void AskFor_Joke()
    {
        string newtext = "Make me Laugh";
        InworldController.Instance.CurrentCharacter.SendText(newtext);
    }
    public void AskFor_SadStory()
    {
        string newtext = "what makes you cry";
        InworldController.Instance.CurrentCharacter.SendText(newtext);
    }


}
