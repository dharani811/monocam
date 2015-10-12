# Introduction #

The TravelingSalesman problem occurs at least in the following CAM operations
  * drill-type paths. in which sequence do we drill a complex number of holes?
  * contour-paths. if we have many parts to contour (cutting 2D parts with a laser or similar) then in what order should we cut the parts
    * this is actually a slightly modified version of the TSP since as we might enter each contour (city) at a different position than what we exit. So the graph of cities is asymmetric in some sense.

# Brute Force #
this is only possible for very smalls sets
# Genetic Algorithm #
There's a C# implementation of a genetic algorithm for the TSP here http://www.lalena.com/AI/Tsp/
It also includes a nice web-demo! (IE required)
# simulated annealing #