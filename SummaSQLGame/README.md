# SummaSQLGame

## Overzicht
SummaSQLGame is een educatieve applicatie waarmee gebruikers op een speelse manier SQL-vaardigheden kunnen oefenen. De app bevat verschillende puzzeltypes, elk gericht op het toepassen van SQL-kennis in realistische scenario's. De focus ligt op het leren gebruiken van onder andere 'ORDER BY' en 'WHERE' clauses, maar de structuur is uitbreidbaar naar andere SQL-onderwerpen.

## Functionaliteiten
- **SQL Practice Puzzle**: Oefen met het schrijven van SQL-queries op basis van willekeurige opdrachten. Elke poging toont een andere variant, zodat je niet kunt volstaan met het onthouden van één oplossing.
- **Meerdere puzzeltypes**: Naast de SQL Practice Puzzle zijn er ook andere puzzels, zoals:
  - Knoppenpuzzel
  - Battleship/woordenpuzzel
  - Avonturierspuzzel
  - En meer (uitbreidbaar)
- **Directe feedback**: Je krijgt direct te zien of je antwoord goed of fout is, en je kunt onbeperkt opnieuw proberen.
- **Modulaire opzet**: Elk puzzeltype heeft een eigen ViewModel en logica, zodat nieuwe puzzels eenvoudig toe te voegen zijn.

## Voorbeeld: SQL Practice Puzzle
Hieronder enkele voorbeeldvarianten:

1. **Studenten uit klas 'A', score aflopend**
   - _Beschrijving_: Selecteer alle studenten uit klas 'A', geordend op score aflopend.
   - _Voorbeeld SQL_: `SELECT * FROM studenten WHERE klas = 'A' ORDER BY score DESC;`

2. **Steden in Nederland, inwoners oplopend**
   - _Beschrijving_: Selecteer alle steden in het land 'Nederland', geordend op inwoneraantal oplopend.
   - _Voorbeeld SQL_: `SELECT * FROM steden WHERE land = 'Nederland' ORDER BY inwoners ASC;`

3. **Bieren met alcohol > 6, naam oplopend**
   - _Beschrijving_: Selecteer alle bieren met een alcoholpercentage hoger dan 6, geordend op naam oplopend.
   - _Voorbeeld SQL_: `SELECT * FROM bieren WHERE alcohol > 6 ORDER BY naam ASC;`

4. **Liedjes van Queen, jaartal aflopend**
   - _Beschrijving_: Selecteer alle liedjes van artiest 'Queen', geordend op jaartal aflopend.
   - _Voorbeeld SQL_: `SELECT * FROM liedjes WHERE artiest = 'Queen' ORDER BY jaar DESC;`

5. **Honden ouder dan 5, ras oplopend**
   - _Beschrijving_: Selecteer alle honden ouder dan 5 jaar, geordend op ras oplopend.
   - _Voorbeeld SQL_: `SELECT * FROM honden WHERE leeftijd > 5 ORDER BY ras ASC;`

## Uitbreidbaarheid
- Nieuwe puzzelvarianten kun je toevoegen in de variant-lijst van de betreffende ViewModel.
- Nieuwe puzzeltypes zijn eenvoudig toe te voegen dankzij de modulaire structuur.
- De app ondersteunt uitbreiding naar andere SQL-onderwerpen en tabellen.

## Gebruikerservaring
- Duidelijke UI met uitleg en voorbeeldopdrachten.
- Directe feedback en onbeperkt proberen.
- Geschikt voor klassikaal gebruik, zelfstudie of als toets.

## Mappenstructuur (belangrijkste onderdelen)
- `Databases/` – Seeders en databasecontext voor alle tabellen
- `ViewModels/` – Logica voor elk puzzeltype
- `Views/` – UI voor elk puzzeltype
- `Helpers/` – Hulpmethodes en utilities
- `Migrations/` – Database migraties en seeds
- `Assets/` – Afbeeldingen, geluiden en andere resources

## Installatie & Gebruik
1. Clone deze repository.
2. Open het project in Visual Studio of een andere C# IDE.
3. Voer de migraties uit om de database te vullen met voorbeelddata.
4. Start de applicatie en kies een puzzeltype.

## Bijdragen
Wil je nieuwe puzzelvarianten of puzzeltypes voorstellen? Open een issue of maak een pull request.

---
© 2025 SummaSQLGame – Educatief project voor SQL-vaardigheden
