# Reviver

Aplikacja Reviver jest rozwiązaniem mającym na celu monitorowanie dostępności innych aplikacji poprzez regularne sprawdzanie ich stanu. Głównym zadaniem aplikacji jest utrzymanie tych aplikacji w stanie aktywnym poprzez cykliczne wywoływanie określonych zasobów. Dzięki temu unika się usypiania tych aplikacji ze względu na brak ruchu.  
  
  
## Funkcje

- Worker jest odpowiedzialny za regularne sprawdzanie dostępności określonych zasobów internetowych. Jego działanie opiera się na cyklicznym wywoływaniu wybranych URL-i w określonych odstępach czasu. W przypadku braku odpowiedzi zasobu, aplikacja rejestruje informacje o błędzie w logach.
- Interfejs użytkownika oparty jest na technologii Razor Pages i zapewnia prosty sposób przeglądania logów zebranych przez Worker-a.

## Technologie

ASP.NET Core 7 Razor Pages

C#  
JavaScript  
HTML  
CSS  
Bootstrap

## Jak uruchomić aplikację

1. Uruchom aplikację (Ctrl+F5).  
  

  
> Reviver współpracuje ze swoim [odpowiednikiem](http://reviver.somee.com), tworząc spójne środowisko dla aplikacji. Odpowiednik regularnie kontaktuje się z głównym Reviverem, który nadzoruje nie tylko swój odpowiednik, lecz także inne aplikacje, dbając o ich ciągłą aktywność.
