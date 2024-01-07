Aplikacja GymApp
Autorzy: Mateusz Nowak & Zbigniew Skrzypczak

Uruchomienie: https://gymapp.hostingasp.pl/
Aplikacja jest hostowana poprzez portal webio.

Chcąc uruchomić aplikację lokalnie należy zainstalować serwer MySQL.
Za pomocą programu np.Azure Data Studio utworzyc pustą bazę danych.
W pliku appsettings.json w węźle ConnectionString analogicznie dodać connectionstring do swojej bazy danych oraz serwera.
W pliku Program.cs w zmiennej connectionString podmieniamy GymApp na nazwe swojego connectionStringa z appsetings.json.
W terminalu wywołujemy metodę Update-Database - Po poprawnym zbudowaniu sie bazy danych mozna odpalic aplikację.
Myślę, że dzięlki hostingowi, nie trzeba tego wykonywać.



Technologie:
Baza danych – MySQL - został wybrany MySQL ze względu na sprzęt, na którym była tworzona aplikacja - MacBook - który nie obsługuje SQL Servera bez Dockera(lub podobnych).
Aplikacja - Aplikacja była tworzona za pomocą ASP.NET MVC - więc zarówno warstwa Backend jak i Frontend znajduje się w jednym źródle.
Zostały zastosowane między innymi biblioteki ASP.NET Identity oraz Entity Framework - dzięki którym mogłobyć konfigurowane połączenie z bazą danych oraz dodane uwierzytelnianie użytkowników.


Aplikacja przeglądarkowa (strona internetowa z funkcjonalnościami pozwalającymi nazywać ją aplikacją).

1. Użytkownicy niezarejestrowani będą za to mogli potraktować ją jako encyklopedię wiedzy na temat ćwiczeń dla danej partii ciała.
Dla tej części użytkowników dostępna jest ogólna lista ćwiczeń oraz w Zakładce Partie Ciała można sprawdzić ćwiczenia przypisane do konkretnej partii.

2. Użytkownicy zarejestrowani mają większy wachlarz możliwości.
Każdy z użytkowników ma możliwość :
- Utworzenia nowego treningu
- Dodania do niego wybranych ćwiczeń oraz dobrania liczby serii.
- Utworzenia sesji treningowej
- Konfiguracja sesji treningowej - Dla każdej serii każdego ćwiczenia można dopisać ciężar, jaki będzie stosowany podczas treningu.


  FUNKCJONALNOŚCI:

  1.System rejestracji / logowania
  Do zaimplementowania systemu rejestracji i logowania zostało użyte gotowe rozwiązanie dostępne w projektach ASP.NET  - biblioteki Identity.
  Niestety, ze względu na brak dostępu do Pages/Account/Register oraz Login nie mogliśmy skonfigurować formularzy do swoich potrzeb i dlatego system rejestracji oraz logowania jest standardowy dla tej biblioteki.
  Dzięki Identity mogliśmy rozdzielić funkcjonalności na takie, które są dostępne dla użytkowników niezarejestrowanych oraz zarejestrowanych.

  2. Lista ćwiczeń ogólna / podzielona pod poszczególne partie ciała.
  Prosta funkcjonalność, ogólnodostępna (dla użytkowników zarówno zalogowanych jak i niezalogowanych).
  W zakładce 'Ćwiczenia' mamy listę wszystkich dostępnych ćwiczeń oraz partie ciała do jakiej są przypisane.
  W zakładce 'Partie ciała' mamy listę Partii ciała. Po kliknięciu w 'Szczegóły' danej partii ciała wyświetlaja się ćwiczenia które są przypisane do tej konkretnej Partii Ciała.

 3. Funkcjonalności dot. Treningu.
    Funkcjonalności te dostępne są tylko i wyłącznie dla zarejestrowanych/zalogowanych użytkowników.
    W zakładce 'Moje treningi' widoczna jest lista utworzonych treningów przypisanych do/utworzonych przez konkretnego użytkownika, który jest zalogowany.
    Użytkownik nie widzi treningów innego użytkownika i nie może na nie wpływać.

   Utwórz nowy trening:
      Ta funkcjonalność pozwala nam utworzyć nowy trening.
      Na tym etapie Wpisujemy tylko nazwę.

   Konfiguracja treningu:
      Ta funkcjonalność dostepna jest dla każdego utworzonego treningu.
      Pozwala ona na dodanie ćwiczeń które nas interesują w danym treningu oraz przypisanie im ilości serii, która podczas tego treningu będzie wykonywana.  

   Oczywiście trening możemy również usunąć.
   
   Pokaż szczegóły treningu:
      Wchodząć w tą zakładkę ukazują nam się szczegóły treningu:
      Mamy listę ćwiczeń wraz z ilością serii którą przypisaliśmy. Możemy usunąć ćwiczenie lub edytować (zmienić ilość serii).
   
  4. Funkcjonalnośći dot. Sesji treningowych.
     Funkcjonalności również dostępne tylko dla użytkowników zarejestrowanych/zalogowanych.
     W zakładce 'Pokaż moje sesje' widoczna jest lista sesji treningowych przypisanych do/utworzonych przez konkretnego użytkownika który jest zalogowany.
     Sesję są indywidualne dla poszczególnych uzytkownikow.

     'Dodaj sesje':
        Dzięki tej funkcjonalności użytkownik może dodać nową sesję którą będzie wykonywał.
          - Określamy datę (nie późniejszą niż aktualna)
          - Wybieramy trening (jeden z utworzonych przez nasz wcześniej)
     'Konfiguruj sesję'
     Przy każdej z utworzonych sesji mamy opcję 'Konfiguruj sesję'.
       Funkcjonalność ta
         - najpierw zaczytuje poprawną ilość serii każdego z ćwiczeń przypisanych do treningu który wybraliśmy przy tworzeniu sesji,
         - w formularzu do każdej serii każdego ćwiczenia przypisujemy ciężar z jakim będziemy ćwiczyć w danej serii (nie mniejszy niż 0).
         - sesję możemy skonfigurować raz.
         - sesję możemu usunąć
         - możemy podejrzeć szczegóły sesji.
     

