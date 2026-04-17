using System;
using UnityEngine;

[Serializable]
public struct CameraOffsetConfigByStackCount
{
    public int stackThreshold;    // Mốc số gạch (Ví dụ: 10, 20, 30)
    public Vector3 offset;        // Khoảng cách Camera
    public Vector3 rotationOffset; // Góc xoay Camera
}

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance; // Singleton để gọi từ StackManager dễ dàng

    public Transform target; // Kéo vật thể PlayerRoot vào đây

    [Header("Cấu hình các mốc Camera")]
    public CameraOffsetConfigByStackCount[] cameraConfigs; 

    private CameraOffsetConfigByStackCount activeConfig;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    void Start()
    {
        // Gán mốc mặc định ban đầu (Mốc 0)

    }

    // Hàm này sẽ được gọi mỗi khi Player ăn hoặc rớt gạch
    public void UpdateCameraMilestone(int currentStackCount)
    {
        if (cameraConfigs == null) return;
        
        // Duyệt qua tất cả các mốc bạn đã tạo
        foreach (var config in cameraConfigs)
        {
            // Nếu số gạch hiện tại VƯỢT QUA hoặc BẰNG mốc này
            if (currentStackCount >= config.stackThreshold)
            {
                activeConfig = config; 
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Tính toán vị trí đích đến của Camera
        Vector3 targetPosition = target.position + activeConfig.offset;
        Quaternion targetRotation = Quaternion.Euler(activeConfig.rotationOffset);

        // 2. Di chuyển Camera siêu mượt bằng Lerp
        float smoothSpeed = 5f * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed);
    }
}
