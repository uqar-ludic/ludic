ludic
=====

Web-based, gamified, platform to teach programming

Overview
========

This project is composed of different parts:
- A website realised with MVC and EntityFramework in ASP.NET.
- A web service containing a compilation dll and a sandbox dll.
- The compilation dll uses MSbuild to generate the executable file.
- The sandbox dll uses Microsoft Sandboxing solution.

Compilation
===========
The compilation project is based on this tutorial : [link](http://sanganakauthority.blogspot.ca/2014/01/how-to-build-solution-using-c-code-in.html)

Sandbox
=======
The sandbox project is based on this MSDN tutorial : [link](http://msdn.microsoft.com/en-us/library/bb763046(v=vs.110).aspx)

Web service
===========
The web service allow to call the compilation and sandbox's methods. 

Website
=======
