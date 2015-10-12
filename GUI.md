back to [MonoCAMArchitecture](MonoCAMArchitecture.md)

# GUI stuff #
  * OpenGL view (see below). Start with only one view. Possibility later of multiple views like in Rhino or other CAD programs.
  * Menus/toolbars. We need a smart dynamic config file for the menus and toolbars. It should be easy to reconfigure the location of commands and add/remove new commands.
  * Property editor: show available properties for the selected object (color,layer,position etc) and allow editing them
  * Operations manager: show CAM operations in a list/tree-structure. Allow editing parameters etc.
  * on-screen Selection of GEO objects
  * Selection set viewer: show currently selected objects (many if user holds down control while selecting)
  * Progress bar (used by complex CAD/CAM operations to show how long an algorithm will take) (is multi-threading worth it? i.e. GUI in thread1, CAD in thread2, CAM in thread3)

# Viewport feature list #
  * the basic functionality consists of rendering these primitives: **triangles**, **points**, **lines**
  * camera control: (the mouse events or keyboard keys used for this should be selectable by the user, for example mouse-wheel zooms, drag with left button pans, with right button rotates) see ZoomRotatePan
  * shortcut keys to X,Y,Z-projection
  * Grid. In the X,Y,Z views we should be able to turn on/off a background grid(color adjustable). The grid should autoscale when zooming.
  * coordinate view (at what coordinates is the mouse located)
  * shading control (shading vs wireframe)
  * projection control: perspective vs. orthographic