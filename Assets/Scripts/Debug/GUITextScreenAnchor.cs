using UnityEngine;
using System.Collections;

/**
 *  fix camera on the screen
 * */
 
public class GUITextScreenAnchor: MonoBehaviour {

	
	// LTOP - Left Top
	// RTOP = Right Top
	public enum Pos {
		LTOP,
		RTOP,
	}
	public	Camera drawCamera;
	public 	Pos pos;

	void updateScreenPos( float width, float height ) 
	{
		if ( guiText == null ) return;
		switch ( pos ) {
		case Pos.LTOP:
			guiText.anchor = TextAnchor.UpperLeft;
			guiText.pixelOffset = new Vector2( 0, height );
			break;
		case Pos.RTOP:
			guiText.anchor = TextAnchor.UpperRight;
			guiText.pixelOffset = new Vector2( width, height );
			break;
		}		
	}
	
	void Start() {
		if ( drawCamera != null ) {
			updateScreenPos( drawCamera.pixelRect.width, drawCamera.pixelRect.height );
		}
	}
	
#if UNITY_EDITOR
	void Update() {
		if ( drawCamera != null ) {
			updateScreenPos( drawCamera.pixelRect.width, drawCamera.pixelRect.height );
		}
	}
#endif
}
