@echo off
del %systemroot%\mt.txt
echo {tr} {td} {img src='../../Recursos/imagenes/generales/computer.png' alt='NombrePC'} {.td} {td} {b}Nombre Equipo:{.b} {.td} {td} %computername% {.td} {.tr} > %systemroot%\mt.txt
echo {tr} {td} {img src='../../Recursos/imagenes/generales/apps-folder.png' alt='Aplicaciones'} {.td} {td} {b}Programas:{.b} {.td} {td} %programfiles% {.td} {.tr} >> %systemroot%\mt.txt
echo {tr} {td} {img src='../../Recursos/imagenes/generales/hdd.png' alt='Disco duro'} {.td} {td} {b}Disco Principal:{.b} {.td} {td} %systemdrive% {.td} {.tr} >> %systemroot%\mt.txt
echo {tr} {td} {img src='../../Recursos/imagenes/generales/system-folder.png' alt='Carpeta Sistema'} {.td} {td} {b}Carpeta Sistema:{.b} {.td} {td} %windir% {.td} {.tr} >> %systemroot%\mt.txt
echo {tr} {td} {img src='../../Recursos/imagenes/generales/temp-folder.png' alt='Carpeta Temporal'} {.td} {td} {b}Carpeta Temporal:{.b} {.td} {td} %temp% {.td} {.tr} >> %systemroot%\mt.txt
echo {tr} {td} {img src='../../Recursos/imagenes/generales/user-folder.png' alt='Carpeta Usuario'} {.td} {td} {b}Carpeta Usuario:{.b} {.td} {td} %userprofile% {.td} {.tr} >> %systemroot%\mt.txt
echo {tr} {td} {img src='../../Recursos/imagenes/generales/system-time.png' alt='Hora Sistema'} {.td} {td} {b}Hora Sistema:{.b} {.td} {td} %time% {.td} {.tr} >> %systemroot%\mt.txt
@echo on