using UnityEngine;
using System.Collections;

public enum PauseState
{
	SCENE_IS_PAUSED,
	SCENE_IS_RESUMED,
}

public class SceneStepper : MonoBehaviour {

	public static PauseState currentPauseState = PauseState.SCENE_IS_RESUMED;
	public const string STATE_IS_PAUSED = "STATE_IS_PAUSED";
	public const string STATE_IS_RESUMED = "STATE_IS_RESUMED";
    private bool coroutineRunning = false;
	private const float sceneStepTime = 0.009f;

	void Update ( ) {
	    if( Input.GetKeyDown( KeyCode.Space ) )
		{
			TogglePauseState( );
		}
		if( !coroutineRunning )
		{
			StepThroughInput( );
	    }
    }

	private void TogglePauseState( )
	{
		if( currentPauseState == PauseState.SCENE_IS_RESUMED )
		{
			PauseFrame( );
			return;
		}
		ResumeFrame( );
	}

	private void StepThroughInput( )
	{
		if( Input.GetKeyDown( KeyCode.M ) )
		{
			StartCoroutine( StepThroughScene( 1.0f ) );
		}
	}
	 
	IEnumerator StepThroughScene( float direction )
	{
		coroutineRunning = true;
		ResumeFrame( );
		yield return new WaitForSeconds( sceneStepTime );
		coroutineRunning = false;
		PauseFrame( );
	}

	private void ResumeFrame( )
	{
		currentPauseState = PauseState.SCENE_IS_RESUMED;
		Subject.Notify( STATE_IS_RESUMED );
		Time.timeScale = 1.0f;
	}

	private void PauseFrame( )
	{
		currentPauseState = PauseState.SCENE_IS_PAUSED;
		Subject.Notify( STATE_IS_PAUSED );
        Time.timeScale = 0.0f;
	}
}