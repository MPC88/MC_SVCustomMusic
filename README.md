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
  
Install  
=======  
1. Install BepInEx - https://docs.bepinex.dev/articles/user_guide/installation/index.html Stable version 5.4.21 x86.  
2. Run the game at least once to initialise BepInEx and quit.  
3. Download latest mod release.  
4. Place MC_SVCustomMusic.dll in .\SteamLibrary\steamapps\common\Star Valor\BepInEx\plugins\
   
5. Optional: Download MCSVCustomMusic.zip and extract the contents to .\SteamLibrary\steamapps\common\Star Valor\BepInEx\plugins\MCSVCustomMusic\

Configuration  
=============
After first run, a new file mc.starvalor.custommusic.cfg will be created in the .\Star Valor\BepInex\config\ folder.

Replace originals - If true, original music tracks will be replace if any files (even invalid) exist in the category folder.  If false, new tracks are added to the original pool.    
Randomise next track - If true, the next track played will be random.  If false, the default sequential loop through will be used.
  
Credits  
=======  
All tracks in the .zip are from https://pixabay.com/ converted from .mp3 to .ogg.
  
Battle1.ogg - Sci-Fi Cyberpunk Trailer, Stringer_Bell (trimmed)  
Battle2.ogg - Digital Adrenaline (Background, Vlog Music), Top-Flow-Production (trimmed)  
Battle3.ogg - Energy Zoom, Keyframe_Audio (trimmed)  
Battle4.ogg - Exit Dirty, Keyframe_Audio (trimmed)  
  
Tense1.ogg - Space Ambient Sci-Fi, Lexin_Music  
Tense2.ogg - Corporate Power (Suspense Sci-Fi Background), soundbay  
Tense3.ogg - Dark Room, Keyframe_Audio  
Tense4.ogg - Darkness, Defekt_Maschine  
Tense5.ogg - Ambient Suspense Atmosphere, Stringer_Bell  
Tense6.ogg - Mysterious Pulsing Synths - Draco, Keyframe_Audio  
  
Chill1.ogg - Cinematic Cello, Lexin_Music  
Chill2.ogg - Interstellar Space Travel, Lexin_Music  
Chill3.ogg - Space trip, Playsound  
Chill4.ogg - Space, The_Mountain  
Chill5.ogg - Futuristic Sci Fi Cinematic, AudioCoffee  
Chill6.ogg - Epic Technology, www_lokhmatovmusic_com  
