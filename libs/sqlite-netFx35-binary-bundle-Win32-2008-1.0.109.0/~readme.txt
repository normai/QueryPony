
   This folder
    - sqlite-netFx35-binary-bundle-Win32-2008-1.0.109.0
   stores the files extracted from file
    - sqlite-netFx35-binary-bundle-Win32-2008-1.0.109.0.zip
   which was downloaded from 
    - http://system.data.sqlite.org/index.html/doc/trunk/www/downloads.wiki
   [as download 20180815°0221]

   So far, the only file used from this folder, is the Interop Assembly
    - System.Data.SQLite.dll (1 344 000 bytes)
   This file was copied from here to
    - /queryponydev/trunk/querypony/QueryPonyGui/libs/
   from where it is incorporated into the executable.

   Note. (1) In the SharpDevelop/VisualStudio solution,
   a reference must be set in the QueryPonyLib project.
   (2) But during runtime, not this reference is use, but
   the library will be programmatically extracted from the
   executable to the Application folder and be called from
   there. (3) Considere this, when updating/exchanging
   System.Data.SQLite.dll (or any other ressource). (4) Then, also
   update file /trunk/querypony/QueryPonyGui/docs/thirdparty.txt.

   ———————————————————————
   [file 20180815°0322] ʘΩ
