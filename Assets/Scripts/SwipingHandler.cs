using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwipingHandler : MonoBehaviour 
{

	public enum SwipeDirection
	{
		Up,
		Down,
		Left,
		Right,
	}

	public Action<SwipeDirection> UserSwiped;
	
	// Use this for initialization
	void Start ()
	{
		UserSwiped += direction =>
		{
			switch (direction)
			{
				case SwipeDirection.Up:
					break;
				case SwipeDirection.Down:
					break;
				case SwipeDirection.Right:
					break;
				case SwipeDirection.Left:
					break;
				default:
					break;
			}
		};
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		//CheckMouseInput();
		CheckMobileInput();
	}

	private bool swiping = false;
	private List<Vector2> swipingPoints = new List<Vector2>();

	private int? currentTouch;
	private void CheckMobileInput()
	{
		foreach (Touch touch in Input.touches)
		{
			if (currentTouch == null) currentTouch = touch.fingerId;

			if (currentTouch != touch.fingerId) continue;
			
			swipingPoints.Add(touch.position);
			return;
		}

		if (currentTouch == null) return;
		
		currentTouch = null;
		SwipeDirection direction = GetDirection(swipingPoints);
		UserSwiped(direction);
	}
	
	private void CheckMouseInput()
	{
		if (Input.GetMouseButton(0))
		{
			if (!swiping)
			{
				swipingPoints.Clear();
				swiping = true;
			} 
			
			swipingPoints.Add(Input.mousePosition);
		}
		else if (swiping)
		{
			swiping = false;
			SwipeDirection direction = GetDirection(swipingPoints);
			UserSwiped(direction);
		}
	}

	private SwipeDirection GetDirection(List<Vector2> swipingPoints)
	{
		float xAmount = swipingPoints.Last().x - swipingPoints.First().x;
		float yAmount = swipingPoints.Last().y - swipingPoints.First().y;

		if (Math.Abs(xAmount) > Math.Abs(yAmount))
		{
			// left or right
			return xAmount > 0 ? SwipeDirection.Right : SwipeDirection.Left;
		}
		else
		{
			// up or down
			return yAmount > 0 ? SwipeDirection.Up : SwipeDirection.Down;
		}

	}
}
