@echo off
tasklist /fi "imagename eq SlimeRancher.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "SlimeRancher.exe"