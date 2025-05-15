# Sketchpop

#### Apractice-orienteddrawingprogram.

## Introduction

#### Ourprogramaimstohelpitsuserspracticedrawingwhileminimizingexecutive

#### dysfunction.Quicksketchesandmessydrawingscanbedoneinourprogramwithno

#### pressuretocreatecompletedartworks.Findreferenceimagesquicklyusingourbuilt-in

#### referenceimagesearchcapabilitiesoruseourbuilt-inexercisestopracticeessential

#### drawingskills.

## Technologies

#### ● C#(.NET4.7.2)

#### ● AWS+MySql

#### ● VisualStudio

#### ● SkiaSharp

#### ● WindowsFormsApplication

```
○ (Windowsexclusive)
```
#### ● Unsplash API

## Installation

#### SketchpopisdevelopedinMicrosoftVisualStudio 2022 Community,targetinga4.7.

#### .NETFramework.Assuchitcanbebuiltbyopeningandbuilding(Sketchpop.sln)inthe

#### outermostdirectoryofthisproject.

#### ThemajorityoftheNuGetpackages should auto-install;however,wehadtheissue

#### wheresomehadtobemanuallyinstalled,notablySkiaSharp.

#### ThefollowingNuGetpackagesareinstalledforthissolution:

```
● Google.Protobug
● K4os.Compression.LZ
● K4os.Compression.LZ4.Streams
● K4os.Hash.xxHash
```

```
● MySql.Data
● Newtonsoft.Json
● OpenTK
● OpenTK.GLControl
● SkiaSharp
● SkiaSharp.NativeAssets.macOS
● SkiaSharp.NativeAssets.Win
● SkiaSharp.Views
● SkiaSharp.Views.Desktop.Common
● SkiaSharp.Views.Gtk
● SkiaSharp.Views.WindowsForms
● SkiaSharp.Views.WPF
● System.Buffers
● System.Drawing.Common
● System.IO.Pipelines
● System.Memory
● System.Numerics.Vectors
● System.Runtime.CompilerServices.Unsafe
● System.Threading.Tasks.Extension
```
#### AfterbeingbuiltinVisualStudioitshouldalsobeabletoberunthroughthesame

#### methods.

## Usage

#### Theprogramstartsoffreadytodraw,likemostdrawingprograms.Tipscanbetoggled

#### forcertainfeatures,buthereisanexplanationofsomeofourotherfeatures:

### Menubar

```
● Exercises :selectyourdesiredexercisehere.Formoreinfo,seeExercisessectionbelow.
● About
○ Signup/Login:bringuptheSignup/Loginpage(loggingindoesn’thaveany
currentfunctionality,andlogininfoisn’tyetencrypted).
○ ToggleTips:togglethehoverovertipsthatdisplayhelpfulinfoonsomeofour
controls.
● File :save/loadfilesofyourdrawings
● Edit :Undo/Redoyourlastdrawnactions
○ Shortcuts:Ctrl+ZforUndo,Ctrl+Shift+ZforRedo
```

### Exercises

```
● MuscleMemory :
○ RepeatedCirclesPractice:Thisexerciseisforpracticing/buildingthemuscle
memoryfordrawingcircles.Drawasmanyidenticalcirclesasyoucanin
betweenparallelstraightlines.Bringsupoptionsforlinespacing/anglefora
varietyofpractice
● RedLining :
○ Thisexercisefocusesonbreakingaformdowntoitsmostbasicshapes.This
helpstheuserpracticethefundamentalsofdrawing,composition,and
proportions.
● RandomPrompt :
○ Stucktryingtofigureoutwhattodraw?Thisexercisegivesyouarandomprompt
soyoudon’tneedtowastetimefiguringoutwhattodraw.
● Values :
○ Thisexerciserequirestheusertobelimitedto 5 colors,arangeofgray-scale
colorstobemorespecific,whichtheymustusetoblockoutthecolorsofa
monochromeimage.Thisexercisehelpstheuserfocusoncomposition,andto
enhancetheirskillsinshading,contrastandtonalvariation.
```
### Maindrawingtoolbar

```
● +/- :zoominoroutonyourdrawing.Youcanalsousethescrollwheeltozoomin/out.
● Pen /Paintbrush:“Pen”isthedefaultdrawingtool.Selectthedropdownarrownexttoit
andselect“PaintBrush”foratexturedpaintbrushdrawtool.
● Eraser :Selecttoswitchtotheerasertool
● Hand :Thehandtoolisusedformovingthecanvasaround,usefulforifyouhaveresized
thewindow,canvas,orzoomedinorout.Theshortcutforthisisholdingdownthe
middlemousebutton.
● RectSelect/LassoSelect :Selectanarea(eitherarectangleoralasso).Drawingis
restrictedtoinsideyourselection.PressEscapeorclickwithoneoftheselecttools
selectedwithoutdraggingtogetridoftheselection
○ Cut/Copy :Youcanalsocopy/cutyourselectiontothesystemclipboardwith
Ctrl+C/Ctrl+X
○ Paste :YoucanpasteanyimagedatainthesystemclipboardwithCtrl+V.It’ll
pasteinthecurrentlyselectedlayer,andyoucanmovethepastedcontent
aroundbydraggingit.PressEscorselectanothertooltofinishpasting
● Filltool :thefilltoolreplacesallofasimilarlycoloredareawithyourselectedcolor
instead.
● PenPressure :Togglepenpressurefunctionality.Ifyouhaveadrawingtabletpluggedin
withpenpressurefunctionality,thiswillmakeyourPentooldrawsmaller/biggerbased
onhowhardyoupressyourpenonthetablet(inadditiontothestrokesize.Makesure
WindowsInkisenabledforyourtabletforthisfeaturetowork.
```

```
● Clear :Resetcanvastothedefaultstate.
```
### LeftPane

```
● Searchforimages :Searchforreferenceimagesbyclickingthe“Browse”button.A
searchformwillopenandyoucanthenenterasearchqueryintothesearchbar.You
mayalsofavoriteimages,anduploadyourownimageslocallytousefordrawing
references!
● Coloroptions :YoucansetyourcoloreitherwiththeindividualRedGreenBluevalues,
thepredefinedcolorpalette,orclick“SetColor”formorecolorpaletteoptions
● StrokeSize :HowlargethePen/Eraser/PaintBrushstrokesizeis
● Width/Height: Theimagedimensionsofthecanvas.Hit“resizecanvas”toclearyour
drawingandsetthedimensions
```
### RightPane

```
● PreviewPanel :ThePreviewPanelinthetoprightshowsazoomedoutviewofthewhole
canvasforreference
● ExerciseControls :
○ Exercise-specificcontrolswillappearinthispanel.
● Layers :
○ +/- :addalayertothetopofthestack/removethetoplayer.
○ Layersareacommonwayindrawingprogramstokeepseparatepartsofa
drawingseparated.Layersontopwilldisplayontopoflayersbelow,andyour
editswillonlyhappenonthelayeryouselected
○ Eachlayerhasanindividualopacityslider:youcanmakelayerspartiallyor
completelytransparentifdesired.
○ Thereisanunselectablebottomlayerfordisplayingthedefaultbackground.
```
## Contributors

AdrianFlores,KadenJones,KangZhao,ParkerGordon

## License

#### UniversityofUtah,KalhertSchoolofComputing


