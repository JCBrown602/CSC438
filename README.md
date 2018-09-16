# CSC438 Project

University of Advancing Technology  
CSC438 AI - FALL 2018  
Professor: Tony Hinton  
Student: Jason Brown  
14 September, 2018  
 
REF source code examples from Microsoft:
https://azure.microsoft.com/en-us/services/cognitive-services/face/

# FRAMEWORKS
I was previously unaware that the target .NET frameworks were set automatically, sometimes to
very different, conflicting versions, depending on the project type selected at creation. The
Aesthetics project was from previous work and I mistakenly added new projects to the solution
"Console Applications" instead of "Class Libraries". This helped to complicate the framework
issue for me. 

The following is a list of the frameworks for each project and their test status
as of #[b87dfef0](https://github.com/JCBrown602/CSC438/commit/d5ca917ba38451a7f9727a85ee20f52fa872dc28). I'm planning on leaving things as they are since the Face API executes
successfully. If the untested projects are troublesome, they can be recreated easily or simply
removed.

Project Name | Target Framework | Tested?
------------ | ---------------- | ---------
Aesthetics | .NET Framework 4.5 | YES
DebugTools | .NET Framework 4.5 | YES
FaceDataDisplay | .NET Core 2.1 | NO
FileIO | .NET Core 2.1 | NO
JARVIS (Main App) | .NET Core 2.1 | YES
# CLASSES
## JARVIS

## FaceDataDisplay

## FileIO

## Aesthetics

# IDEAS / TODO
* Add functionality to analyze multiple images at one time. Multithreading?
* Can this scan videos, or an area of the screen?
** Identify faces in a YouTube video
** ID faces in a movie and compare them to faces in a database to ID the actor
 
<center> :octocat: </center>