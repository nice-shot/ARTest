using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TowerCreator : MonoBehaviour, ITrackableEventHandler {
    public GameObject TowerPrefab;

    private TrackableBehaviour mTrackableBehavior;

    void Start() {
        mTrackableBehavior = GetComponent<TrackableBehaviour>();
        if (mTrackableBehavior) {
            mTrackableBehavior.RegisterTrackableEventHandler(this);
        }
    }

    void Update() {

    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus) {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
            Debug.Log("Found image!");
            // Create tower in the middle of map
            Instantiate(TowerPrefab, this.transform, false);
        }
    }

}
