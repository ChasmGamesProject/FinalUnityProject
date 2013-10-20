using UnityEngine;
using System.Collections;

public class WorldHelper
{
	public static bool isPlayerInRange(Transform myTransform)
	{
		if(myTransform != null && GlobalVars.player_transform != null)
		{
			Vector2 a = new Vector2(myTransform.position.x, myTransform.position.z);
			Vector2 b = new Vector2(GlobalVars.player_transform.position.x, GlobalVars.player_transform.position.z);
			float d = Vector2.Distance(a, b);
			if(d < 0.4f)
				return true;
			else
				return false;
		}
		else
			return false;
	}
}
