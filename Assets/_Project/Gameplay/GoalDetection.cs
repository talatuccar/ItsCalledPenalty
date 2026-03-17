using UnityEngine;

public class GoalDetection : MonoBehaviour
{

    public enum NetSide { Left, Center, Right }
    public NetSide side;
    [SerializeField] private NetSoftPhysics deformer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 pushDir = new Vector3(1, 0, 0); // Kalenin içine doðru
            Vector3 rotPower = Vector3.zero;

            // Bölgeye göre farklý rotasyon "sahtekarlýðý" yapýyoruz
            switch (side)
            {
                case NetSide.Left:
                    rotPower = new Vector3(0, 0, 15f); // Sol köþe havalansýn
                    break;
                case NetSide.Right:
                    rotPower = new Vector3(0, 0, -15f); // Sað köþe havalansýn
                    break;
                case NetSide.Center:
                    rotPower = new Vector3(10f, 0, 0); // Orta yukarý esnesin
                    break;
            }

            deformer.Impact(pushDir, rotPower);
        }
    }
}