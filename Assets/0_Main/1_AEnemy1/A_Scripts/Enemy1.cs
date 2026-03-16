using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float val = Mathf.Sin(Time.time *2)*3;
        transform.position = startPos+new Vector3(val,0,0);

    }
}
