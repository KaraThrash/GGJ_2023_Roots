using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inworld.Runtime;
using Inworld.Sample.UI;
using Inworld.Util;
using System.Linq;
using TMPro;
using Inworld;


public enum Prompt
{
    joke,sad,intro,outro,weird
}

public class ShowManager : MonoBehaviour
{
    public int stage = 0;
    public int score;
    public Prompt current_prompt;
    public Prompt pending_response;
    public List<Prompt> personality;

    public float responseTime = 5;
    public float timer_waitingForPlayer;
    public float timer_bufferNextLine;

    public InworldCharacter host;
    public TMP_Text text_result;

    public SO_hostPrompt funnyPrompts;
    public SO_hostPrompt sadPrompts;
    public SO_hostPrompt introPrompts;
    public SO_hostPrompt outroPrompts;
    public SO_hostPrompt weirdPrompts;

    void Start()
    {
        if (host) { host.Event.AddListener(DoneTalking); }
        
    }

    void Update()
    {
        if (timer_waitingForPlayer != -1)
        { 
            timer_waitingForPlayer -= Time.deltaTime;
            if (timer_waitingForPlayer <= 0)
            {
             //   PromptResponse((int)Random.Range(0, 5));
                //timer_waitingForPlayer = -1;
                //timer_bufferNextLine = 5;
                //PromptResponse(pending_response);
            }
        }
        //if (timer_bufferNextLine != -1)
        //{
        //    if (timer_bufferNextLine > 0)
        //    {
        //        timer_bufferNextLine -= Time.deltaTime;
        //        if (timer_bufferNextLine <= 0)
        //        {
        //            GetNextPrompt();
        //            pending_response = (Prompt)Random.Range(0, 5);
        //            timer_bufferNextLine = -1;
        //            //timer_waitingForPlayer = 5;
        //        }
        //    }
            
        //}
    }

    public void GetNextPrompt()
    {
        if (personality == null || personality.Count < 1)
        {
            current_prompt = (Prompt)Random.Range(0, 5);
            SendText(current_prompt);
            return;
        }
        current_prompt = personality[(int)Random.Range(0, personality.Count)];
        SendText(current_prompt);

    }



    public void DoneTalking(InteractionStatus status, List<HistoryItem> historyItems)
    {

        //  PromptResponse((int)Random.Range(0, 5));
        //  timer_waitingForPlayer = -1;
        //timer_bufferNextLine = 5;

        //if (status == InteractionStatus.InteractionCompleted)
        //{
        //    stage = (stage + 1) % 3;
        //    if (stage == 1)
        //    {
        //        timer_waitingForPlayer = 5;
        //        timer_bufferNextLine = -1;
        //        pending_response = (Prompt)Random.Range(0, 5);
        //    }
            

        //    if (stage == 0)
        //    {
        //        timer_bufferNextLine = 5;
        //        timer_waitingForPlayer = -1;
        //        GetNextPrompt();
        //    }

        //}

        Debug.Log(">> DoneTalking  <<" + status.ToString());
    }

    public void PromptResponse(int _response)
    {
        stage = (stage + 1) % 3;
        if ((Prompt)_response == current_prompt)
        {
            score++;
            if (text_result) { text_result.text = "correct: " + score.ToString(); }
            AskFor_PositiveResponse();
        }
        else
        {
            score--;
            if (text_result) { text_result.text = "wrong: " + score.ToString(); }
            AskFor_NegativeResponse();
        }
        timer_bufferNextLine = 5;
    }
    public void PromptResponse(Prompt _response)
    {
        stage = (stage + 1) % 3;
        if (_response == current_prompt)
        {
            score++;
            if (text_result) { text_result.text = "correct: " + score.ToString(); }
            AskFor_PositiveResponse();
        }
        else
        {
            score--;
            if (text_result) { text_result.text = "wrong: " + score.ToString(); }
            AskFor_NegativeResponse();
        }
        timer_bufferNextLine = 5;
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


    public void SendText(int _prompt)
    {
        if ((Prompt)_prompt == Prompt.joke)
        { AskFor_Joke(); }
        else if ((Prompt)_prompt == Prompt.sad)
        { AskFor_SadStory(); }
        else if ((Prompt)_prompt == Prompt.intro)
        { AskFor_Intro(); }
        else if ((Prompt)_prompt == Prompt.outro)
        { AskFor_Outro(); }
        else
        { AskFor_Weird(); }

    }

    public void SendText(Prompt _prompt)
    {
        if (_prompt == Prompt.joke)
        { AskFor_Joke(); }
        else if (_prompt == Prompt.sad)
        { AskFor_SadStory(); }
        else if (_prompt == Prompt.intro)
        { AskFor_Intro(); }
        else if (_prompt == Prompt.outro)
        { AskFor_Outro(); }
        else
        { AskFor_Weird(); }

    }


    public void AskFor_Prompt(SO_hostPrompt _promptList)
    {
        string newtext = "say something about the stuff";

        if (_promptList.text_input != null && _promptList.text_input.Count > 1)
        {
            newtext = _promptList.text_input[(int)Random.Range(0, _promptList.text_input.Count)];
        }
        


        InworldController.Instance.CurrentCharacter.SendText(newtext);
    }

    public void AskFor_Joke()
    {
        if (funnyPrompts != null)
        {
            AskFor_Prompt(funnyPrompts);
            return;
        }


        string newtext = "Make me Laugh";

       
        
        InworldController.Instance.CurrentCharacter.SendText(newtext);
    }
    public void AskFor_SadStory()
    {

        if (sadPrompts != null)
        {
            AskFor_Prompt(sadPrompts);
            return;
        }
        string newtext = "what makes you cry";
        InworldController.Instance.CurrentCharacter.SendText(newtext);
    }
  
    public void AskFor_Intro()
    {
        if (introPrompts != null)
        {
            AskFor_Prompt(introPrompts);
            return;
        }
        string newtext = "Introduce the next guest";
        InworldController.Instance.CurrentCharacter.SendText(newtext);
    }
    public void AskFor_Outro()
    {
        if (outroPrompts != null)
        {
            AskFor_Prompt(outroPrompts);
            return;
        }
        string newtext = "Play me off the stage";
        InworldController.Instance.CurrentCharacter.SendText(newtext);
    }
    public void AskFor_Weird()
    {
        if (weirdPrompts != null)
        {
            AskFor_Prompt(weirdPrompts);
            return;
        }
        string newtext = "what is peculiar";
        InworldController.Instance.CurrentCharacter.SendText(newtext);
    }
}
