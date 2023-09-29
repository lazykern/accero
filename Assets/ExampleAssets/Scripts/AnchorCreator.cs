using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//
// This script allows us to create anchors with
// a prefab attached in order to visbly discern where the anchors are created.
// Anchors are a particular point in space that you are asking your device to track.
//

[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class AnchorCreator : MonoBehaviour
{
    // This is the prefab that will appear every time an anchor is created.
    [FormerlySerializedAs("m_AnchorPrefab")]
    [SerializeField]
    GameObject mAnchorPrefab;

    public GameObject AnchorPrefab
    {
        get => mAnchorPrefab;
        set => mAnchorPrefab = value;
    }

    // Removes all the anchors that have been created.
    public void RemoveAllAnchors()
    {
        foreach (var anchor in _mAnchorPoints)
        {
            Destroy(anchor);
        }
        _mAnchorPoints.Clear();
    }

    // On Awake(), we obtains a reference to all the required components.
    // The ARRaycastManager allows us to perform raycasts so that we know where to place an anchor.
    // The ARPlaneManager detects surfaces we can place our objects on.
    // The ARAnchorManager handles the processing of all anchors and updates their position and rotation.
    void Awake()
    {
        _mRaycastManager = GetComponent<ARRaycastManager>();
        _mAnchorManager = GetComponent<ARAnchorManager>();
        _mPlaneManager = GetComponent<ARPlaneManager>();
        _mAnchorPoints = new List<ARAnchor>();
    }

    void Update()
    {
        // If there is no tap, then simply do nothing until the next call to Update().
        if (Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        if (_mRaycastManager.Raycast(touch.position, _sHits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = _sHits[0].pose;
            var hitTrackableId = _sHits[0].trackableId;
            var hitPlane = _mPlaneManager.GetPlane(hitTrackableId);

            // This attaches an anchor to the area on the plane corresponding to the raycast hit,
            // and afterwards instantiates an instance of your chosen prefab at that point.
            // This prefab instance is parented to the anchor to make sure the position of the prefab is consistent
            // with the anchor, since an anchor attached to an ARPlane will be updated automatically by the ARAnchorManager as the ARPlane's exact position is refined.
            var anchor = _mAnchorManager.AttachAnchor(hitPlane, hitPose);
            Instantiate(mAnchorPrefab, anchor.transform);

            if (anchor == null)
            {
                Debug.Log("Error creating anchor.");
            }
            else
            {
                // Stores the anchor so that it may be removed later.
                _mAnchorPoints.Add(anchor);
            }
        }
    }

    static List<ARRaycastHit> _sHits = new List<ARRaycastHit>();

    List<ARAnchor> _mAnchorPoints;

    ARRaycastManager _mRaycastManager;

    ARAnchorManager _mAnchorManager;

    ARPlaneManager _mPlaneManager;
}
