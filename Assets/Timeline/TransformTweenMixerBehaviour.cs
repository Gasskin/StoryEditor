using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TransformTweenMixerBehaviour : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var transform = playerData as Transform;
        if (transform == null) 
            return;

        // 一共有几个Clip
        var count = playable.GetInputCount();
        // 每一个Clip的权重
        for (int i = 0; i < count; i++)
        {
            var weight = playable.GetInputWeight(i);
            var inputPlayable = (ScriptPlayable<TransformTweenBehaviour>) playable.GetInput(i);
            var behaviour = inputPlayable.GetBehaviour();
            if (behaviour != null)
                behaviour.weight = weight;
        }
    }
}
