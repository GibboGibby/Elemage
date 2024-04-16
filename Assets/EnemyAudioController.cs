using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyAudioController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip startingInvestSound;
    [SerializeField] private AudioClip AlertSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "startingInvest":
                audioSource.PlayOneShot(startingInvestSound);
                break;

            case "alert":
                audioSource.PlayOneShot(AlertSound);
                break;
        }
    }
}
