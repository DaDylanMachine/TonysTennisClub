using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewScrollRect : ScrollRect
{
    private static string mouseScrollWheelAxis = "Mouse ScrollWheel";
    private bool swallowMouseWheelScrolls = true;

    private void Update()
    {
        // Detect the mouse wheel and generate a scroll. This fixes the issue where Unity will prevent our ScrollRect
        // from receiving any mouse wheel messages if the mouse is over a raycast target (such as a button).
        if (IsMouseWheelRolling())
        {
            var delta = UnityEngine.Input.GetAxis(mouseScrollWheelAxis);

            PointerEventData pointerData = new(EventSystem.current);
            pointerData.scrollDelta = new Vector2(0f, delta);

            swallowMouseWheelScrolls = false;
            OnScroll(pointerData);
            swallowMouseWheelScrolls = true;
        }
    }

    public override void OnScroll(PointerEventData data)
    {
        if (IsMouseWheelRolling() && swallowMouseWheelScrolls)
        {
            // Eat the scroll so that we don't get a double scroll when the mouse is over an image
        }
        else
        {
            // Amplify the mousewheel so that it matches the scroll sensitivity.
            if (data.scrollDelta.y < -Mathf.Epsilon)
                data.scrollDelta = new Vector2(0f, -scrollSensitivity);
            else if (data.scrollDelta.y > Mathf.Epsilon)
                data.scrollDelta = new Vector2(0f, scrollSensitivity);

            base.OnScroll(data);
        }
    }

    private static bool IsMouseWheelRolling()
    {
        return UnityEngine.Input.GetAxis(mouseScrollWheelAxis) != 0;
    }
}