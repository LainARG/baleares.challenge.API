///////////////////////////////////////////////////////////////
/////////////////////// Deploy local //////////////////////////
///////////////////////////////////////////////////////////////

1 - Abra la solución en VS2022 u otro IDE. 

2 - Altere todo lo anterior a ";Database" en 
    el campo "DefaultConnection" del archivo 
    appsettings.development.json del proyecto
    para que coincida con su instancia de 
    SQL Server. 

3 - Acceda a:
    Herramientas > Administrador de paquetes NUGET > 
    consola del administrador de paquetes. 
    ó
    Tools > NuGet Package Manager > Package Manager Console
    según corresponda. 

4 - Verifique que "proyecto predeterminado" ó Default project
    están apuntando al proyecto indicado en la parte superior
    de la consola que acaba de acceder. 

5 - Ingrese update-database y presione enter. 

6 - Verifique que las tablas han sido creada correctamente
    en su base de datos. 

7 - Si lo anterior falla, sírvase del script Baleares.sql, pre-
    sente en el root del proyecto/migrations/backup.

///////////////////////////////////////////////////////////////
////////////////////// Endpoint Test. /////////////////////////
///////////////////////////////////////////////////////////////
**NOTA: La interfaz de Swagger se generará automáticamente 
        al iniciar el proyecto y se encuentra configurada
        para trabajar con el JWT implementado, utilice el
        endpoint "register" para crear un usuario, luego 
        utilice el endpoint login para loguearse, luego 
        copie el token obtenido por response, diríjase al 
        sector superior derecho de la pantalla, presione 
        sobre el botón "Authorize" y pegue el token copiado, 
        tras esto, tendrá acceso a todos los endpoints.
///////////////////////////////////////////////////////////////
Registrar un usuario://////////////////////////////////////////
///////////////////////////////////////////////////////////////
POST ----------> https://localhost:44371/api/users/register
request body:
{
  "userName": "lain",
  "Email": "dcaballero2022@gmail.com",
  "Password": "Fry02320**",
  "FirstName": "Diego",
  "LastName": "Caballero"
}
** Obtendrá un usuario con el cual loguear y usar la api.
//////////////////////////////////////////////////////////////


//////////////////////////////////////////////////////////////
Loguear al usuario:///////////////////////////////////////////
//////////////////////////////////////////////////////////////
POST -----------> https://localhost:44371/api/Account/login
request body:
{
  "Username": "lain",
  "Password": "Fry02320**"
}
** Obtendrá un token de sesión como response, lo requerirá
para poder acceder a los demás endpoints, el token expirará a
los 20 minutos de loguearse o al desloguearse./////////////////
///////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////
Alta de contacto:///////////////////////////////////////
////////////////////////////////////////////////////////
POST -----------> https://localhost:44371/api/contacts        
request body: 
{
  "userId": 1,
  "name": "pequeño contacto",
  "company": "pequeña empresa",
  "email": "contactito@gmail.com",
  "birthDate": "1991-04-12",
  "phone": "1130389798",
  "phoneWork": "1130389798",
  "address": "casa de contactito 123",
  "province": "san contacto",
  "city": "contact city"
}
//////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////
Obtener contacto por Id:///////////////////////////////////////
///////////////////////////////////////////////////////////////
GET -----------> https://localhost:44371/api/contacts/1 ///////
///////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////
Actualizar contacto:////////////////////////////////////
////////////////////////////////////////////////////////
PUT -----------> https://localhost:44371/api/contacts/1
request body: 
{
  "userId": 1,
  "name": "pequeño contacto actualizado",
  "company": "pequeña empresa actualizada",
  "email": "contactitoactualizado@gmail.com",
  "birthDate": "1991-04-12",
  "phone": "1130389798",
  "phoneWork": "1130389798",
  "address": "casa de contactito actualizado 123",
  "province": "san contacto actualizado",
  "city": "contact city actualizada"
}
////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////
Eliminar contacto://////////////////////////////////////
///////////////////////////////////////////////////////////////
DELETE -----------> https://localhost:44371/api/contacts //////
///////////////////////////////////////////////////////////////
request body: 
{
  "email": "contactito@gmail.com"
}
///////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////
Eliminar contacto por Id://////////////////////////////////////
///////////////////////////////////////////////////////////////
DELETE -----------> https://localhost:44371/api/contacts/1 ////
///////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////////////////////////////
Buscar contacto por telefono o Email:///////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////
GET -----------> https://localhost:44371/api/contacts/SearchByPhoneOrEmail?query=contactito
////////////////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////
Obtener todos los contactos:///////////////////////////////////
///////////////////////////////////////////////////////////////
GET -----------> https://localhost:44371/api/contacts  ////////
///////////////////////////////////////////////////////////////


/////////////////////////////////////////////////////////////////////////////////////////
Buscar contacto por ciudad o provincia://////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////
GET -----------> https://localhost:44371/api/Contacts/GetByCityOrProvince?query=contacto
/////////////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////
Obtener usuarios ordenados por email://////////////////////////
///////////////////////////////////////////////////////////////
GET -----------> https://localhost:44371/api/users/sortByEmail
///////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////
Desloguear usuario:////////////////////////////////////////////
///////////////////////////////////////////////////////////////
POST -----------> https://localhost:44371/api/Account/logout

** Revocará todos los tokens de sesión que haya generado al
loguearse, perderá el acceso a todos los endpoints que no 
correspondan al registro y logueo de usuarios hasta que 
vuelva a loguearse.///////////////////////////////////////////
//////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////////////////////////////////////////////
######################################## IMPRESIONES GENERALES ############################################
////////////////////////////////////////////////////////////////////////////////////////////////////////////

Por mi parte, miento si digo que este proyecto representó alguna clase de desafío, es algo que puede 
hacerse en dos dias de desarrollo (16hs o menos) sin demasiado inconveniente, más que nada por tratarse 
de un requerimiento común del dia a dia en todo sentido, es muy dificil llegar a tener un perfil Sr. 
antes de haber hecho decenas de veces algo como esto. 

                                    ¿Como pudo ser más desafiante?

Se pudo solicitar un consumidor básico de ésta API, lo cual si bien escapa un poco a las capacidades de un
dev. backend plano, lo vuelve al mismo tiempo un poco más complejo para la mayoría de los que tenemos un 
perfil orientado a fullstack.


** CONCLUSIÓN: No fue desafiante, pero fue algo más lento por el requerimiento específico de usar una 
base de datos, cuando sin dudas pudo haberse configurado un seed de datos en memoria. 
             
////////////////////////////////////////////////////////////////////////////////////////////////////////////
 