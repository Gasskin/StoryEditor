using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TransformTweenBehaviour : PlayableBehaviour
{
    public Transform endLocation;
    public float duration;
    public float weight;
    
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var actor = playerData as Transform;
        if (actor == null) 
            return;
        actor.position = Vector3.Lerp(actor.position, endLocation.position, info.deltaTime * weight);
    }
}
