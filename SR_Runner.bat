tasklist /fi "imagename eq SlimeRancher.exe" |find ":" &gt; nul
if errorlevel 1 taskkill /f /im "SlimeRancher.exe"

start steam://rungameid/433340