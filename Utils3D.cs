using System.Linq;
using Godot;
using Godot.Collections;

namespace core.utils._3d;

public static class Utils3D
{
    public static readonly Vector3 OutOfBound = new Vector3(-999, -999, -999);

    public static bool IsOutOfBound(this Vector3 in_vector)
    {
        return in_vector == OutOfBound;
    }
    
    public static Dictionary GetMouseRayCast3D(this Camera3D in_camera, Array<CollisionObject3D> in_excludeList, out Vector3 out_position)
    {
        Vector2 mouseViewPosition = in_camera.GetViewport().GetMousePosition();
        float raycastLength = 1000.0f;
        Vector3 raycastFrom = in_camera.ProjectRayOrigin(mouseViewPosition);
        Vector3 raycastTo = raycastFrom + in_camera.ProjectRayNormal(mouseViewPosition) * raycastLength;
        Array<Rid> exclude = new (in_excludeList.Select((in_c) => in_c.GetRid()));
        PhysicsDirectSpaceState3D spaceState3D = in_camera.GetWorld3D().DirectSpaceState;
        PhysicsRayQueryParameters3D raycastParameters = new ();
        raycastParameters.From = raycastFrom;
        raycastParameters.To = raycastTo;
        raycastParameters.Exclude = exclude;
        Dictionary rayCastResult = spaceState3D.IntersectRay(raycastParameters);
        if (rayCastResult.Count == 0)
            out_position = OutOfBound;
        else 
            out_position = (Vector3)rayCastResult["position"];
        return rayCastResult;
    }

    public static Vector3 GetMouseWorldPosition(this Camera3D in_camera, Array<CollisionObject3D> in_excludeList)
    {
        in_camera.GetMouseRayCast3D(in_excludeList, out Vector3 out_position);
        return out_position;
    }
}

public static class Extensions
{
    public static Vector2 To2D(this Vector3 in_vector)
    {
        return new Vector2(in_vector.X, in_vector.Z);
    }

    public static Vector3 To3D(this Vector2 in_vector)
    {
        return new Vector3(in_vector.X, 0, in_vector.Y);
    }
}