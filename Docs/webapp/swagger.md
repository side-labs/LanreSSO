# Swagger

## Acceso 

Se puede acceder en {webapp}/swagger

## Llamadas autenticadas

Para hacer llamadas a endpoints autenticados, tenemos que hacer login, coger el access token que nos devuelve y hacer las llamadas con ese token. 

### Pasos

1. Ir a *{website}/swagger*
2. Ejecutar el endpoint de */Api/Accounts*
3. Copiamos el *AccessToken* que nos devuelve
4. Hacemos click en el botón de *Authorize*
5. En el textbox insertamos *Bearer* y el *AccessToken* al final. Ej:
    - Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlcmlj...
6. Todas las llamadas estarán autenticadas a partir de ahora