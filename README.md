![Image](https://github.com/user-attachments/assets/792d72bf-0167-4ec0-9ff6-ca0441b78bd3)

## **Introduction**

Our program aims to help its users practice drawing while minimizing executive dysfunction. Quick sketches and messy drawings can be done in our program with no pressure to create completed artworks. Find reference images quickly using our built-in reference image search capabilities or use our built-in exercises to practice essential drawing skills.

## **Installation**

Release 1.0 contains an installer ```setup.exe```, after installed the program can be initialized by running the ```Sketchpop.application``` in the same folder.

## **Usage**

The program starts off ready to draw, like most drawing programs. Tips can be toggled for certain features, but here is an explanation of some of our other features:

Menu bar

- **Exercises**: select your desired exercise here. For more info, see Exercises section below.
- **About**
- Sign up / Login: bring up the Sign up / Login page (logging in doesn’t have any current functionality, and login info isn’t yet encrypted).
- Toggle Tips: toggle the hover over tips that display helpful info on some of our controls.
- **File**: save / load files of your drawings
- **Edit**: Undo / Redo your last drawn actions
- Shortcuts: Ctrl + Z for Undo, Ctrl + Shift + Z for Redo

Exercises

- **Muscle Memory**:
  - Repeated Circles Practice: This exercise is for practicing / building the muscle memory for drawing circles. Draw as many identical circles as you can in between parallel straight lines. Brings up options for line spacing / angle for a variety of practice
- **Red Lining**:
  - This exercise focuses on breaking a form down to its most basic shapes. This helps the user practice the fundamentals of drawing, composition, and proportions.
- **Random Prompt**:
  - Stuck trying to figure out what to draw? This exercise gives you a random prompt so you don’t need to waste time figuring out what to draw.
- **Values**:
- This exercise requires the user to be limited to 5 colors, a range of gray-scale colors to be more specific, which they must use to block out the colors of a monochrome image. This exercise helps the user focus on composition, and to enhance their skills in shading, contrast and tonal variation.

Main drawing tool bar

- **+/-** : zoom in or out on your drawing. You can also use the scroll wheel to zoom in / out.
- **Pen** / Paint brush: “Pen” is the default drawing tool. Select the dropdown arrow next to it and select “Paint Brush” for a textured paintbrush draw tool.
- **Eraser**: Select to switch to the eraser tool
- **Hand**: The hand tool is used for moving the canvas around, useful for if you have resized the window, canvas, or zoomed in or out. The shortcut for this is holding down the middle mouse button.
- **Rect Select / Lasso Select**: Select an area (either a rectangle or a lasso). Drawing is restricted to inside your selection. Press Escape or click with one of the select tools selected without dragging to get rid of the selection
- **Cut / Copy**: You can also copy/cut your selection to the system clipboard with Ctrl + C / Ctrl + X
- **Paste**: You can paste any image data in the system clipboard with Ctrl + V. It’ll paste in the currently selected layer, and you can move the pasted content around by dragging it. Press Esc or select another tool to finish pasting
- **Fill tool**: the fill tool replaces all of a similarly colored area with your selected color instead.
- **Pen Pressure**: Toggle pen pressure functionality. If you have a drawing tablet plugged in with pen pressure functionality, this will make your Pen tool draw smaller / bigger based on how hard you press your pen on the tablet (in addition to the stroke size. Make sure Windows Ink is enabled for your tablet for this feature to work.
- **Clear**: Reset canvas to the default state.

Left Pane

- **Search for images**: Search for reference images by clicking the “Browse” button. A search form will open and you can then enter a search query into the search bar. You may also favorite images, and upload your own images locally to use for drawing references!
- **Color options**: You can set your color either with the individual Red Green Blue values, the predefined color palette, or click “Set Color” for more color palette options
- **Stroke Size**: How large the Pen / Eraser / Paint Brush stroke size is
- **Width / Height:** The image dimensions of the canvas. Hit “resize canvas” to clear your drawing and set the dimensions

Right Pane

- **Preview Panel**: The Preview Panel in the top right shows a zoomed out view of the whole canvas for reference
- **Exercise Controls**:
  - Exercise-specific controls will appear in this panel.
- **Layers**:
- **+ / -** : add a layer to the top of the stack / remove the top layer.
- Layers are a common way in drawing programs to keep separate parts of a drawing separated. Layers on top will display on top of layers below, and your edits will only happen on the layer you selected
- Each layer has an individual opacity slider: you can make layers partially or completely transparent if desired.
- There is an unselectable bottom layer for displaying the default background.

## **Technologies**

- C# (.NET 4.7.2)
- AWS + MySql
- Visual Studio
- SkiaSharp
- Windows Forms Application
  - (Windows exclusive)
- *Unsplash* API

## **Contributors**

Adrian Flores, Kaden Jones, Kang Zhao, Parker Gordon

## **License**

University of Utah, Kalhert School of Computing
