using UnityEngine;
using System.Collections;

public class ScreenRelativePosition : MonoBehaviour {

	//http://www.raywenderlich.com/70344/unity-2d-tutorial-physics-and-screen-sizes
	public enum ScreenEdge{LEFT,RIGHT,TOP,BOTTOM}
	public ScreenEdge screenEdge;
	public float yOffset;
	public float xOffset;

	// Use this for initialization
	void Start () {
		Vector3 newPosition = transform.position;
		Camera camera = Camera.main;

		switch (screenEdge) {
		case ScreenEdge.RIGHT:
			newPosition.x = camera.aspect * camera.orthographicSize + xOffset;
			newPosition.y = yOffset;
			break;

		case ScreenEdge.TOP:
			newPosition.y = camera.orthographicSize + yOffset;
			newPosition.x = xOffset;
			break;

		case ScreenEdge.LEFT:
			newPosition.x = -camera.aspect * camera.orthographicSize + xOffset;
			newPosition.y = yOffset;
			break;

		case ScreenEdge.BOTTOM:
			newPosition.y = -camera.orthographicSize + yOffset;
			newPosition.x = xOffset;
			break;

		default:
			break;
		}

		transform.position = newPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
