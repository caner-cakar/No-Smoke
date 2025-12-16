using UnityEngine;
using System.Collections;

public class BlendShapeLooper : MonoBehaviour
{
    public SkinnedMeshRenderer[] meshes;
    public int blendShapeIndex = 0;
    public float cycleDuration = 1f;
    public bool autoPlay = true;

    Coroutine routine;
    float timeValue;
    bool playing;

    public bool IsPlaying => playing;

    void Start()
    {
        if (meshes == null || meshes.Length == 0)
            meshes = GetComponentsInChildren<SkinnedMeshRenderer>();

        if (autoPlay)
            ResumeLoop();
    }

    public void PlayFromStart()
    {
        timeValue = 0f;
        ResumeLoop();
    }

    public void PauseLoop()
    {
        playing = false;
        if (routine != null)
            StopCoroutine(routine);
        routine = null;
    }

    public void ResumeLoop()
    {
        if (meshes == null || meshes.Length == 0)
            return;
        if (playing)
            return;
        if (blendShapeIndex < 0)
            return;

        playing = true;
        if (routine != null)
            StopCoroutine(routine);
        routine = StartCoroutine(LoopRoutine());
    }

    IEnumerator LoopRoutine()
    {
        while (playing)
        {
            timeValue += Time.deltaTime;
            float phase = Mathf.PingPong(timeValue / cycleDuration, 1f);
            float weight = phase * 100f;

            for (int i = 0; i < meshes.Length; i++)
            {
                var m = meshes[i];
                if (m == null)
                    continue;
                var sm = m.sharedMesh;
                if (sm == null)
                    continue;
                if (blendShapeIndex >= sm.blendShapeCount)
                    continue;

                m.SetBlendShapeWeight(blendShapeIndex, weight);
            }

            yield return null;
        }
    }
}
