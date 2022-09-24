using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

public class DialogBehaviour : PlayableBehaviour
{
#region 字段
    public DialogData dialogData;
    private StorySystem storySystem;
#endregion

#region Override
    public override void OnGraphStart(Playable playable)
    {
        var go = GameObject.FindWithTag("StorySystem");
        if (go == null) 
        {
            Debug.LogError("找不到StorySystem");
            return;
        }

        storySystem = go.GetComponent<StorySystem>();
        if (storySystem == null)
        {
            Debug.LogError("没有挂载StorySystem.cs");
            return;
        }
        
        storySystem.view.SetActive(true);
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (storySystem == null) 
            return;
        
        storySystem.options.SetActive(dialogData.dialogType == DialogType.Option);

        if (dialogData.dialogType == DialogType.Role)
        {
            // 运行模式DoTween
            if (Application.isPlaying)
            {
                storySystem.dialog.text = "";
                storySystem.dialog.DOText(dialogData.dialog, dialogData.dialogDuration);
            }
            // 编辑器模式预览
            else
            {
                storySystem.dialog.text = dialogData.dialog;
            }
        }
        else
        {
            storySystem.option1.gameObject.SetActive(dialogData.option1);
            storySystem.option2.gameObject.SetActive(dialogData.option2);
            storySystem.option3.gameObject.SetActive(dialogData.option3);

            storySystem.text1.text = dialogData.option1Text;
            storySystem.text2.text = dialogData.option2Text;
            storySystem.text3.text = dialogData.option3Text;

            if (Application.isPlaying)
            {
                storySystem.option1.onClick.RemoveAllListeners();
                storySystem.option2.onClick.RemoveAllListeners();
                storySystem.option3.onClick.RemoveAllListeners();

                storySystem.option1.onClick.AddListener(Select1);
                storySystem.option1.onClick.AddListener(Select2);
                storySystem.option1.onClick.AddListener(Select3);
            }
        }
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var current = storySystem.director.time;
        if (current >= dialogData.end)
        {
            if (dialogData.isEnd)
            {
                storySystem.view.SetActive(false);
                return;
            }
            if (Application.isPlaying)
            {
                storySystem.director.time += 0.9f;
            }
        }
            
        if (dialogData.dialogType == DialogType.Role) 
        {
            
        }
        else
        {
            var percentage = (current - dialogData.start) / dialogData.duration;
            storySystem.limit.fillAmount = (float)(1-percentage);
        }
    }
#endregion

#region 选项

    private void Select1()
    {
    }

    private void Select2()
    {
    }

    private void Select3()
    {
    }

    private void DoSelect(string timeline)
    {
        Debug.Log(timeline);
    }
#endregion
}










