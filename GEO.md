back to [MonoCAMArchitecture](MonoCAMArchitecture.md)

# How Geometry is stored #
There should be a container for geometry objects, and maybe a separate one for ToolPath data.

When either the GUI or a CAM function or a file import function wants to create new geometry objects it should probably be done with something like
```
geo_container.add(new Point(x,y,z))
geo_container.add(new Line(x1,y1,z1,x2,y2,z2))
```
The geo\_container should have a mechanism for telling the GUI whenever a change has occurred. The gui will the requires the glData from the object and create a display-list for it. The same thing happens when the object is moved (for example dragging a point), or when the object is deleted.

It possibly makes sense to have a different container for toolpath data because these are exclusively created by CAM operations and they are modified through the operations manager and not through the normal CAD interface. See ToolPath

## Individual GEO Objects ##
Each geometric entity (point, curve, surface) consists of a GEO object.

Each GEO object should have:

Each geometry object should have these common methods available (probably makes sense to implement this as an Interface?)
  * properties (what properties of this object can the GUI edit)
    * A unique name
    * some method for automagically finding out the list of possible properties is needed!
    * layer
    * color
    * rendermode
    * shown/hidden
  * glData (i.e. an OpenGL interface)
    * color
    * rendermode (shade/wireframe)
    * type (point, line, triangle)
    * points (interpreted as coordinates for either point, line, triangle)
    * the object should probably store the display-list ID
    * a mechanism for messaging to the GUI that something has changed and we need to generate a new display-list
  * CAM interface
    * A method for outputting itself in a CAM friendly format. This is mostly the same format as for OpenGL (points,lines,triangles), but the difference is that a given tolerance value in mm should be respected when converting to triangles/lines

Examples of simple GEO objects are:
  * STLSurf, a triangulated surface (for example read from an STL file)
  * Point
  * Line
  * Arc/Circle
  * ToolPath

Compound objects could easily be created from simple objects. Such as:
  * rectangle (set of four lines)
  * etc

## Relational Geometry ##
RelationalGeometry is a nice object oriented way of implementing parametric CAD.