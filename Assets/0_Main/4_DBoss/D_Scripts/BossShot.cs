using UnityEngine;

public class BossShot : MonoBehaviour
{
    public float deleteTime = 4.0f;

    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Destroy(gameObject);
    //}

}
