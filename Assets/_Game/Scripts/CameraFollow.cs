using System;
using UnityEngine;

[Serializable]
public struct CameraOffsetConfigByStackCount
{
    public int stackThreshold;    
    public Vector3 offset;        
    public Vector3 rotationOffset; 
}

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    [Header("Cấu hình các mốc Camera")]
    public CameraOffsetConfigByStackCount[] cameraConfigs; 

    private CameraOffsetConfigByStackCount activeConfig;

    public void OnInit()
    {
        if (cameraConfigs != null && cameraConfigs.Length > 0)
        {
            activeConfig = cameraConfigs[0];
        }
        if (target == null)
        {
            Debug.LogError("CameraFollow: No target assigned.");
        }
        transform.position = target.position + activeConfig.offset;
        transform.rotation = Quaternion.Euler(activeConfig.rotationOffset);
    }


    public void UpdateCameraMilestone(int currentStackCount)
    {
        if (cameraConfigs == null) return;
        
        foreach (var config in cameraConfigs)
        {
            if (currentStackCount >= config.stackThreshold)
            {
                activeConfig = config; 
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + activeConfig.offset;
        Quaternion targetRotation = Quaternion.Euler(activeConfig.rotationOffset);

        float smoothSpeed = 8f * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed);
    }
}
