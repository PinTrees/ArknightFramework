using UnityEngine;
using System.IO;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
#endif

public static class AnimatorEditorEx 
{
#if UNITY_EDITOR
    public static void AddPublicMotion(this AnimatorControllerLayer layer, string savePath, string stateName, string tag="")
    {
        if (tag == "")
            tag = stateName;

        string animatorName = $"{stateName}";
        string fullPath = Path.Combine(savePath, animatorName + ".asset");

        // 폴더가 존재하지 않는 경우 생성
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
            AssetDatabase.Refresh();
        }

        // 클립이 이미 존재하는지 확인
        AnimationClip motion = AssetDatabase.LoadAssetAtPath<AnimationClip>(fullPath);
        if (motion == null)
        {
            // 존재하지 않으면 새 애니메이션 클립 생성
            motion = new AnimationClip();
            AssetDatabase.CreateAsset(motion, fullPath);
            AssetDatabase.SaveAssets();
        }

        // 에디터에서 생성된 또는 로드된 애니메이션 클립 선택
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = motion;

        // Animator 상태 추가 또는 업데이트
        AnimatorState state = layer.stateMachine.AddState(stateName.ToString());
        state.motion = motion;
        state.tag = tag;
    }

    public static void AddState(this AnimatorControllerLayer layer, AnimationClip motion, string stateName, string tag = "")
    {
        if (tag == "")
            tag = stateName;

        AnimatorState state = layer.stateMachine.AddState(stateName.ToString());
        state.motion = motion;
        state.tag = tag;
    }


    /*public static BlendTree CreateBlendTree(
       this AnimatorController animatorController,
       TpsAnimationSet_8Dir animationClip_8Dir,
       string stateName, string tag = "", string paramXName = "x", string paramYName = "y")
    {
        if (tag == "")
            tag = stateName;

        BlendTree blendTree;

        animatorController.CreateBlendTreeInController(stateName, out blendTree);
        blendTree.blendType = BlendTreeType.FreeformDirectional2D;
        blendTree.blendParameter = paramXName;
        blendTree.blendParameterY = paramYName;

        blendTree.AddChild(animationClip_8Dir.F, new Vector2(0, 1).normalized);
        blendTree.AddChild(animationClip_8Dir.B, new Vector2(0, -1).normalized);
        blendTree.AddChild(animationClip_8Dir.R, new Vector2(1, 0).normalized);
        blendTree.AddChild(animationClip_8Dir.L, new Vector2(-1, 0).normalized);
        blendTree.AddChild(animationClip_8Dir.FR, new Vector2(1, 1).normalized);
        blendTree.AddChild(animationClip_8Dir.FL, new Vector2(-1, 1).normalized);
        blendTree.AddChild(animationClip_8Dir.BL, new Vector2(-1, -1).normalized);
        blendTree.AddChild(animationClip_8Dir.BR, new Vector2(1, -1).normalized);

        animatorController.layers.First().stateMachine.FindState(stateName).tag = tag;

        return blendTree;
    }*/
#endif
}
