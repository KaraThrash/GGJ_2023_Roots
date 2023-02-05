using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inworld.Runtime;
using Inworld.Sample.UI;
using Inworld.Util;
using System.Linq;
using TMPro;
using Inworld;
using Inworld.Sample;


public enum Prompt
{
    joke,sad,intro,outro,weird
}

public class ShowManager : MonoBehaviour
{
  private SoundManager soundManager;

    public int stage = 0;
    public int score;
    public Prompt current_prompt;
    public Prompt pending_response;
    public List<Prompt> personality;

    public float responseTime = 5;
    public float timer_waitingForPlayer;
    public float timer_bufferNextLine;

    public InworldPlayer player;
    public InworldCharacter host;
    public InworldCharacter guest;
    public TMP_Text text_result;

    public SO_hostPrompt funnyPrompts;
    public SO_hostPrompt sadPrompts;
    public SO_hostPrompt introPrompts;
    public SO_hostPrompt outroPrompts;
    public SO_hostPrompt weirdPrompts;
    public SO_hostPrompt hostQuestionList;

    public string lastline;
    public string host_question;

    void Start()
    {
      soundManager = GetComponent<SoundManager>();
        if (host) { host.Event.AddListener(HostDoneTalking); }
        if (guest) { guest.Event.AddListener(GuestDoneTalking); }
        // current_prompt = personality[(int)Random.Range(0, personality.Count)];
        //  SendText(current_prompt);

    }

    void Update()
    {
        if (timer_bufferNextLine != -1)
        {
            timer_bufferNextLine -= Time.deltaTime;
            if (timer_bufferNextLine <= 0)
            {
                timer_bufferNextLine = -1;
                 if(stage == 0)
                 {
                    current_prompt = personality[(int)Random.Range(0, personality.Count)];
                    SendText(current_prompt);

                 }
                 else if(stage == 1 || stage == 3)
                 {
                    guest.SendText(lastline);

                 }
                 else if(stage == 4 )
                 {
                    host.SendText(lastline);

                 }
                 else
                 {
                    RespondToHost();

                 }
            }
        }

    }



    public void HostDoneTalking(InteractionStatus status, List<HistoryItem> historyItems)
    {


        if (status == InteractionStatus.InteractionCompleted)
        {
            // lastline = historyItems[historyItems.Count - 1].Event.Text;
            lastline = historyItems[0].Event.Text;
            stage ++;
           // if (stage == 0)
           // {
           //      stage = 1;
           //
           //    //  pending_response = (Prompt)Random.Range(0, 5);
           // }
           // else if (stage == 2)
           // {
           //      stage = 3;
           //
           //    //  pending_response = (Prompt)Random.Range(0, 5);
           // }
           // else if (stage == 4)
           // {
           //      stage = 0;
           //
           //    //  pending_response = (Prompt)Random.Range(0, 5);
           // }
           if(stage >=4){stage = 0;}
           timer_bufferNextLine = responseTime;

        }


        Debug.Log(">>HOST DoneTalking  <<" + status.ToString());
    }

    public void GuestDoneTalking(InteractionStatus status, List<HistoryItem> historyItems)
    {

        //  PromptResponse((int)Random.Range(0, 5));
        //  timer_waitingForPlayer = -1;
        //timer_bufferNextLine = 5;

        if (status == InteractionStatus.InteractionCompleted)
        {
            lastline = historyItems[0].Event.Text;

              stage ++;
           // if (stage == 1)
           // {
           //    stage = 2;
           // }
           // else if (stage == 3)
           // {
           //    stage = 4;
           // }

           timer_bufferNextLine = responseTime;

        }


        Debug.Log(">>GUEST DoneTalking  <<" + status.ToString());
    }



    public void PromptResponse(int _response)
    {

      Prompt response = (Prompt)_response;
      soundManager.PlaySound(response);


      //  stage = (stage + 1) % 3;
        if (response == current_prompt)
        {
            score++;
            if (text_result) { text_result.text = "correct: " + score.ToString(); }
          //  AskFor_PositiveResponse();
        }
        else
        {
            score--;
            if (text_result) { text_result.text = "wrong: " + score.ToString(); }
        //    AskFor_NegativeResponse();
        }
        timer_bufferNextLine = 5;
    }


    public void PromptResponse(Prompt _response)
    {


      //  stage = (stage + 1) % 3;
        if (_response == current_prompt)
        {
            score++;
            if (text_result) { text_result.text = "correct: " + score.ToString(); }
          //  AskFor_PositiveResponse();
        }
        else
        {
            score--;
            if (text_result) { text_result.text = "wrong: " + score.ToString(); }
          //  AskFor_NegativeResponse();
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


    // public void SendText(int _prompt)
    // {
    //   current_prompt = (Prompt)_prompt;
    //     if ((Prompt)_prompt == Prompt.joke)
    //     { AskFor_Joke(); }
    //     else if ((Prompt)_prompt == Prompt.sad)
    //     { AskFor_SadStory(); }
    //     else if ((Prompt)_prompt == Prompt.intro)
    //     { AskFor_Intro(); }
    //     else if ((Prompt)_prompt == Prompt.outro)
    //     { AskFor_Outro(); }
    //     else
    //     { AskFor_Weird(); }
    //
    // }

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


    public void RespondToHost()
    {
      // Prompt prompt = current_prompt;
      // SO_hostPrompt responseList = funnyPrompts;
      //
      // if(prompt == Prompt.sad){responseList = sadPrompts;}
      // if(prompt == Prompt.intro){responseList = introPrompts;}
      // if(prompt == Prompt.outro){responseList = outroPrompts;}
      // if(prompt == Prompt.weird){responseList = weirdPrompts;}
      //
      //
      //   string newtext = "say something about the stuff";
      //
      //   if (responseList.text_response != null && responseList.text_response.Count > 1)
      //   {
      //       newtext = responseList.text_input[(int)Random.Range(0, responseList.text_input.Count)];
      //   }



        guest.SendText(lastline);
    }


    public void AskFor_Prompt(SO_hostPrompt _promptList)
    {
        string newtext = "say something about the stuff";

        if (_promptList.text_input != null && _promptList.text_input.Count > 1)
        {
            newtext = _promptList.text_input[(int)Random.Range(0, _promptList.text_input.Count)];
        }

        host_question = hostQuestionList.text_input[Random.Range(0,hostQuestionList.text_input.Count)] + " " + newtext;
        newtext = host_question;
      //  InworldController.Instance.CurrentCharacter.SendText(newtext);
      //  guest.SendText(newtext );
        guest.SendText(newtext);
        player.SendText(newtext);
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
