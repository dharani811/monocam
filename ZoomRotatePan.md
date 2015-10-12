back to [GUI](GUI.md)

We are locating the camera with the gluLookAt function:
> gluLookAt(cam.x , cam.y, cam.z, center.x, center.y, center.z, up.x, up.y, up.z)
this positions the camera at (cam.x , cam.y, cam.z) and it is pointed to look at the point (center.x, center.y, center.z). It is oriented so that camera up is in the direction (up.x, up.y, up.z)

It makes sense to internally represent the position of the camera in [spherical coordinates](http://mathworld.wolfram.com/SphericalCoordinates.html)
> (r, theta, fi)
The transformation from spherical coords to cartesian is:
```
 cam.x = r * cos(theta) * sin(fi)
 cam.y = r * sin(theta) * sin(fi)
 cam.z = r * cos(fi)
```

if we want an upright view of our world then it probably makes sense to always have the **up** vector calculated to orient the camera upwards. If I remember correctly the result is quite horrible if up is constant
```
up=(0,0,1)
```

so, we need to always orient up towards polar north pole. I don't recall the exact description of this, but it works :)
```
u = (0,0,1)
v1 = u x cam
v1 = v1 / sqrt(v1.v1)
v2 = cam x v1
v2 = v2 / sqrt(v2.v2)
up = (u.v1)*v1 + (u.v2)*v2
up = up / sqrt(up.up)
```

here the dot (.) is used for vector dot-product and the cross (x) is used for vector cross product. star is normal multiplication.


# zoom #
zoom is very easy, you just change r.

my suggestion is that this is done with the mouse-wheel.

for the up vector calculation above to work **it is important that r does not go to zero!**
# rotate #
this is also fairly easy.

to rotate side-to-side you change theta

to rotate up-down you change fi. it might be a good idea to restrict fi to -pi/2<fi<pi/2. try it out and see how it feels.

# pan #
we want to pan in the plane which is perpendicular to the vector from the camera to center.

There are two panning directions left-right (lr) and up-down (ud).

**Note** these calculations are the same for dragging. When the user drags a point or a geometry object it should move in the same plane as panning is done.

The camera up vector directly gives us the up-down direction. The other direction is perpendicular to the vector from the camera to center and up.
```
ud = up
lr = (cam-center) x up
```
to get consistent panning we might want to normalize these
```
ud = ud / sqrt(ud.ud)
lr = lr / sqrt(lr.lr)
```
then panning is just moving center by some amount in one of these directions.

left-right panning:
```
center = center + alfa * lr
```
where alfa is an adjustable parameter

up-down panning:
```
center = center + alfa * ud
```