using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{

    public static bool PickedUp = false;
    [SerializeField] private List<GameObject> EnemiesToSpawn = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < EnemiesToSpawn.Count; i++)
        {
            EnemiesToSpawn[i].SetActive(false);
        }
        upPos = transform.position + new Vector3(0f, 0.2f, 0f);
        bottomPos = transform.position - new Vector3(0f, 0.2f, 0f);
    }
    Vector3 upPos;
    Vector3 bottomPos;
    bool up = true;
    private float currentTime;
    [SerializeField] private float lerpTime = 1f;

    public float EaseInOutQuad(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value + start;
        value--;
        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > lerpTime)
        {
            currentTime = 0f;
            up = !up;
        }
        if (up)
        {
            //transform.position = Vector3.Lerp(bottomPos, upPos, currentTime / lerpTime);
            transform.position = Vector3.Lerp(bottomPos, upPos, EaseInOutQuad(0,1,currentTime/lerpTime));
        }
        else
        {
            //transform.position = Vector3.Lerp(upPos, bottomPos, currentTime / lerpTime);
            transform.position = Vector3.Lerp(upPos, bottomPos, EaseInOutQuad(0, 1, currentTime / lerpTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickupHandler.PickedUp = true;
            for (int i = 0; i < EnemiesToSpawn.Count; i++)
            {
                EnemiesToSpawn[i].SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
