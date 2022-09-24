using UnityEditor.Timeline;
using UnityEngine.Timeline;

[CustomTimelineEditor(typeof(DialogAsset))]
public class DialogAssetEditor : ClipEditor
{
    public override void OnClipChanged(TimelineClip clip)
    {
        base.OnClipChanged(clip);

        var dialog = clip.asset as DialogAsset;
        if (dialog == null) 
            return;
        dialog.dialogData.start = clip.start;
        dialog.dialogData.end = clip.end - 1;
    }
}
