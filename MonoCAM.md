# MonoCAM is: #
  * a CAD program - you can design and manipulate points, curves, and surfaces
  * a CAM program - you can calculate and output toolpaths for cnc-machines
  * Open-Source, licensed under GPLv2
  * written in C#/mono, so it's cross-platform

# Architecture #
[MonoCAMArchitecture](MonoCAMArchitecture.md) describes plans for the program architecture.
  * GUI (interaction with the user, OpenGL redering)
    * ZoomRotatePan (a bit of math for gluLookAt)
  * [CAM](CAM.md) (toolpath calculations)
    * DropCutter (raster-finish paths on surfaces)
    * TravelingSalesman (optimizing drilling etc operations)
  * [GEO](GEO.md)
    * RelationalGeometry
    * ToolPath
  * [IO](IO.md) (Input/Output of geometry and toolpaths)
  * TOOLLIB
  * CUTSIM

It's not clear yet if we should write everything from scratch or use some kind of FrameWork.

# Links #
  * Homepage: http://code.google.com/p/monocam
  * SVN Mailing list http://groups.google.com/group/monocam-svn
  * http://wiki.linuxcnc.org/cgi-bin/emcinfo.pl?Cam
  * http://wiki.linuxcnc.org/cgi-bin/emcinfo.pl?List_Of_CAM_References
  * http://www.editthis.info/opencam/Main_Page

There are also OtherProjects

There is a BookList with books related to the project.

Looking at other CAM systems: CamVideos