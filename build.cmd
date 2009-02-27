msbuild Source\Compete.sln

copy Source\Compete.Bot\bin\Debug\Compete.Bot.dll ..\rock_paper_scissors_pro\PackageSource\Libraries
copy Source\Compete.Uploader\bin\Debug\Compete.Uploader.exe ..\rock_paper_scissors_pro\PackageSource\Tools
copy ..\rock_paper_scissors_pro\Source\RockPaperScissorsPro\bin\Debug\RockPaperScissorsPro.dll ..\rock_paper_scissors_pro\PackageSource\Libraries
