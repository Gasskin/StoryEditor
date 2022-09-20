using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.855f,0.855f,0.855f)]
[TrackClipType(typeof(TransformTweenClip))]
[TrackBindingType(typeof(Transform))]
public class TransformTweenTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        foreach (var clip in GetClips())
        {
            var clipAsset = clip.asset as TransformTweenClip;
            if (clipAsset != null)
                clipAsset.duration = (float) clip.duration;
        }
        return ScriptPlayable<TransformTweenMixerBehaviour>.Create(graph, inputCount);
    }
}
