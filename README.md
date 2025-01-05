# README
## Structuur
De applicatie is opgebouwd uit een paar eenvoudige onderdelen die samenwerken om boeken te beheren:

1. Beslissingsmodule
De beslissingsmodule bepaalt of een boek beschikbaar is om uit te lenen:

Het kijkt of het boek al is uitgeleend.
Als het boek beschikbaar is, kan de gebruiker het lenen, anders niet.
2. API-consumerende Klasse
Deze klasse haalt boekinformatie en uitleengegevens op uit een externe bron, zoals een API. Het zorgt ervoor dat de applicatie de juiste informatie heeft over boeken (bijv. titel, beschikbaarheid).

3. Acties
De applicatie voert de volgende acties uit:

Lenen van een boek (maakt het boek tijdelijk onbeschikbaar).
Terugbrengen van een boek (maakt het boek weer beschikbaar).
Toon de lijst van beschikbare boeken.

Waarom deze structuur?
De structuur is simpel en overzichtelijk. Elk onderdeel doet één specifieke taak, waardoor het makkelijk is om fouten op te lossen en de applicatie uit te breiden.

## Class diagram

```plantuml
class LeenboekBeheerSysteem {
    - List<Book> books
    - List<Loan> loans
    --
    + void BorrowBook(bookId: int, userId: int)
    + void ReturnBook(bookId: int)
    + List<Book> GetAvailableBooks()
}

interface IBookInfoProvider {
    + {abstract} Book GetBookInfo(bookId: int)
}

class Book {
    - int id
    - String title
    - boolean isAvailable
    --
    + boolean IsAvailable()
}

class Loan {
    - int bookId
    - int userId
    - DateTime loanDate
    - DateTime returnDate
    --
    + boolean IsOverdue()
}

class BookInfoProvider {
    - String url
    --
    + Book GetBookInfo(bookId: int)
}

LeenboekBeheerSysteem o-- IBookInfoProvider
LeenboekBeheerSysteem o-- Loan
LeenboekBeheerSysteem o-- Book
BookInfoProvider -up-|> IBookInfoProvider
```

## Test cases

* Als een boek beschikbaar is voor uitleen --> boek kan worden uitgeleend
* Als een boek nog niet is uitgeleend --> boek is beschikbaar voor uitleen
* Als een boek wordt teruggebracht --> boek wordt weer beschikbaar
* Als een gebruiker probeert een boek uit te lenen dat al uitgeleend is --> foutmelding "boek niet beschikbaar"
* Als een gebruiker probeert een boek te reserveren met een terugbrengdatum in het verleden --> foutmelding "ongeldige datum"
* Als een boek wordt teruggebracht --> uitleengegevens worden correct bijgewerkt
* Als er een probleem is met het ophalen van boekinformatie uit de database of API --> exception vangen, geen verdere actie met het boek
