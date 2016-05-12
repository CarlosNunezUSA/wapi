@echo off
CLS
XCOPY Schedule\*.* C:\Temp\Hermes\Schedule\*.* /q /e /r /y
XCOPY Jobs\*.* C:\Temp\Hermes\Jobs\*.*  /q /e /r /y