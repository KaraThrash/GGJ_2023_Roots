using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HostPrompt", menuName = "ScriptableObjects/HostPrompt", order = 1)]
public class SO_hostPrompt : ScriptableObject
{
    public List<string> text_input;
    public List<string> text_response;
}
