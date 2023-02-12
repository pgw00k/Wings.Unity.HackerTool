@echo off
rem this script copy game dll to here for project references
set gamepath="D:\Program\Unity_2018.04.26_f1\Editor"
set /p gamepath="Set Game Main Path:"
echo Game Path : %gamepath%
xcopy %gamepath%\Data\Managed %~dp0Editor /s /e /i /y