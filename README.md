# Demo WMP Black Frame

Demos using WMP in WPF in various circumstances that result in an initial black frame.

Change the VidePlayerSettings.json file for a specific video file to playback.

Desire is to be able to have a video opened and scrubbed to a position without showing a black frame. Am using in presentation software that transitions between slides and videos. Having a black frame/flash in the middle of a transition is not ideal.

# Playback scenarios

### Simple
- just opens and plays the video
- Observed: black frame -> playing video
- This suggests that I should try to have the video already opened and scrubbed to not have the black frame.

### Quick show
- opens, plays, and makes the video visible at the same time
- Observed: goes from white -> black frame -> plays video
- I believe I need to wait longer before making the player visible, otherwise I still get the initial black frame.

### Delay show
- Opens video, makes it visible after 250ms, and then plays it after 1 second.
- Observed: goes from white -> black frame -> shows video -> plays
- This shows me that waiting while paused still shows a black frame.

### Red rect no delay
- Displays red rectangle and opens video, waits a second, and then removes the red while playing video
- Observed: goes from black frame -> showing video with red rectangle -> plays video with black strip where the red rectangle was -> just playing video
- Showcases that I can still get a black frame strip where the red rectangle used to be. It would be ideal if I could not get the black strip.

### Red rect with delay
- Displays red rectangle and opens video, waits a second, removes the red rectangle, and then plays the video after 250ms
- Observed: goes from black frame -> show video with red rectangle -> removes red rectangle -> plays video
- This is a workaround to not have the black strip, but also not ideal since the video isn't playing immediately when shown.

### Red rect dispatched
- Displays red rectangle and opens video, waits a second, removes the red rectangle, and then plays the video after the main thread circles around.
- Observed: goes from black frame -> showing video with red rectangle -> plays video with black strip where the red rectangle was -> just playing video
- This is similar to the red rect with no delay.

### Red rect CompTarget.Rendering
- Displays red rectangle and opens video, waits a second, removes the red rectangle, and then plays the video after a couple composition rendering passes.
- Observed: goes from black frame -> showing video with red rectangle -> plays video with black strip where the red rectangle was -> just playing video
- This is still similar to the red rect with no delay.

### Show when loaded + CompTarget.Rendering
- Opens the video while hidden, then shows and plays it after a couple composition rendering passes.
- Observed: goes from black frame -> playing video

### Opacity show
- Opens and plays the video, showing it after 250ms.
- Observed: goes from white frame -> playing video
- Seems to be the best workaround, but would prefer to show the video immediately instead of waiting.</value>