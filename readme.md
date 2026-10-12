# UsbCam

Zobrazení obrazu z USB kamery s pomůckami (kříž, kružnice, měření úhlu). Určeno pro potřeby aplikace XYCordReader (viz https://www.printables.com/cs/model/717882-souradnicovy-snimac).

## Požadavky

- 64bitový Windows.
- Nic dalšího instalovat nemusíte, `UsbCam.exe` je samostatný soubor včetně .NET 10.

## Spuštění

1. Spusťte `UsbCam.exe`.
2. V menu **Camera** vyberte kameru (výběr se pamatuje a příště se spustí sama).

## Ovládání

| Prvek | Funkce |
|---|---|
| **Circle** | Průměr kružnice (poloměr v pixelech) nakreslené do středu obrazu, `Esc` = 0 (bez kružnice). |
| **Angle** | Úhel natočení kříže 0–359°, šipky nebo kolečko myši. Přetečení přes 0/359 se zalomí. |
| `Enter` v poli Angle / tlačítko **Set Guide Line** | Uloží aktuální úhel jako značku (vodicí čára). |
| `Esc` v poli Angle | Zruší značku (při nulové značce vynuluje i úhel). |
| Popisek vedle tlačítka | Značka − úhel = rozdíl / doplněk do 360°. |
| Tlačítko s fotoaparátem | Zkopíruje aktuální snímek do schránky. |
| Tlačítko s disketou | Uloží snímek do složky *File Target Path*. |
| Tlačítko s kamerou | Zapne / vypne periodické ukládání snímků do složky *Periodical Target Path* (zapnuté je červené). |

Snímek obsahuje i nakreslené pomůcky (kříž, kružnice, úhly).

Soubory se ukládají jako PNG pod názvem `UsbCam_yyyyMMdd_HHmmss_fff.png`.

## Menu Settings

- **Barevná sada pomůcek** – výběr barvy čar. Zkratka `Ctrl` + číslo na numerické klávesnici (`0`–`9`), `Ctrl` + `.` pro výchozí sadu.
- **Size Mode** – Normal / Center / Zoom (způsob zobrazení obrazu v okně).
- **Targets…** – nastavení cílových složek a periody (1–86400 s) pro ukládání snímků.
- **Always on Top** – okno vždy navrchu.
- **Dark Mode** – tmavý režim. Projeví se po restartu aplikace (aplikace nabídne restart).

## Uložené nastavení

Aplikace si pamatuje kameru, polohu a velikost okna, barevnou sadu, Size Mode, Always on Top, Dark Mode, cílové složky, periodu, Circle, Angle i značku úhlu. Nastavení je v registru:

`HKEY_CURRENT_USER\Software\PaRi\UsbCam\1.0.x.x`

## Sestavení

- Vývoj: `UsbCam.slnx` (Visual Studio 2022+ / `dotnet build`), cíl `net10.0-windows`.
- Jeden exe: spusťte `publish.cmd`, výsledek je ve složce `publish\` (`UsbCam.exe` a `readme.md`).
- Malý exe bez .NET (cca 0,7 MB): `publishMin.cmd`, výsledek ve složce `publishMin`. Na cílovém počítači musí být nainstalován .NET 10 Desktop Runtime (x64).
