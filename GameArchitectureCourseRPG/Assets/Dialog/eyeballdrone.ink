I've been waiting forever, I'm glad you're finally here.
*   What? Why are you waiting for me?
    -I need you to open that dor for me. #E.ShowGreenDoor
    *   I'll try, but I'm not sure what to do.#E.HideGreenDoor
    -> instructions 
    *   I don't think I want to do that right now #E.HideGreenDoor
        Ahh okay, bye then, thanks for nothing.. -> END
        
    == instructions ==
    
    Go to the panel, it's in the back room. #E.ShowBackRoom
        *   Why can't you do that? 
            I'm too wide.. #Q.InspectThePanel
            **  Okay, I'm on it! #E.HideBackRoom
                Thanks! -> END
- -> END