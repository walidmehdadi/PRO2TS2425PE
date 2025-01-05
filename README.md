# README
## Structuur
De applicatie is opgebouwd uit een paar eenvoudige onderdelen die samenwerken om boeken te beheren:

### 1. Beslissingsmodule
De beslissingsmodule bepaalt of een boek beschikbaar is om uit te lenen:

* Het kijkt of het boek al is uitgeleend.
* Als het boek beschikbaar is, kan de gebruiker het lenen, anders niet.
### 2. API-consumerende Klasse
Deze klasse haalt boekinformatie en uitleengegevens op uit een externe bron, zoals een API. Het zorgt ervoor dat de applicatie de juiste informatie heeft over boeken (bijv. titel, beschikbaarheid).

### 3. Acties
De applicatie voert de volgende acties uit:

* Lenen van een boek (maakt het boek tijdelijk onbeschikbaar).
* Terugbrengen van een boek (maakt het boek weer beschikbaar).
* Toon de lijst van beschikbare boeken.

#### Waarom deze structuur?
De structuur is simpel en overzichtelijk. Elk onderdeel doet één specifieke taak, waardoor het makkelijk is om fouten op te lossen en de applicatie uit te breiden.

## Class diagram

```plantuml
class Book {
    - int Id
    - string Title
    - bool isAvailable
    --
    + bool IsAvailable()
}

interface IBookInfoProvider {
    + string Url
    + List<Book> GetBooks()
    + Book GetBookInfo(int bookId)
}

class BookInfoProvider {
    - string Url
    --
    + List<Book> GetBooks()
    + Book GetBookInfo(int bookId)
}

interface IBookManagement {
    + void BorrowBook(int bookId, int userId)
    + void ReturnBook(int bookId)
}

class BookManagement {
    - IBookInfoProvider bookInfoProvider
    --
    + void BorrowBook(int bookId, int userId)
    + void ReturnBook(int bookId)
}

class Library {
    - IBookInfoProvider bookInfoProvider
    - IBookManagement bookManagement
    --
    + void Work()
    + void BorrowBookInteraction()
    + void ReturnBookInteraction()
    + void DisplayAvailableBooks()
    + bool IsValidReturnDate(string returnDate)
}

BookInfoProvider -up-|> IBookInfoProvider
BookManagement -up-|> IBookManagement
Library o-down- IBookInfoProvider
Library o-down- IBookManagement
IBookInfoProvider o-down- Book
```

## Test cases

* Als een boek beschikbaar is voor uitleen --> boek kan worden uitgeleend
* Als een boek nog niet is uitgeleend --> boek is beschikbaar voor uitleen
* Als een boek wordt teruggebracht --> boek wordt weer beschikbaar
* Als een gebruiker probeert een boek uit te lenen dat al uitgeleend is --> foutmelding "boek niet beschikbaar"
* Als een gebruiker probeert een boek te reserveren met een terugbrengdatum in het verleden --> foutmelding "ongeldige datum"
* Als een boek wordt teruggebracht --> uitleengegevens worden correct bijgewerkt
* Als er een probleem is met het ophalen van boekinformatie uit de database of API --> exception vangen, geen verdere actie met het boek
* Als een gebruiker meerdere boeken tegelijk probeert uit te lenen --> de boeken worden één voor één verwerkt
* Als meerdere boeken tegelijk worden teruggebracht --> ze worden allemaal als beschikbaar gemarkeerd

De testen zijn opgesteld door gebruik te maken van het ZOMBIES-principe, waarbij de tests in verschillende fasen zijn opgebouwd:

1. Zero Tests: We beginnen met eenvoudige scenario's, zoals het uitlenen van een beschikbaar boek, en controleren of het boek beschikbaar is als het niet is uitgeleend.

2. One Tests: We breiden de testen uit naar situaties waarin een gebruiker probeert een boek te reserveren met een verkeerde datum of een boek probeert uit te lenen dat al is uitgeleend. Deze testen gaan over foutafhandelingsscenario's.

3. Many Tests: Hier testen we complexere situaties, zoals meerdere boeken tegelijk uitlenen of terugbrengen, waarbij we testen of de boeken correct één voor één verwerkt worden of correct als beschikbaar worden gemarkeerd.

4. Exception Handling en Externe Fouten: We testen ook hoe de applicatie omgaat met externe fouten, zoals problemen met het ophalen van boekinformatie, en zorgen ervoor dat de applicatie geen acties uitvoert in geval van een error.
