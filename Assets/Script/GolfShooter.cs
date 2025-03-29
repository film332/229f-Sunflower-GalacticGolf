using UnityEngine;
using UnityEngine.SceneManagement; // เพิ่มการใช้งาน SceneManagement
using TMPro;

public class GolfShooter : MonoBehaviour
{
    public Transform aimDirection;      // Drag AimDirection ที่ลูกกอล์ฟมาวาง
    public LineRenderer aimLine;          // Drag AimLine (ที่มี LineRenderer)
    public Rigidbody rb;                // Rigidbody ของลูกกอล์ฟ
    public float maxPower = 10f;          // ค่าแรงสูงสุดที่ชาร์จได้
    public float chargeSpeed = 5f;        // ความเร็วชาร์จแรง
    public float ballMass = 1f;           // มวลลูกกอล์ฟ (ปรับตามดาว)
    public TextMeshProUGUI powerText;     // Drag UI Text สำหรับแสดงค่าแรง
    public TextMeshProUGUI shotText;      // ลาก ShotText เข้ามาใน Inspector
    public TextMeshProUGUI winText;       // ลาก WinText เข้ามาใน Inspector
    public GameObject shootEffectPrefab; // Drag Prefab FX ยิงเข้ามา
    public Transform shootEffectSpawnPoint; // จุดที่ FX จะเกิด
    private Vector3 startPosition;       // ตำแหน่งเริ่มต้น
    private Quaternion startRotation;    // หมุนเริ่มต้น
    private float currentPower = 0f;
    private bool isCharging = false;
    private bool isMoving = false;
    private int shotCount = 0;            // จำนวนครั้งยิง
    private float shotTimer = 0f;         // เวลาหลังจากยิง
    public float maxShotTime = 10f;       // เวลาสูงสุดก่อนรีเซ็ต
    private bool win = false;             // รู้ว่ายิงเข้าหลุมไหม

    void Start()
    {
        startPosition = transform.position; // บันทึกตำแหน่งเริ่มต้น
        startRotation = transform.rotation; // บันทึกการหมุนเริ่มต้น
    }

    void Update()
    {
        // เมื่อบอลไม่เคลื่อนที่ ให้เปิดระบบชาร์จและแสดง UI กับเส้นเล็ง
        if (!isMoving)
        {
            // ชาร์จแรงเมื่อกด Spacebar ค้าง
            if (Input.GetKey(KeyCode.Space))
            {
                isCharging = true;
                currentPower += chargeSpeed * Time.deltaTime;
                currentPower = Mathf.Clamp(currentPower, 0, maxPower);

                // อัปเดต UI แสดงค่าแรง
                if (powerText != null)
                    powerText.text = "Power: " + Mathf.RoundToInt(currentPower);

                // ให้เส้นเล็งแสดงอยู่
                if (aimLine != null && !aimLine.enabled)
                    aimLine.enabled = true;
            }

            // เมื่อปล่อย Spacebar และกำลังชาร์จอยู่ → ยิงลูก
            if (Input.GetKeyUp(KeyCode.Space) && isCharging)
            {
                Shoot();
            }
        }
        else
        {
            // ในขณะที่ลูกเคลื่อนที่ ให้ซ่อน UI (UI จะถูกเคลียร์)
            if (powerText != null)
                powerText.text = "";
        }

        // จับเวลาและรีเซ็ตเมื่อครบ 10 วินาที
        if (isMoving && !win)
        {
            shotTimer += Time.deltaTime;

            if (shotTimer >= maxShotTime)
            {
                ResetBall();
            }
        }
    }

    void Shoot()
    {
        // ยิงลูกตามทิศทางของ aimDirection
        Vector3 dir = aimDirection.forward;
        float force = ballMass * currentPower;
        rb.AddForce(dir * force, ForceMode.Impulse);
        isCharging = false;
        isMoving = true;

        // ✨ เล่น Particle Effect ตอนยิง
        if (shootEffectPrefab != null && shootEffectSpawnPoint != null)
        {
            Instantiate(shootEffectPrefab, shootEffectSpawnPoint.position, Quaternion.identity);
        }

        // ซ่อนเส้นเล็งและ UI ทันทีเมื่อยิงลูก
        if (aimLine != null)
            aimLine.enabled = false;
        if (powerText != null)
            powerText.text = "";

        // ✨ เพิ่มจำนวนครั้งยิง
        shotCount++;

        // ✨ แสดงผลบน UI
        if (shotText != null)
            shotText.text = "Shots: " + shotCount;

        // เริ่มจับเวลาเมื่อยิง
        shotTimer = 0f;
        win = false; // รีเซ็ตสถานะการชนะ
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("You Win!");
            ShowWinUI();
            win = true; // ตั้งค่า win เป็น true

            // ปิดการควบคุม (ตามเดิม)
            this.enabled = false;
        }
    }

    void ShowWinUI()
    {
        if (winText != null)
            winText.enabled = true; // แสดงข้อความชนะ

        // ✨ โหลดด่านถัดไปหลังดีเลย์ 2 วินาที
        Invoke("LoadNextLevel", 2f);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // ถ้ามีด่านถัดไปจริง ค่อยโหลด
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // ถ้าไม่มีด่านแล้ว = กลับเมนู (หรือจบด่าน)
            SceneManager.LoadScene("MainMenu"); // เปลี่ยนตามชื่อ Scene เมนูของเธอ
        }
    }

    void ResetBall()
    {
        transform.position = startPosition; // รีเซ็ตตำแหน่งลูก
        transform.rotation = startRotation; // รีเซ็ตการหมุนลูก

        rb.velocity = Vector3.zero; // รีเซ็ตความเร็ว
        rb.angularVelocity = Vector3.zero; // รีเซ็ตความเร็วหมุน

        currentPower = 0; // รีเซ็ตค่าแรง
        isMoving = false; // รีเซ็ตสถานะการเคลื่อนที่
        isCharging = false; // รีเซ็ตสถานะการชาร์จ
        shotTimer = 0f; // รีเซ็ตตัวจับเวลา
        win = false; // รีเซ็ตสถานะการชนะ

        if (aimLine != null) aimLine.enabled = true; // เปิดเส้นเล็งอีกครั้ง
        if (powerText != null) powerText.text = "Power: 0"; // รีเซ็ต UI แสดงค่าแรง
    }
}