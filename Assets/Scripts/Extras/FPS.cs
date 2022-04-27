using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    private float deltaTime;
    private float fps;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
    }
}