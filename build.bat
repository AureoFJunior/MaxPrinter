:inicio
@echo off
set PROJETO=MaxPrinter
cls
echo. ---------------------------
echo  Assistente de compilacao v1.0
echo  Testado apenas com o .NET 5.0
echo. ---------------------------
echo  1  - [Win32] Compilar para um unico arquivo executavel (Requer Framework)
echo  2  - [Win32] Compilar para um unico arquivo executavel (Requer Framework) (Com arquivos de configuracao junto)
echo  3  - [Win32] Compilar para um unico arquivo executavel (Independente)
echo  11 - [Win64] Compilar para um unico arquivo executavel (Requer Framework)
echo  12 - [Win64] Compilar para um unico arquivo executavel (Requer Framework) (Com arquivos de configuracao junto)
echo  13 - [Win64] Compilar para um unico arquivo executavel (Independente)
echo  0 - SAIR
echo. ---------------------------

set /p Comando= Digite uma Opcao :
if "%Comando%" equ "1" (goto:op1)
if "%Comando%" equ "2" (goto:op2)
if "%Comando%" equ "3" (goto:op3)
if "%Comando%" equ "11" (goto:op11)
if "%Comando%" equ "12" (goto:op12)
if "%Comando%" equ "13" (goto:op13)
if "%Comando%" equ "0" (goto:exit)

:erro
cls
goto:inicio


:op1
dotnet publish %PROJETO% -r win-x86 --self-contained=false /p:PublishSingleFile=true 
pause
goto:inicio

:op2
dotnet publish %PROJETO% -r win-x86 --self-contained=false /p:PublishSingleFile=true /p:IncludeAllContentForSelfExtract=true
pause
goto:inicio

:op3
dotnet publish %PROJETO% -r win-x86 --self-contained=true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true 
pause
goto:inicio


:op11
dotnet publish %PROJETO% -r win-x64 --self-contained=false /p:PublishSingleFile=true 
pause
goto:inicio

:op12
dotnet publish %PROJETO% -r win-x64 --self-contained=false /p:PublishSingleFile=true /p:IncludeAllContentForSelfExtract=true
pause
goto:inicio

:op13
dotnet publish %PROJETO% -r win-x64 --self-contained=true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true 
pause
goto:inicio



:exit
exit