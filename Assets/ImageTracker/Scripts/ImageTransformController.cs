using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTransformController : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabSpawns;
    public ARTrackedImageManager arTrackedImageManager;
    [SerializeField] Dictionary<string, GameObject> _arObject;
    private void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObject = new Dictionary<string, GameObject>();
        SpawnItems();
    }
    private void Start()
    {
        arTrackedImageManager.trackedImagesChanged += OnTrackImageChange;
    }

    void SpawnItems()
    {
        foreach (GameObject item in prefabSpawns)
        {
            GameObject obj = Instantiate(item);
            obj.name = item.name;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.gameObject.SetActive(false);
            _arObject.Add(item.name,obj);
        }
    }

    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnTrackImageChange;
    }

    public void OnTrackImageChange(ARTrackedImagesChangedEventArgs args)
    {
        foreach(ARTrackedImage item in args.added)
        {
            UpdateTrackedImage(item);
        }
        foreach (ARTrackedImage item in args.updated)
        {
            UpdateTrackedImage(item);
        }
        foreach (ARTrackedImage item in args.removed)
        {
            _arObject[item.referenceImage.name].gameObject.SetActive(false);
        }
    }

    public void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            _arObject[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }
        Debug.Log(trackedImage.referenceImage.name);
        if (trackedImage.referenceImage.name != null)
        {
            _arObject[trackedImage.referenceImage.name].gameObject.SetActive(true);
            _arObject[trackedImage.referenceImage.name].gameObject.transform.position = trackedImage.transform.position;
        }
    }
}
