## API ECOMMERCE

API RESTful desarrollada en .NET para la gestión de un sistema de ecommerce. Permite administrar usuarios, productos y pedidos, implementando autenticación segura mediante JWT y una arquitectura por capas.

## Tecnologías

-.NET 10 (EcommerceApi.Api)
-Entity Framework Core
-SQL Server
-Autenticación JWT (Bearer)

## Arquitectura

- `EcommerceApi.Domain`: entidades (`Producto`, `Usuario`, `Pedido`, `PedidoDetalle`, `Categoria`)
- `EcommerceApi.Application`: DTOs, interfaces, servicios (`ProductoService`, `UsuarioService`, `PedidoService`, `AuthService`).
- `EcommerceApi.Infrastructure`: EF Core (`EcommerceDbContext`), repositorios
- `EcommerceApi.Api`: controllers, generación de JWT (`JwtTokenGenerator`), carga de `JWT_KEY` desde `.env` (`Configuration/EnvFileLoader`, `JwtKeyResolver`), `Program.cs`

## Autenticación y Autorización

-Registro público de usuarios
-Login con generación de token JWT
-Protección de endpoints mediante [Authorize]
-Endpoints públicos usan [AllowAnonymous]

## JWT

-Algoritmo: HS256
-Token incluye datos del usuario (claims)
-Expiración configurable

## Seguridad

-Contraseñas hasheadas con PBKDF2
-Tokens firmados con JWT
-Clave secreta protegida fuera del repositorio

## Funcionalidades

- `Producto` - Listar: `GET /api/Producto`
- `Producto` - Obtener por id: `GET /api/Producto/{id}`
- `Producto` - Crear: `POST /api/Producto`
- `Usuario` - Listar: `GET /api/Usuario`
- `Usuario` - Obtener por id: `GET /api/Usuario/{id}`
- `Usuario` - Registro: `POST /api/Usuario` con `UsuarioCreateDTO`
- `Pedido` - Crear: `POST /api/pedidos` con `PedidoCreateDTO`

## DTOs de usuario

- `UsuarioDTO`: datos de salida (sin contraseña): `Id`, `Nombre`, `Email`, `Rol`, `Estado`, `FechaRegistro`.
- `UsuarioCreateDTO`: registro con `Contraseña` y demás campos; la API no devuelve la contraseña.
- `LoginRequestDTO` / `LoginResponseDTO`: credenciales y token tras el login.

## Cómo ejecutar el proyecto

Configurar conexión en:
EcommerceApi.Api/appsettings.json

"ConnectionStrings": {
"DefaultConnection": "connection_string"}

Configurar variables JWT:
Crear archivo .env basado en .env.example

Crear migraciones:
dotnet ef migrations add InitialCreate --project EcommerceApi.Infrastructure --startup-project EcommerceApi.Api

Aplicar migraciones:
dotnet ef database update --project EcommerceApi.Infrastructure --startup-project EcommerceApi.Api

Ejecutar la API:
dotnet run --project EcommerceApi.Api

## Endpoints principales

- `POST /api/Usuario` (registro, público)
- `POST /api/Auth/login` (público)
- `GET /api/Producto`, `GET /api/Producto/{id}`, `POST /api/Producto` (JWT)
- `GET /api/Usuario`, `GET /api/Usuario/{id}` (JWT)
- `POST /api/pedidos` (JWT)
