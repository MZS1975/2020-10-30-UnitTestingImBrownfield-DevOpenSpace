# Benutzung von Windows Appium / Selenium for Windows

Der Windows Application Driver muss installiert sein und als Kommandozeile laufen. Er übernimmt die Ansteuerung des Remote-Programms. Alle Bestandteile der Oberfläche werden als Browser-Elemente interpretiert.

Per 
```
    var element = session.FindElementByAccessibilityId('elementName')
```

wird ein Bildschirmelement gesucht. Hierzu muss es natürlich die passende AccessibilityId bekommen:
```
    <Grid AutomationProperties.AutomationId="elementName"
  ...
```

Das funktioniert sogar bei Styles
```
    <Style x:Key="ListViewItemStyle" TargetType="ListViewItem">
        <Setter Property="AutomationProperties.AutomationId" Value="ListViewItem" />
```
sodass auch Listenelemente gefunden werden können.