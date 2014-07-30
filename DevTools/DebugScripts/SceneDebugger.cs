using UnityEngine;
using System.Collections;

public enum ToggleDebugViewable
{
	DEBUG_VIEWABLE,
	DEBUG_HIDDEN
}

public class SceneDebugger : MonoBehaviour
{
	public static ToggleDebugViewable toggleView = ToggleDebugViewable.DEBUG_HIDDEN;
	private DebuggerFPS currentFrames;
	private Rect mTopLeft = new Rect( 0, 0, 100, 50 ); 
	private Rect mBottomLeft = new Rect ( 0, Screen.height - 50, 100, 50 );
	private Rect mBottomRight = new Rect( Screen.width - 100, Screen.height - 50, 100, 50 );
	private bool coroutineRunning;
	private SceneReverser sceneReverser;
	
	void Start( )
	{
		DontDestroyOnLoad( this );
		sceneReverser = GetComponent< SceneReverser >( );
		SetLabelReferences( );
		AddDebugObserver( );
	}

	void OnGUI( )
	{
		if( Input.GetKeyDown( KeyCode.H  ) && !coroutineRunning )
		{
			StartCoroutine( WaitAfterInput( ) );
			Subject.NotifySendObject( gameObject, SceneDebuggerObserver.TOGGLE_VIEWABLE );
		}
		if( toggleView == ToggleDebugViewable.DEBUG_VIEWABLE  )
		{
			FrameInformationBox( );
		}
	}

	private void SetLabelReferences( )
	{
		currentFrames = GetComponent< DebuggerFPS >(  );
    }

	private void AddDebugObserver( )
	{
        #pragma warning disable 0168
		var sceneDebuggerObserver = new SceneDebuggerObserver( );
	}

	private IEnumerator WaitAfterInput( )
	{
		coroutineRunning = true;
	    yield return new WaitForSeconds( 0.05f );
		coroutineRunning = false;
	}

	private void FrameInformationBox( )
	{
		GUI.Box( new Rect( Screen.width - 200, 0, 200, 400 ), "Frame Information" );
		GUI.Label( new Rect( Screen.width - 160, 40, 100, 50 ), currentFrames.FramesPerSecond( ) );
		GUI.Label( new Rect( Screen.width - 160, 60, 100, 50 ), Subject.NumberOfObservers( ) );
		GUI.Label( new Rect( Screen.width - 160, 80, 100, 50 ), Subject.NumberOfUnityObservers( ) );
		GUI.Label( new Rect( Screen.width - 160, 100, 100, 50 ), "Target Position\" +
			"
		          n" + sceneReverser.GetTargetPosition( ) );
	}
}
