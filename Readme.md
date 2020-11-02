Hallo!

Hier noch einige erg�nzende Hinweise zu meiner L�sung.

Es wird ein wenig �berdimensioniert wirken, da es hier nur um Sessions mit zwei Attributen geht. Sobald aber weitere Funktionen dazu kommen, wie z. B. eine automatische Raumplanoptimierung basierend auf Teilnehmerzahl, Raumgr��e und Aktivit�t w�hrend der Sessions,
zeigt sich die expressive St�rke in der Kerndom�ne nicht mit POCOs zu arbeiten.

## Grundlegende Designentscheidungen

- UI ist Modeabh�ngig und sollte mit minimalem Aufwand ersetzt werden k�nnen. Daher ist diese der eigentlichen Anwendung nur vorgeschaltet. Die Anwendung selbst hat einen eigenen DI Container und wird vorkonfektioniert in die UI integriert.
- Storage ist Implementierungsdetail und daher in einer austauschbaren Assembly audgelagert.
- Die Client/Server Aufteilung ist eine pure Architekturentscheidung und darf sich nicht in der Kerndom�ne auswirken. 

## Offene Punkte

- Es gibt eine Race - Condition nach der Eindeutigkeitspr�fung des Titels auf Applikationsebene (Anwenden der UniqueTitle Domain Policy), wo theoretisch von zwei Clients aus der Titel ung�nstig ge�ndert werden kann und das erst im Storage festgestellt wird.
  Dies k�nnte dadurch gel�st werden, dass die eigentlich Applikation vom Client in einen separaten Applikationsserver, wo sich alle Clients den selben Zustand teilen, verlagert wird. Das war mir aber zu aufw�ndig f�r eine �bung.  
- Es gibt noch eine Tetunterdeckung in der UI wodurch Fehler im Data Bindung erst im manuellen Test oder Betrieb auffallen. Die passende UI Test Automation war mir aber jetzt zu aufw�ndig.
- Es gibt keine Applikationstelemetrie zur Runtime - Diagnostic, f�r einen tats�chlichen Produktivbetrieb w�re das ein No-Go.