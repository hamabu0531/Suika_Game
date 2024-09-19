using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public GameObject audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(audioManager);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
