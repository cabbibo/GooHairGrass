# Goo Hair Grass

![WeirdGif1](https://media.giphy.com/media/3o7bueYS5NtpOdfBYc/giphy.gif)

Hey all! 

I've been working on some helpful lil scripts for you to make weird physical stuff using compute shaders in [Unity] ! With it you can turn models into goo, grow hair on the models, or grow grass on the models even ( well by grass I mean weird rainbow tubes, but whatever! )

Its my first attempt at creating any sort of archeticture for compute shaders, and also my first attempt at making any sort of archeticture in unity, so please know that it could be totally wrong and silly!  That being said, lets look at how to use it!


# 3 Scripts


There are 3 specific scripts ( so far ) that you can add :
  - Gooify
  - Hairify
  - Grassify
 
Lets look what they all need to have first:

1) A Human Buffer
2) A Base Physics Shader
3) A Gather Shader
4) A Base Renderer

Although the human buffer gets a bit more complex ( We'll go into this in a bit ) The rest of these are just to make the mesh into a compute shader that we can do strange things too. Basically, all of these scripts are to help us turn CPU info into GPU info and pass that GPU info back and forth. 

I still don't completely understand what compute shaders are, so its a better bet for you to just google 'Unity Compute Shader Tutorial', but with this code it looks a bit like this:

Mesh --> Vert Buffer ( list of verts w/ uvs , normals, positions, etc. ) & Triangle Buffer ( list of triangles with dif ids ) 

We can then just straight up render this vert buffer by looking up in the triangle buffer ( making it no different from using a regular mesh ), 
But that wouldn't make it very interesting!

Thus in Gooify, it goes:

Mesh -> vert buffer / tri buffer -> Physics loop which updates the vert buffer -> render vert buffer

Hairify builds off this base and looks like: 
Mesh -> vert buffer / tri buffer / hair buffer -> vert buffer physics loop -> hair buffer physics loop ( which uses vert buffer ) -> render vert buffer and hair buffer

Grassify is just this plus one extra render step to render the grass itself!

# Human Buffer

All of the scripts use a 'human buffer' which basically takes the information of the player and passes it into a compute buffer we can use when we update the physics. This requires having 'Human Info' added to some place in the Hierarchy which takes in 2 hand infos ( added to steam controllers ) and a head info ( added to steam camera ). Check out the already created scenes to see how this works




### Todos

 - Make more shaders!
 - Learn how compute shaders work!

### THANKS

 - Morten Mikkelson for helping me learn about Compute Shaders
 - George Michael Bower for the delightful models!
 - Yağmur Uyanık for sounds!

License
----
Copyright (c)  2017  Isaac Cohen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.



[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [UNITY]: <https://unity3d.com/>

