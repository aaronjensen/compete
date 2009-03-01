@echo off
del /s /q C:\Compete\Bots\*
del /s /q C:\Compete\Games\*
Mkdir C:\Compete
Mkdir C:\Compete\Bots
Mkdir C:\Compete\Games
rem Copy ..\rock_paper_scissors_pro\Source\RockPaperScissorsPro.AlwaysThrows\bin\Debug\RockPaperScissorsPro.AlwaysThrows.dll C:\Compete\Bots\jacob.dll
rem Copy ..\rock_paper_scissors_pro\Source\RockPaperScissorsPro.AlwaysThrows\bin\Debug\RockPaperScissorsPro.AlwaysThrows.dll C:\Compete\Bots\dan.dll
rem Copy ..\rock_paper_scissors_pro\Source\RockPaperScissorsPro.AlwaysThrows\bin\Debug\RockPaperScissorsPro.AlwaysThrows.dll C:\Compete\Bots\nathan.dll
rem Copy ..\rock_paper_scissors_pro\Source\RockPaperScissorsPro.AlwaysThrows\bin\Debug\RockPaperScissorsPro.AlwaysThrows.dll C:\Compete\Bots\khalil.dll
rem Copy ..\rock_paper_scissors_pro\Source\RockPaperScissorsPro.AlwaysThrows\bin\Debug\RockPaperScissorsPro.AlwaysThrows.dll C:\Compete\Bots\TakeForever.dll
Copy ..\rock_paper_scissors_pro\Source\RockPaperScissorsPro.AlwaysThrows\bin\Debug\RockPaperScissorsPro.dll C:\Compete\Games
Copy ..\rock_paper_scissors_pro\Source\RockPaperScissorsPro.Wrapper\bin\Debug\RockPaperScissorsPro.Wrapper.dll c:\compete\games
