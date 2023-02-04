using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inworld.Runtime;
using Inworld.Sample.UI;
using Inworld.Util;

using System.Linq;
using TMPro;
using Inworld;

public class Player : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
