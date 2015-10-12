Back to [MonoCAM](MonoCAM.md)

# Main parts #

  * [GUI](GUI.md)
    * This is the GUI. It has an OpenGL view of current GEO objects.
    * menus and toolbars for performing CAD/CAM operations.
  * [GEO](GEO.md)
    * This is a container (Object-database?) for all geometry objects.
    * Simple GEO is Points, Lines, Triangles. They can be rendered by OpenGL and processed by CAM directly
    * non-simple GEO is Circles, Arcs, Splines, Nurbs, Surfaces etc. These objects need to be tesselated into simpleGEO before rendering and/or CAM (with some exceptions)
    * Toolpaths are also GEO objects. They consist of rapid feed, linear feed, arc feed.
  * [CAM](CAM.md)
    * These are algorithms that take some input GEO objects and produce some output GEO objects
    * 1D input: Points
    * 2D input: line segments (possibly arcs)
    * 3D input: triangles
    * Output:
      * **Linear Rapid
      *** Linear Feed
      * **Arc Feed
      *** (coolant,mist,spindle,tool,etc.)
  * [IO](IO.md)
    * This is a library that can either read files and create GEO from them (import)
    * or read GEO and write a file (export)
    * note that a post-processor is a kind of export function. it takes a toolpath and represents it as g-code
  * TOOLLIB
    * a library of cutting tools with associated parameters (cutter shapes, feed/tooth etc)
  * CUTSIM
    * Cutting simulation/verification.
    * Simple Z-map model is fairly easy to implement for 3-axis machining.
    * The result of the simulation is most probably a triangulated surface, so the GUI should have a mode where it can show an animated changing triangulated surface.


# Patterns #

we need to identify some useful design patterns for how components communicate.

The **Command** pattern is probably useful for implementing undo/redo.

The **chain-of-responsibility** pattern is probably needed between GEO and GUI so that GUI knows when a change has happened in GEO.

The abstract-factory idea could be used for import of geometry.

Something on C# patterns is found here: http://en.csharp-online.net/CSharp_Design_Patterns_Made_Simple