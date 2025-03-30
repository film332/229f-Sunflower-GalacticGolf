using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ไม่ให้ถูกลบตอนเปลี่ยน Scene
        }
        else
        {
            Destroy(gameObject); // ถ้ามีอันเก่าอยู่แล้วก็ลบอันใหม่ทิ้ง
        }
    }
}
