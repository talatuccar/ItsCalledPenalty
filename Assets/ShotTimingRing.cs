using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShotTimingRing : MonoBehaviour
{
    private LineRenderer _line;
    [SerializeField] private int segments = 50; // Halkanýn pürüzsüzlüðü
    private bool _isActive = false;
    [Header("Boyut Ayarlarý")]
    public float maxScale = 2.0f;
    public float minScale = 0.4f;
    public float speed = 2.0f;

    [Header("Renk Ayarlarý")]
    public Color badColor = Color.red;
    public Color perfectColor = Color.green;

    private float _timer;
    public float Accuracy { get; private set; }

    void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.enabled = false; // Baþlangýçta gizli
    }

    public void Activate()
    {
        _isActive = true;
        _line.enabled = true;
        _timer = 0; // Her seferinde en büyükten baþlasýn
    }

    public void Deactivate()
    {
        _isActive = false;
        _line.enabled = false;
    }
    void Update()
    {
        if (!_isActive) return;
        // Zamanlayýcý (0 ile 1 arasý gidip gelir)
        _timer += Time.deltaTime * speed;
        float pingPong = Mathf.PingPong(_timer, 1f);
        Accuracy = pingPong; // 1 = En dar (Yeþil), 0 = En geniþ (Kýrmýzý)

        // Mevcut boyutu hesapla ve çiz
        float currentRadius = Mathf.Lerp(maxScale, minScale, pingPong);
        DrawCircle(currentRadius);

        // Rengi güncelle
        Color currentColor = Color.Lerp(badColor, perfectColor, pingPong);
        _line.startColor = currentColor;
        _line.endColor = currentColor;

        // Zemine yatay durmasý için rotasyonu sabitle (Ebeveyni dönse de o dönmez)
        transform.rotation = Quaternion.Euler(90, 0, 0);
        transform.position = transform.parent.position + Vector3.up * 0.05f;
    }

    void DrawCircle(float radius)
    {
        _line.positionCount = segments + 1;
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            _line.SetPosition(i, new Vector3(x, y, 0));
            angle += (360f / segments);
        }
    }
}