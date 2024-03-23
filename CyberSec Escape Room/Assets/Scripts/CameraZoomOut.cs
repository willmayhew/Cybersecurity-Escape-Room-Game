using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;

    public float zoomOutDuration = 3f;
    public float zoomOutFOV = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        StartCoroutine(ZoomCameraOutAndIn());

    }

    IEnumerator ZoomCameraOutAndIn()
    {

        // Zoom out
        float originalZoom = virtualCamera.m_Lens.OrthographicSize;
        float timer = 0f;
        while (timer < zoomOutDuration)
        {
            float t = timer / zoomOutDuration;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(originalZoom, zoomOutFOV, t);
            timer += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = zoomOutFOV;

    }
}
