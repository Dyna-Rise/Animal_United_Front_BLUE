using UnityEngine;

public class BossSlash : MonoBehaviour
{
    public float deleteTime = 0.4f;

    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

}
