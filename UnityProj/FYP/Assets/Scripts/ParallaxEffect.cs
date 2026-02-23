using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ParallaxEffect : MonoBehaviour {
    [SerializeField] private Camera Cam;
    [SerializeField] private Transform FollowPlayer;

    private Vector2 StartPos;

    private float StartingZ;

    private Vector2 CamMoveSinceStart => (Vector2)Cam.transform.position - StartPos;
    private float ParallaxFactor => (Mathf.Abs(ZdistanceFromTarget) / ClippingPlane) * 50;
    private float ZdistanceFromTarget => transform.position.z - FollowPlayer.transform.position.z;
    private float ClippingPlane => (Cam.transform.position.z + (ZdistanceFromTarget > 0 ? Cam.farClipPlane : Cam.nearClipPlane));

    private void Start() {
        StartPos = transform.position;
        StartingZ = transform.position.z;
    }
    private void Update() {
        Vector2 newPos = StartPos + CamMoveSinceStart * ParallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, StartingZ);
    }
}
