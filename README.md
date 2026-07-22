# Achtung, Baustelle! (Baustellen-Sicherheits-Trainer)

![Gameplay](gp_screenshot.png)

Kleines Serious Game, das ich als Übungsprojekt entwickelt habe, um mich in
Unity und C# einzuarbeiten. Man muss auf einer Baustelle innerhalb von
90 Sekunden 5 Sicherheitsrisiken finden und anklicken. Jeder Fund zeigt eine
kurze Erklärung, warum die Situation gefährlich ist.

**▶ Im Browser spielen (itch.io):** https://maxforty7.itch.io/achtung-baustelle

## Steuerung

- Linksklick: Risiko anklicken
- Rechte Maustaste halten: Kamera drehen
- Mausrad: Zoom

## Features

- Hauptmenü mit Start, Optionen-Hinweis und Beenden
- Zeitlimit von 90 Sekunden mit Punktesystem
- Info-Popup pro gefundenem Risiko (der "Serious"-Teil des Spiels)
- Endbildschirm mit Ergebnis, Neustart und Rückkehr ins Menü

## Technik

- Unity 6, C#
- Szene komplett aus Unity-Primitives gebaut (bewusst ohne externe Assets)
- Risiko-Texte werden über den Inspector gepflegt, nicht im Code
- UI-Input-Icons: Kenney (CC0)

## Aufbau der Skripte

- `GameManager` – Spiellogik: Klick-Erkennung, Timer, Punkte, HUD
- `Hazard` – markiert ein Objekt als anklickbares Risiko mit Info-Text
- `CameraOrbit` – Orbit-Kamera (drehen, zoomen)
- `MainMenuManager` – Steuerung des Hauptmenüs
- `GameMenuNavigation` – Rückkehr ins Menü aus der Spielszene

## Warum

Ich komme von der Unreal Engine (Blueprints) und wollte mir Unity und C#
anhand eines kleinen, abgeschlossenen Projekts beibringen. Das Thema
Baustellensicherheit habe ich gewählt, weil ich Serious Games für die
Praxis spannend finde.

## Status

Erste spielbare Version. Mögliche nächste Schritte: Optionen-Menü, Sounds,
mehr Risiken, Hover-Effekt beim Anvisieren.