using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class TransformTweenClip : PlayableAsset, ITimelineClipAsset
{
    public ExposedReference<Transform> endLocation;
    public ClipCaps clipCaps => ClipCaps.Blending;

    [NonSerialized]
    public float duration;
    private TransformTweenBehaviour data = new TransformTweenBehaviour();
    
    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var clip = ScriptPlayable<TransformTweenBehaviour>.Create(graph, data);
        var clone = clip.GetBehaviour();
        clone.endLocation = endLocation.Resolve(graph.GetResolver());
        clone.duration = duration;
        return clip;
    }
}
