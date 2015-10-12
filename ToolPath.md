back to [MonoCAMArchitecture](MonoCAMArchitecture.md)

Tool paths are in principle represented as normal geometry objects, but they have a few special features. They are created through a CAM operation. They are not edited through the normal CAD interface, but rather through the operations manager.

# Containers #
There should be a container class for all machining operations, **CAMOperations**. This container can contain one or many **Operation** containers. Each Operation container has objects for that particular operation(see below).
  * CAMOperations
    * Operation1 (for example roughing)
      * reference to the geometry used as input
      * Tool Number
      * Machining parameters (feed plane, feedrate, spindle rpm, tolerance etc.)
      * ToolPath (object containing the actual geometry of the operation, see below)
    * Operation2 (for example finishing)
      * etc.

## Communication with GUI ##
All of the above objects should be 'smart' so that communication with the GUI is made simple. The GUI could have a tree-like structure for showing operations. For drawing the tree the GUI would first ask CAMOperations what children it has. Then for each Operation it would ask what parameters it has. These can then be displayed in an operations manager.

This is a similar idea to the one with editing properties of geometry objects.

# ToolPaths #
The container can contain the following objects inside it. As we are targeting current machine tools that almost exclusively use G-Code (RS274) as the control code this representation will somewhat mirror what is available in G-code.
  * preamble (some standard text or G-code defined in the post-processor)
  * tool number/diameter offset/length offset
  * tool-change
  * spindle on/off, spindle rpm
  * coolant/mist on/off
  * pause
  * moves (user-selectable rendering colors for each of these)
    * linear rapid
    * linear feed
    * arc feed
  * program end (also defined in post-processor)

for each of these ToolPath objects the post-processor must know how to translate them into the appropriate G-code block.