using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public enum DialogType
{
    Role,
    Option
}

[Serializable]
public class DialogData
{
    public DialogType dialogType;
    public string dialog;
    public float perTextDuration = 0.1f;

    public bool option1;
    public bool option2;
    public bool option3;

    public string option1Text;
    public string option2Text;
    public string option3Text;

    public bool limit;
    public bool needJump;

    public ExposedReference<TimelineAsset> timeline1;
    public ExposedReference<TimelineAsset> timeline2;
    public ExposedReference<TimelineAsset> timeline3;
    
    public bool isEnd;

    public double start;
    public double end;

    public double duration => end - start;
    public float dialogDuration => perTextDuration * dialog.Length;
}

[System.Serializable]
public class DialogAsset : PlayableAsset
{
    public DialogData dialogData;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var template = new DialogBehaviour();
        var playable = ScriptPlayable<DialogBehaviour>.Create(graph, template);
        var behaviour = playable.GetBehaviour();
        behaviour.dialogData = dialogData;
        return playable;
    }

    public override double duration => dialogData.end - dialogData.start;
}
