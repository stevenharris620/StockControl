Stock Control

An application where users can create a list of suppliers with their respective parts

The project consists of a backend API with  a blazor front end

Front End

The front end is a blazor WASM project written in .net6, the front end authenticates with the backend using a JSON web token

The front end also uses a blazor component library called MudBlazor https://mudblazor.com/

Back End

The backend is a c# .net6 rest api, the backend uses JWT authentication with Microsoft Identity providing the datastore. The databases were created using the entity framework code first approach.
