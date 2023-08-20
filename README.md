# MC_SVCustomMusic
  
Backup your save before using any mods.  
  
Uninstall any mods and attempt to replicate issues before reporting any suspected base game bugs on official channels.  
  
Function  
========  
Add custom music tracks.  
  
After first run, the following folders will be created in your BepInEx plugins folder:  
.\plugins\MCSVCustomMusic\Battle\  
.\plugins\MCSVCustomMusic\Chill\  
.\plugins\MCSVCustomMusic\Tense\  
.\plugins\MCSVCustomMusic\MainMenu\  
  
Add your custom tracks to the relevant folder(s) to include it in the pool the game loads from.  

Check the following page for formats supported by Unity: https://docs.unity3d.com/Manual/AudioFiles.html  
  
Note that the tracks will only be added (or originals replaced) once all tracks of a particular category have been loaded by the mod.  
  
This mod has no music tracks included.  
  
Install  
=======  
1. Install BepInEx - https://docs.bepinex.dev/articles/user_guide/installation/index.html Stable version 5.4.21 x86.  
2. Run the game at least once to initialise BepInEx and quit.  
3. Download latest mod release.  
4. Place MC_SVCustomMusic.dll in .\SteamLibrary\steamapps\common\Star Valor\BepInEx\plugins\  

Configuration  
=============
After first run, a new file mc.starvalor.custommusic.cfg will be created in the .\Star Valor\BepInex\config\ folder.

Replace originals - If true, original music tracks will be replace if any files (even invalid) exist in the category folder.  If false, new tracks are added to the original pool.    
Randomise next track - If true, the next track played will be random.  If false, the default sequential loop through will be used.
