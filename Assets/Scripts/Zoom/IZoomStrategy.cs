using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZoomStrategy // template to be used, camera controlled by orthographiczoom
{
    void ZoomIn(Camera cam, float delta, float nearZoomLimit); // accounts for time.delta time and speed of zooming, limited at nearzoomlimit
    void ZoomOut(Camera cam, float delta, float farZoomLimit); 
}

