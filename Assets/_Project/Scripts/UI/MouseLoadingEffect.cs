using System.Xml.Serialization;
using UnityEngine;

public class MouseLoadingEffect : MonoBehaviour
{
    Vector3 mousePosition;
    [SerializeField] float distanceFromCamera;
    public ParticleSystem pp;
    [SerializeField] Camera uiCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Hide by default
        pp = GetComponentInChildren<ParticleSystem>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Make it follow the mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // distance from camera
        transform.position = uiCam.ScreenToWorldPoint(mousePos);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        if (!pp.isPlaying)
            pp.Play();
    }

    public void Hide()
    {
        pp.Stop();
        gameObject.SetActive(false);
    }
}
