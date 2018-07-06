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
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {

            }
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus) {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
            Debug.Log("Found image!");
            // Create tower in the middle of map
            //Instantiate(TowerPrefab, this.transform, false);
            BuildTower(transform.position, transform.rotation);
        }
    }

    private void BuildTower(Vector3 position, Quaternion rotation) {
        Instantiate(TowerPrefab, position, rotation, this.transform);
    }

}
