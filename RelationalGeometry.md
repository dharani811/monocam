Back to [MonoCAMArchitecture](MonoCAMArchitecture.md)

Relational geometry (RGEO) is a nice object-oriented idea for creating a parametric CAD. My own experience with it comes from MultiSurf, a product by [Aerohydro Inc](http://www.aerohydro.com/).

A paper describing this approach can be found here: http://dx.doi.org/10.1016/0010-4485(95)00003-8
"Relationalnext term geometric synthesis: Part 1—framework"
John S Letcher, Jr , D Michael Shook and Simon G Shepherd
Computer-Aided Design
Volume 27, [Issue 11](https://code.google.com/p/monocam/issues/detail?id=11), November 1995, Pages 821-832

A patent here: http://www.patentstorm.us/patents/5627949-fulltext.html

# Introduction #
The central idea is that of **support** and **dependence**.

The most basic object, an **AbsPoint** is not supported by anything (except the coordinate system), but all other objects are supported somehow by other objects.

For example a line requires a start-point and an end-point. If name the start point p1, the end point p2, and the line L1, then we say that L1 is supported by p1 and p2. And we say that L1 is a dependecy of p1/p2.

This directly leads to parametric CAD behaviour. If p1 or p2 is moved for some reason, L1 should get an update signal and update its own position accordingly.

There are also some special rules for creating and deleting objects. Obviously we can't create line L1 before we have the endpoints - otherwise we could not find where the line should begin and end. Another effect is that once L1 is created, we cannot delete either p1 or p2 while L1 is alive. Deleting p1 or p2 would leave L1 without supports - an impossible situation for L1. So deleting and object with dependencies should throw an error message to the user.

The neat thing about this is that when for example a **Point** is required as a support for a line, then any **Point** object will do. So the line could have a bead or a magnet as supports too.

# RGEO Objects #
All objects share some common features:
  * Unique name
  * Layer number
  * Shown/Hidden
## Special objects ##
  * Coordinate systems, aka Frames
    * Possibly translated and rotated Frames in the future?
    * Cylindrical or spherical coordinate systems?
  * Planes
    * XPlane, YPlane, ZPlane
    * 3PointPlane
    * NormalPlane
## Points ##
Points are '0D' objects, that when queried 'where are you?' should return a single position (x, y, z).

  * AbsPoint
    * Supported by: coordinate triplet (x,y,z)
    * Equation: none
    * Description: A point in space at a user specified (x,y,z) location
  * RelPoint
    * Supported by: another **Point** p
    * equation: p + dp   where dp = (dx,dy,dz) is given by the user
    * Description: a relative point. start at point p and move by (dx,dy,dz) to get to this

Points that lie on a Curve are called **Beads**. They are located on the curve by calling the support curve with a certain t-parameter.
  * AbsBead
    * Support: a Curve
    * Equation: curve(t) where t is a given parameter, usually [0,1]
    * Description: a point on the curve at parameter value t
  * RelBead
    * Support: a Bead and a Curve (the bead must lie on curve)
    * Equation: curve(t+dt) where t is the t-value of the supporting bead, and dt is given by used
    * Description: a bead offset by an amount dt from another bead
  * ArcLenBead
    * Support: Bead or Curve, ds
    * Description: a bead located at arc-length ds from the supporting bead or from the start of the curve.
  * CopyBead
    * Support: a Bead and a Curve (bead need not lie on curve)
    * Description: a bead located at the same t-value as another bead

Points that lie on surfaces are called **Magnets**. They are located by calling the supporting surface's p(u,v) function with parameters (u,v)
  * AbsMagnet
    * Support: Surface, parameter pair (u,v)
    * Description: point at position (u,v) on the surface
  * RelMagnet
  * CopyMagnet

Points that lie on **Snakes** are called **Rings**. All the usual types exist:
  * AbsRing
  * RelRing
  * CopyRing

## Curves ##
Curves are '1D' objects that extend from parameter value t=0 to parameter value t=1. They should have a method for outputting a point that lies on the curve at a give t-value (sometimes t<0 and t>1 is allowed too!)

  * LineCurve
    * Support: two Points, start and endpoint
    * equation: `p1 + t*(p2-p1)`
    * description: a line from point p1 to point p2
  * ArcCurve
    * Support: three Points
    * equation: depends on type
    * description: arc or circle, about 6 different types exist
  * PolyCurve
    * Support: one or more Curves: c1, c2, c3, ...cn
    * description: composite curve starts at c1 and goes to end of cn
  * SubCurve
    * Support: one Curve c and two Beads b1 and b2
    * description: the part of Curve c that lies between beads b1 and b2)
  * SplineCurve
    * Support: three or more Points
    * description: spline with some equation depending on points

Curves that lie on surfaces are called **Snakes**. They have magnets as their supports, and they live in the (u,v) coordinate system of the supporting surface.
  * LineSnake
  * ArcSnake
  * PolySnake
  * EdgeSnake
  * SubSnake
  * SplineSnake
  * etc


## Surfaces ##
Surfaces are '2D' objects parametrized by two variables u=[0,1] and v=[0,1]. Each surface must have a function to return a point on the surface when given (u,v)

  * RuledSurf
    * Supports: two Curves
    * Description: surface made by connecting two master curves with lines
  * SubSurf
    * Supports: one Surface, 2 or 4 snakes
    * Description: the part of Surface s bounded by the snakes
  * etc.