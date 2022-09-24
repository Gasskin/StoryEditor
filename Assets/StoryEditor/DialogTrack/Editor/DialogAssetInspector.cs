using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

[CustomEditor(typeof(DialogAsset))]
public class DialogAssetInspector : UnityEditor.Editor
{
    private SerializedProperty dialogType;
    private SerializedProperty dialog;
    private SerializedProperty option1;
    private SerializedProperty option2;
    private SerializedProperty option3;
    private SerializedProperty option1Text;
    private SerializedProperty option2Text;
    private SerializedProperty option3Text;
    private SerializedProperty isEnd;
    private SerializedProperty needJump;
    private SerializedProperty timeline1;
    private SerializedProperty timeline2;
    private SerializedProperty timeline3;
    private SerializedProperty start;
    private SerializedProperty end;
    private SerializedProperty limit;

    private SerializedProperty dialogData;

    private void OnEnable()
    {
        dialogData = serializedObject.FindProperty(nameof(DialogAsset.dialogData));

        dialogType = dialogData.FindPropertyRelative(nameof(DialogData.dialogType));
        dialog = dialogData.FindPropertyRelative(nameof(DialogData.dialog));

        option1 = dialogData.FindPropertyRelative(nameof(DialogData.option1));
        option2 = dialogData.FindPropertyRelative(nameof(DialogData.option2));
        option3 = dialogData.FindPropertyRelative(nameof(DialogData.option3));

        option1Text = dialogData.FindPropertyRelative(nameof(DialogData.option1Text));
        option2Text = dialogData.FindPropertyRelative(nameof(DialogData.option2Text));
        option3Text = dialogData.FindPropertyRelative(nameof(DialogData.option3Text));

        isEnd = dialogData.FindPropertyRelative(nameof(DialogData.isEnd));

        needJump = dialogData.FindPropertyRelative(nameof(DialogData.needJump));

        timeline1 = dialogData.FindPropertyRelative(nameof(DialogData.timeline1));
        timeline2 = dialogData.FindPropertyRelative(nameof(DialogData.timeline2));
        timeline3 = dialogData.FindPropertyRelative(nameof(DialogData.timeline3));

        start = dialogData.FindPropertyRelative(nameof(DialogData.start));
        end = dialogData.FindPropertyRelative(nameof(DialogData.end));

        limit = dialogData.FindPropertyRelative(nameof(DialogData.limit));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        SetWidth(80);

        GUI.enabled = false;
        EditorGUILayout.DoubleField("开始时间", start.doubleValue);
        EditorGUILayout.DoubleField("结束时间", end.doubleValue);
        GUI.enabled = true;
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        dialogType.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup("对话类型", (DialogType) dialogType.enumValueIndex));
        if (dialogType.enumValueIndex == (int) DialogType.Option)
        {
            needJump.boolValue = EditorGUILayout.Toggle("跳转", needJump.boolValue);
            limit.boolValue = EditorGUILayout.Toggle("限时", limit.boolValue);
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (dialogType.enumValueIndex == (int) DialogType.Role)
            RoleDialog();
        else
            PlayerOption();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        isEnd.boolValue = EditorGUILayout.Toggle("是否结束", isEnd.boolValue);
        serializedObject.ApplyModifiedProperties();
    }

    private void RoleDialog()
    {
        dialog.stringValue = EditorGUILayout.TextField("对话", dialog.stringValue);
    }

    private void PlayerOption()
    {
        option1.boolValue = EditorGUILayout.Toggle("选项1", option1.boolValue);
        if (option1.boolValue)
        {
            option1Text.stringValue = EditorGUILayout.TextField("内容", option1Text.stringValue);
            if (!isEnd.boolValue && needJump.boolValue)
            {
                SetWidth(65);
                EditorGUILayout.PropertyField(timeline1, new GUIContent("Timeline"));
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        SetWidth(80);
        option2.boolValue = EditorGUILayout.Toggle("选项2", option2.boolValue);
        if (option2.boolValue)
        {
            option2Text.stringValue = EditorGUILayout.TextField("内容", option2Text.stringValue);
            if (!isEnd.boolValue && needJump.boolValue)
            {
                SetWidth(65);
                EditorGUILayout.PropertyField(timeline2, new GUIContent("Timeline"));
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        SetWidth(80);
        option3.boolValue = EditorGUILayout.Toggle("选项3", option3.boolValue);
        if (option3.boolValue)
        {
            option3Text.stringValue = EditorGUILayout.TextField("内容", option3Text.stringValue);
            if (!isEnd.boolValue && needJump.boolValue)
            {
                SetWidth(65);
                EditorGUILayout.PropertyField(timeline2, new GUIContent("Timeline"));
            }
        }
    }

    private void SetWidth(float width)
    {
        EditorGUIUtility.labelWidth = width;
    }
}