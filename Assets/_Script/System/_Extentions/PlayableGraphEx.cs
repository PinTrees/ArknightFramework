using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PlayableGraphContainer
{
    public Animator animator;
    public PlayableGraph playableGraph;

    public Dictionary<string, AnimationMixerPlayableContainer> animationMixer = new();

    public void Play() => playableGraph.Play();
    public void Stop() => playableGraph.Stop();
    public void Destroy() => playableGraph.Destroy();

    public AnimationMixerPlayableContainer CreateAnimationMixer(string name, int size)
    {
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, name, animator);

        var mixerPlayable = AnimationMixerPlayable.Create(playableGraph, size);
        playableOutput.SetSourcePlayable(mixerPlayable);

        animationMixer[name] = new AnimationMixerPlayableContainer()
        {
            mixer = mixerPlayable,
            ownerGraph = playableGraph,
        };

        return animationMixer[name];
    }

    public AnimationMixerPlayableContainer GetAnimationMixer(string name)
    {
        return animationMixer[name];
    }
}

public class AnimationMixerPlayableContainer
{
    public AnimationMixerPlayable mixer;
    public PlayableGraph ownerGraph;

    public void Distroy()
    {
        try
        {
            mixer.Destroy();
        }
        catch { }
    }

    public void Play() => ownerGraph.Play();
    public void Stop() => ownerGraph.Stop();

    public void SetAnimationClip(AnimationClip clip, int index)
    {
        // diconnect
        try
        {
            Playable inputPlayable = mixer.GetInput(index);
            ownerGraph.Disconnect(mixer, index);
            inputPlayable.Destroy();
        }
        catch
        {
        }

        // create new
        var clipPlayable = AnimationClipPlayable.Create(ownerGraph, clip);
        ownerGraph.Connect(clipPlayable, 0, mixer, index);
    }

    public void SetWeight(int index, float weight)
    {
        mixer.SetInputWeight(index, weight);
    }
}

public static class PlayableGraphEx
{
}
