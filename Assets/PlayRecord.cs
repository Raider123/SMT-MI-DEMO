
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayRecord : MonoBehaviour
{
    public string animationFileName = "animation.json";
    private List<RecordAnimation.KeyframeData> keyframes;
    private int currentKeyframeIndex = 0;
    private bool isPlaying = false;
    private float startTime;

    void Start()
    {
        LoadAnimation();
        StartAnimation();
    }

    void Update()
    {
        if (isPlaying)
        {
            float currentTime = Time.time - startTime;

            if (currentKeyframeIndex < keyframes.Count)
            {
                RecordAnimation.KeyframeData currentKeyframe = keyframes[currentKeyframeIndex];

                if (currentTime >= currentKeyframe.time)
                {
                    transform.position = currentKeyframe.position;
                    transform.rotation = currentKeyframe.rotation;
                    currentKeyframeIndex++;
                }
            }
            else
            {
                isPlaying = false; // Animation zu Ende
            }
        }
    }

    public void StartAnimation()
    {
        if (keyframes != null && keyframes.Count > 0)
        {
            Debug.Log("Started Animation");
            isPlaying = true;
            startTime = Time.time;
            currentKeyframeIndex = 0;
        }
    }

    private void LoadAnimation()
    {
        string folderPath = "Hand_Transform_Runtime_Data";
        string filePath = Path.Combine(folderPath, animationFileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            RecordAnimation.KeyframeList keyframeList = JsonUtility.FromJson<RecordAnimation.KeyframeList>(json);
            keyframes = keyframeList.keyframes;

            Debug.Log("Animation geladen von: " + filePath);
        }
        else
        {
            Debug.LogError("Animationsdatei nicht gefunden: " + filePath);
        }
    }
}
