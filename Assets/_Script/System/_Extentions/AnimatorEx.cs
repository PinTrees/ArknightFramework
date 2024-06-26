using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using static UnityEngine.UI.GridLayoutGroup;

public static class AnimatorEx
{
    private static Dictionary<string, string> animatorOverrideControllerStateName = new();

    private static void set_animation_clip(AnimatorOverrideController controller, string stateName, AnimationClip clip)
    {
        controller[stateName] = clip;
        //Debug.Log(controller[stateName]);
    }

    public static void SetAnimationClip(this Animator animator, string stateName, AnimationClip clip)
    {
        if (animator.runtimeAnimatorController is AnimatorOverrideController overrideController)
        {
            set_animation_clip(overrideController, stateName, clip);
            //animator.runtimeAnimatorController = overrideController; 
        }
        else
        {
            //var newOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            //set_animation_clip(newOverrideController, stateName, clip);

            Debug.LogError("[Animator-Ex] Animator controller is not an AnimatorOverrideController.");
        }
        //Debug.Log($"[Animator-Ex] {stateName} motion successfully changed. {clip.name}");
    }

    public static void SetAnimationClip(this Animator animator, AnimationClip clip)
    {
        ((AnimatorOverrideController)animator.runtimeAnimatorController)["Base Layer.DefaultState"] = clip;
    }

    public static void CrossFadeAnimatorController(this Animator animator, RuntimeAnimatorController runtimeAnimatorController)
    {
        if (animator.runtimeAnimatorController == runtimeAnimatorController)
            return;

        animator.runtimeAnimatorController = new AnimatorOverrideController(runtimeAnimatorController);
        animator.Update(Time.deltaTime);
    }

    public static void CrossFadeAvata(this Animator animator, Avatar avatar)
    {

    }

    // playable graph system
    public static PlayableGraphContainer CreatePlayableGraph(this Animator animator, string title = "RuntimeAnimationBlending")
    {
        var playableGraph = PlayableGraph.Create(title);

        return new PlayableGraphContainer()
        {
            animator = animator,
            playableGraph = playableGraph,
        };
    }


    // Action
    public static void Replay(this Animator animator)
    {
        float clipLength = animator.GetAnimationLenght();
        float newNormalizedTime = Mathf.Clamp01(0);
        float newTimeValue = newNormalizedTime * clipLength;
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, newTimeValue / clipLength);
    }



    // State
    public static float GetAnimationLenght(this Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }
    public static bool IsPlaying(this Animator animator, string tag)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag(tag);
    }
    public static bool IsPlayedOverTime(this Animator animator, string tag, float normalizedTime)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag(tag)
         && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= normalizedTime)
        {
            return true;
        }
        else return false;
    }
    public static bool IsPlayedInTime(this Animator animator, string tag, float start, float end)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag(tag)
         && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= start
         && animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= end)
        {
            return true;
        }
        else return false;
    }
    public static bool IsPlayedInTime(this Animator animator, float start, float end)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= start
         && animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= end)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// 애니메이션의 현재 재생 위치를 설정합니다.
    /// </summary>
    public static void ResetState(this Animator animator, string stateName, float point)
    {
        // Step 0: 오류 확인
        if (point < 0 || point > 1)
        {
            Debug.LogError("Normalized time 'point' must be between 0 and 1.");
            return;
        }

        // Step 1: 애니메이션 기초 세팅
        float preAnimationSpeed = animator.speed;
        bool preApplyRootMotion = animator.applyRootMotion;
        animator.applyRootMotion = false;
        animator.speed = 0.0001f;  

        // Step 2: 애니메이터 상태를 즉시 업데이트
        animator.Play(stateName, 0, point);
        animator.Update(Time.deltaTime);

        // Step 3: 재생 중지 후 복원
        animator.StopPlayback();
        animator.speed = preAnimationSpeed;
        animator.applyRootMotion = preApplyRootMotion;
    }

    public static Hashtable GetParametersHashTable(this Animator animator)
    {
        AnimatorControllerParameter[] parameters = animator.parameters;
        var savedParameters = new Hashtable();
        foreach (var param in parameters)
        {
            if (param.type == AnimatorControllerParameterType.Float)
                savedParameters[param.name] = animator.GetFloat(param.name);
            else if (param.type == AnimatorControllerParameterType.Int)
                savedParameters[param.name] = animator.GetInteger(param.name);
            else if (param.type == AnimatorControllerParameterType.Bool)
                savedParameters[param.name] = animator.GetBool(param.name);
            else if (param.type == AnimatorControllerParameterType.Trigger)
                savedParameters[param.name] = animator.GetBool(param.name); 
        }

        return savedParameters;
    }

    public static void SetParametersHashTable(this Animator animator, Hashtable saveParameters)
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Float)
                animator.SetFloat(param.name, (float)saveParameters[param.name]);
            else if (param.type == AnimatorControllerParameterType.Int)
                animator.SetInteger(param.name, (int)saveParameters[param.name]);
            else if (param.type == AnimatorControllerParameterType.Bool)
                animator.SetBool(param.name, (bool)saveParameters[param.name]);
        }
    }
}
