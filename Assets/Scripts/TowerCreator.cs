using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TowerCreator : MonoBehaviour, ITrackableEventHandler {
    public GameObject TowerPrefab;

    private bool createdFirstTower = false;
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
                if (hit.transform == this.transform) {
                    BuildTower(hit.point, Quaternion.identity);
                }
            }
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus) {
        bool wasDetected = (newStatus == TrackableBehaviour.Status.DETECTED ||
                            newStatus == TrackableBehaviour.Status.TRACKED ||
                            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED);
        if  (!createdFirstTower && wasDetected) {
            // Create tower in the middle of map
            BuildTower(transform.position, transform.rotation);
        }
    }

    private void BuildTower(Vector3 position, Quaternion rotation) {
        Debug.Log("Creating tower in: " + position);
        GameObject tower = (GameObject)Instantiate(TowerPrefab, position, rotation, this.transform);
    }

}
