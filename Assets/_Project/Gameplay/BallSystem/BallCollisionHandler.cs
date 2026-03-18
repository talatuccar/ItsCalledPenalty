using UnityEngine;

public class BallCollisionHandler : MonoBehaviour
{
    [SerializeField] private float bounceMultiplier = 1.2f; // Sekme þiddeti çarpaný

    private void OnCollisionEnter(Collision collision)
    {
        // Eðer çarptýðýmýz þey kaleciyse
        if (collision.gameObject.CompareTag("Goalkeeper"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            // Çarpýþma anýndaki yönü al ve biraz yukarý/rastgelelik ekle
            Vector3 reflexDirection = Vector3.Reflect(rb.linearVelocity, collision.contacts[0].normal);

            // Topu biraz ileri ve yukarý doðru fýrlat (Daha heyecanlý pozisyonlar için)
            rb.AddForce(reflexDirection * bounceMultiplier + Vector3.up * 2f, ForceMode.Impulse);

            Debug.Log("Top kaleciden sekti!");
        }
    }
}