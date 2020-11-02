Hallo!

Hier noch einige ergänzende Hinweise zu meiner Lösung.

Es wird ein wenig überdimensioniert wirken, da es hier nur um Sessions mit zwei Attributen geht. Sobald aber weitere Funktionen dazu kommen, wie z. B. eine automatische Raumplanoptimierung basierend auf Teilnehmerzahl, Raumgröße und Aktivität während der Sessions,
zeigt sich die expressive Stärke in der Kerndomäne nicht mit POCOs zu arbeiten.

## Grundlegende Designentscheidungen

- UI ist Modeabhängig und sollte mit minimalem Aufwand ersetzt werden können. Daher ist diese der eigentlichen Anwendung nur vorgeschaltet. Die Anwendung selbst hat einen eigenen DI Container und wird vorkonfektioniert in die UI integriert.
- Storage ist Implementierungsdetail und daher in einer austauschbaren Assembly audgelagert.
- Die Client/Server Aufteilung ist eine pure Architekturentscheidung und darf sich nicht in der Kerndomäne auswirken. 

## Offene Punkte

- Es gibt eine Race - Condition nach der Eindeutigkeitsprüfung des Titels auf Applikationsebene (Anwenden der UniqueTitle Domain Policy), wo theoretisch von zwei Clients aus der Titel ungünstig geändert werden kann und das erst im Storage festgestellt wird.
  Dies könnte dadurch gelöst werden, dass die eigentlich Applikation vom Client in einen separaten Applikationsserver, wo sich alle Clients den selben Zustand teilen, verlagert wird. Das war mir aber zu aufwändig für eine Übung.  
- Es gibt noch eine Tetunterdeckung in der UI wodurch Fehler im Data Bindung erst im manuellen Test oder Betrieb auffallen. Die passende UI Test Automation war mir aber jetzt zu aufwändig.
- Es gibt keine Applikationstelemetrie zur Runtime - Diagnostic, für einen tatsächlichen Produktivbetrieb wäre das ein No-Go.