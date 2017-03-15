echo off
sqlcmd -E -S INVASION-PC\KUZYANINM -i %~dp0\structures.sql
sqlcmd -E -S INVASION-PC\KUZYANINM -i %~dp0\createTests.sql
set /p delExit=Press the ENTER key to exit...: