### Introduction

This repository contains code for an image scaling tool implemented in C# using bilinear interpolation. 

Bilinear interpolation is a technique used to resize images while preserving image quality by interpolating pixel values based on surrounding pixels.

### Problem Statement

The main encountered challenge was dealing with concurrency issues while performing image resizing operations in parallel. The `System.InvalidOperationException: 'Object is currently in use elsewhere.'` error occurred when attempting to access the `Height` property of the original bitmap within a parallel loop.

### Solution Overview
1.  **Isolation of Image Processing**: Separated image processing operations from UI updates, ensuring that any modifications to the image were performed sequentially.
    
2.  **Pre-Extraction of Image Dimensions**: Extracted the width and height of the original image outside of the parallel loop to minimize concurrent access to the bitmap object within the loop.
    
3.  **Thread-Safe UI Updates**: Updated the UI components (e.g., PictureBox) in a thread-safe manner, ensuring that UI updates occurred on the main UI thread.
    

### Resources Used

*   https://medium.com/@prequelapp/image-downscaling-maximize-minimization-63207d517f13
* https://efundies.com/scale-an-image-in-c-sharp-preserving-aspect-ratio/
* https://stackoverflow.com/questions/1851292/invalidoperationexception-object-is-currently-in-use-elsewhere
* https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-write-a-simple-parallel-for-loop
* https://dotnettutorials.net/lesson/parallel-for-method-csharp/
