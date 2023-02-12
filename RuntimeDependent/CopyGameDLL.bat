@echo off
rem this script copy game dll to here for project references
set gamepath="D:\Games\Steam\steamapps\common\BLEACH Brave Souls"
set /p gamepath="Set Game Main Path:"
echo Game Path : %gamepath%
xcopy %gamepath%\BleachBraveSouls_Data\Managed %~dp0Managed /s /e /i /y