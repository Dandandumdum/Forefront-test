# Forefront Consulting - Technology
## Kodtest

Studera den bifogade koden CustomerShoeSizeController.cs. Koden ingår i ett projekt med ett fåtal andra klasser, den kompilerar utan fel. Kunden har snabbtestat den och funnit att den löser deras problem för stunden, och den snurrar nu i en produktionsmiljö.

-	Vad gör koden?
-	Vilka delar är bra?
-	Vilka delar är dåliga? Motivera.
-	Utan att skriva om koden ifrån grunden, vad skulle man kunna göra annorlunda?




3. -Tycker inte om routes endpoints. Expose data direct kunde vara en query eller kankse i body, mer secure och better (redan i JSON format)
    - Default endpoint is "[controller]", problematic
    - Null issues: Inget konsitent null checking eller hantering, vissa fields få vara null, men de bli blandad ihop i olika functioner
        e.g Fullname i Customer.cs är non-nullable, och customer kan bli hittat från en nullable Repo call                          (CustomerShoeSizeController.cs, 79 )
    - Mycket declaring variable on the fly i en storre function, sloppy
    - Inget email validation, kan leder till mängda problem downstream
    - Declarera en obj i controller (CustomerShoeSize), inte rätt plats för det gör som Customers.cs
    - Det border vara en CustomerShoeSizeService vart alla business logic kunna vara, MVC model inte allt i en controller,              simplictiy och bra code quality
    - Inget default Constructor för Customers.cs (inte den värsta men), bra att har parity för DTOs
    - Onödigt parameters (CustomerRepository.cs, 45), exposing data för ingen anledning
    - Declaring ny object inline i CustomerRepository.cs istället för creating an instance innan, leder till issues?
    - Fake data layer inget DB (men jag fattar att det är inte en riktigt produkt)
    - Where are the tests?
    - Where is the error handling? Controller med lite error hantering för enklare debugging
    - Try-catch block saknas, ledeer to ohanterad errors
    - US_CUSTOMARY_FEMALE saknas i switch statement, fast US_CHILD har ett error thrown, kankse inte den bäste lösning
    - StatisticsService är aldrig använt, känns som scope creep, och det är inte thread safe. Risk för ingen anledning.
    - Async functioner i CustomerRepository.cs men inget await när dem bli kallad
    
    
4. -Bättre structure:
        -Utilities fil, inte har helper functions mitt i andra functions, 
        -har ENUMs på eget fil, 
        -Alla DTOs borde har eget fil och en DTO map, med samma structure e.g default constructures och getter/setter methods
        -Bättre namning conventions, "OtherClasses"? Som Engelska eller Geographi? MVC please
        -SKapa riktigt Service for CustomerShoe logic, och har controller fokuserad på dataflow.
   - Fix potential null pointers:
        - Har Null checks och DTOs som tillater inte null values om det leder till downstream issues
        - Null handling i Service level, efter Service har blivit skapad
   - Implement Tests, särskilt för alla logic.
   - Använd en local DB (EF etc) så att CRUD actions kan vara testad, och tjänsten kan faktist funkar.
   - Fix HTTP requests. Har data i Body av requestet istället för rätt i den endpoint, och fixa den default endpoint. 
   - Ser till att async functions använd await, eller tar bort async om inte nödigt
   - Input validering för allt som kom in från användare, och Email format validering (@ och .com/.co etc)
