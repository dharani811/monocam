Back to [MonoCAMArchitecture](MonoCAMArchitecture.md)

## CAM ##
One way of dividing these algorithms is into 1D/2D/3D:
1D Toolpaths

[Notes on CAM architecture](http://www.freesteel.co.uk/wpblog/2006/06/the-structure-of-a-cam-system/)

### 1D toolpaths ###
1D TPs are based on points. Typically you would specify a point e.g. where you want to drill a hole. This algorithm is very simple: (1) in the clearance plane, rapid to the (x,y) point, (2) rapid down to the feed plane, (3) feed down to the point.

A first stupid version of the algorithm could just machine the points in the order they were given to the algorithm. A smarter version would use one of the many TravelingSalesman algorithms for improving the order of the points. Solving the [TSP](http://en.wikipedia.org/wiki/Travelling_salesman) should probably be done with a different algorithm depending on the problem size.

For small sets (<10 or so) you can calculate all routes by brute-force. For medium-sized problems a number of heuristic approaches exist (nearest neighbour, spanning trees, etc.) For really large sets some general purpose optimization algorithm is probably more suitable. David MacNab’s pygene module that implements a genetic algorithm might be a good starting point. Simulated annealing is another option.

These general purpose optimization algorithms could also be used for sorting more advanced operations, along the lines discussed [here](http://www.freesteel.co.uk/wpblog/2006/01/intelligent-machining-algorithms/).

### 2D toolpaths ###
2D toolpahts are based on planar two-dimensional curves. Some CAM programs treat all curves as polylines (smooth curves are approximated by many small line segments), but since all cnc machines use G2/3 moves for circular arcs, I think it makes sense to have two 2D-curve primitives: linear and arc.

A toolpath for exactly following a given input geometry (e.g. for laser cutting) is trivial. The challenge is in creating robust offsets (an offset curve is shifted, along the curve normal, from the part by half of the cutter diameter, so that the cutter edge cuts exactly the part). There’s a recent paper by Liu et al. which might be just what is required.

On a mill, the tool shape in the (x,y) plane is always a circle, so a constant offset can be applied for all planar curves, but something more advanced might be needed for lathe toolpahts when the cutter might not be symmetric.

Once the offset curve is obtained, it should be fairly straightforward to create the typical simple roughing/clearing paths:

  * zig / zigzag ([example](http://www.cosy.sbg.ac.at/~held/projects/pocket_zig/28_zig.gif) from Martin Held)
  * spiral (use a ‘winding-angle sweep line’)
  * contour offset ([example](http://www.cosy.sbg.ac.at/~held/projects/pocket_off/38_off.gif) from Martin Held)
  * morph or blend toolpaths could be created as described by [Bieterman](http://www.ima.umn.edu/industrial/2000-2001/bieterman/bieterman.pdf).

Some post-processing of a 2D toolpath may include line-simplification with the Douglas-Peucker algorithm, and an arc-filter to find G2/G3 moves among many short linear segments (haven’t found a reference for that yet).

[Dragomatz and Mann](http://www.cgl.uwaterloo.ca/~smann/Papers/survey2.pdf) reviewed toolpath algorithms in 1997.

### 3D toolpaths ###
If you believe the freesteel guys (and you probably should, they are pros on CAM algorithms), 3D toolpaths should be based strictly on triangulated surfaces only. CAD programs like to represent surfaces in parametric form, but they should be tessellated into triangles for the CAM algorithm.

#### Roughing ####
Simple roughing toolpaths could be created by slicing the model with a z-plane to obtain a 2D contour at that z-value. Then use one of the 2D algorithms to clear this contour. This is possibly valid only for cylindrical cutters, so may be of less use with toroidal or ballnose cutters.

<need to find a reference on how to calculate z-slice from a set of triangles!>

#### Finishing ####
A simple approach is to create a 2D pattern in the clearance plane using one of the 2D algorithms (like zigzag), then sample points along this toolpath at some (high) sample rate, and for each (x,y) point use a DropCutter algorithm to drop down the cutter so it touches the model.

See some [notes on determining the machining sample rate](http://www.freesteel.co.uk/wpblog/2006/04/sample-rate-from-machining-tolerance/).

This also requires a cutter definition. A simple notation is C(d,r) where d is the shaft diameter of the cutter, and r is the corner radius. Thus C(10,0) is a d=10 cylindical endmill, C(10,5) is a spherical or ballnose endmill, and C(10,2) is a filleted (or bullnose/toroidal) cutter. A development of this is to include [tapered cutters](http://www.freesteel.co.uk/wpblog/2007/02/one-tapered-tool-diagram-coming-up/), but I’m not sure how common those are in the cnc industry.

Besides the freesteel blog, some papers where this approach is discussed are: [Chuang2004](http://dx.doi.org/10.1016/j.cad.2004.10.005) and [Chuang2002](http://www.springerlink.com/content/d6nf8ybb0d3p6k5m/).