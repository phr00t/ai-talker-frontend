I loved KoboldCpp so much, I decided to make a Windows .NET frontend for it.

![image](https://github.com/user-attachments/assets/fd50a646-4d55-44ad-aed3-0cd73bb31b06)

0. Uses your KoboldCpp config files to load models
1. You can import Character Card PNGs, Kobold stories and character JSON files (via Add/Edit Char)
2. Connects to ComfyUI to handle many important features that you can use custom workflows for (voice cloning, image generation, voice generation, speech recognition)
3. Comes with some ComfyUI workflows: Chroma and SDXL image generation, Whisper speech recognition, Parler TTS for voice generation, F5-TTS voice cloning and VRAM freeing
4. Simple interface for taking and sending pictures (webcam, pasting or loading from file)
5. Handles opening and closing KoboldCpp to free up VRAM for image generation (it set in options, useful for bigger models like Chroma)
6. Automatic image generation options (per response or continuously when not generating text)
7. Can handle sending partial text responses to ComfyUI for more responsive voice generation
8. "Group Chat" feature where you can share a discussion with multiple characters
9. "Auto Prompt" feature where characters can respond even if you don't (useful for "Group Chat" where characters will talk amongst themselves when you don't)
10. Handles switching between text models to vision models, so you can "send images" to models that don't support image input (see under Picture Config)
11. Unique method of handling "long term memory" and recalling information that'd normally be "out of context"
12. Option to maximize available context to stuff as much "long term memory" as possible that is relevant to the discussion along with a chat log
13. Includes "jailbreaking" prompt language
14. Takes up less memory/VRAM than opening up a whole web browser

![image](https://github.com/user-attachments/assets/bc27383c-5194-4cdf-bd13-dad8dae1be3e)

![image](https://github.com/user-attachments/assets/8412a9b8-ef85-47ca-b7b5-bca843708262)

![image](https://github.com/user-attachments/assets/4e100e89-0646-450e-994e-40fe68c15af9)

![image](https://github.com/user-attachments/assets/d5ecaef5-807f-443b-a769-5e7aec32fdc7)

CC BY-NC-SA 4.0
Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International
https://creativecommons.org/licenses/by-nc-sa/4.0/
