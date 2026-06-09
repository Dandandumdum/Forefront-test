# Forefront Consulting - Technology
## Kodtest

Studera den bifogade koden CustomerShoeSizeController.cs. Koden ingår i ett projekt med ett fåtal andra klasser, den kompilerar utan fel. Kunden har snabbtestat den och funnit att den löser deras problem för stunden, och den snurrar nu i en produktionsmiljö.

-	Vad gör koden?
-	Vilka delar är bra?
-	Vilka delar är dåliga? Motivera.
-	Utan att skriva om koden ifrån grunden, vad skulle man kunna göra annorlunda?




3. -Tycker inte om routes endpoints. Expose data direct kunde vara en query eller kankse i body, mer secure och better (redan i JSON format)
    - Null issues: Inget konsitent null checking eller hantering, vissa fields få vara null, men de bli blandad ihop i olika functioner
        e.g Fullname i Customer.cs är non-nullable, och customer kan bli hittat från en nullable Repo call                          (CustomerShoeSizeController.cs, 79 )
    - Mycket declaring variable on the fly i en storre function, sloppy
    - Inget email validation
    - Declarera en obj i controller (CustomerShoeSize), inte rätt plats för det gör som Customers.cs
    - Inget default Constructor för Customers.cs (inte den värsta men)
    
    
    
    
4. -Bättre structure:
        -Utilities fil, inte har helper functions mitt i andra functions, 
        -har ENUMs på eget fil, 
        -Alla DTOs borde har eget fil och en DTO map, med samma structure e.g default constructures och getter/setter methods
        -Bättre namning conventions, "OtherClasses"? Som Engelska eller Geographi?
   - Fix potential null pointers:
        - Har Null checks och DTOs som tillater inte null values om det leder till downstream issues
        
