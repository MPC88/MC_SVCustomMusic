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
  
Battle1.mp3 - Sci-Fi Cyberpunk Trailer, Stringer_Bell (trimmed)  
Battle2.mp3 - Digital Adrenaline (Background, Vlog Music), Top-Flow-Production (trimmed)  
Battle3.mp3 - Energy Zoom, Keyframe_Audio (trimmed)  
Battle4.mp3 - Exit Dirty, Keyframe_Audio (trimmed)  
  
Tense1.mp3 - Space Ambient Sci-Fi, Lexin_Music  
Tense2.mp3 - Corporate Power (Suspense Sci-Fi Background), soundbay  
Tense3.mp3 - Dark Room, Keyframe_Audio  
Tense4.mp3 - Darkness, Defekt_Maschine  
Tense5.mp3 - Ambient Suspense Atmosphere, Stringer_Bell  
Tense6.mp3 - Mysterious Pulsing Synths - Draco, Keyframe_Audio  
   
Chill1.mp3 - Cinematic Cello, Lexin_Music  
Chill2.mp3 - Interstellar Space Travel, Lexin_Music  
Chill3.mp3 - Space trip, Playsound  
Chill4.mp3 - Space, The_Mountain  
Chill5.mp3 - Futuristic Sci Fi Cinematic, AudioCoffee  
Chill6.mp3 - Epic Technology, www_lokhmatovmusic_com  
