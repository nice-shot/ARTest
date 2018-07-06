using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TowerCreator : MonoBehaviour, ITrackableEventHandler {
    public GameObject TowerPrefab;

    private bool createdFirstTower = false;
    private TrackableBehaviour mTrackableBehavior;
    private Stack<GameObject> towers = new Stack<GameObject>();

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
                    // Change the tower's rotation to add variety
                    Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    BuildTower(hit.point, rotation);
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
        GameObject tower = (GameObject)Instantiate(TowerPrefab, position, rotation, this.transform);
        towers.Push(tower);
    }

    public void Reset() {
        // Remove all towers but the first one
        while(towers.Count > 1) {
            GameObject tower = towers.Pop();
            Destroy(tower);
        }
    }

}
