using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    public Result_SO currentRes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowDialogue(Result_SO result_SO){
        currentRes = result_SO;
    }
    public enum Speaker{
        NONE, PLAYER, NPC
    }
}
