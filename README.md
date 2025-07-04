# Examen - Lenguajes Visuales 1

Aplicación de escritorio desarrollada en C# usando WPF, Entity Framework Core y arquitectura MVVM.  
Permite gestionar usuarios, productos y categorías con login seguro, registro y CRUD completo.

---

## Tecnologías utilizadas

- WPF (Windows Presentation Foundation)
- Entity Framework Core
- SQL Server
- MVVM (Model-View-ViewModel)

---

## Características principales

- **Login seguro y registro de usuarios**
- **Gestión de productos y categorías**: CRUD completo con validaciones
- **Búsqueda y filtros** para productos y categorías
- **Interfaz moderna** con estilos y notificaciones visuales
- **Validaciones**: No permite eliminar categorías con productos asociados, ni registros duplicados

---

## ¿Cómo usar la app?

1. **Abrir la aplicación** (ejecutar el archivo `.exe` o desde Visual Studio).
2. **Registrar un nuevo usuario** desde la ventana de login.
3. **Iniciar sesión** con ese usuario.
4. **Gestionar productos y categorías** desde la ventana principal.
5. **Buscar, editar y eliminar registros** según corresponda.

---

## Configuración de la base de datos

Esta aplicación utiliza **SQL Server** como motor de base de datos.

### 1. Crear la base de datos

Puedes crear una base de datos llamada, por ejemplo, `ExamenLV1DB` en SQL Server Management Studio (SSMS):

### 2. Tablas necesarias
La aplicación utiliza tres tablas principales: Usuarios, Categorias y Productos.
Normalmente, Entity Framework las crea automáticamente al ejecutar la app por primera vez,
pero aquí se detallan sus estructuras básicas por si necesitas revisarlas o crearlas manualmente:

**Tabla: Usuarios**

Id	int	PRIMARY KEY, Identity
Nombre	nvarchar	No nulo
Correo	nvarchar	ÚNICO, No nulo
Contraseña	nvarchar	No nulo

- **Tabla: Categorias**

Id	int	PRIMARY KEY, Identity
Nombre	nvarchar	ÚNICO, No nulo

- **Tabla: Productos**

Id	int	PRIMARY KEY, Identity
Nombre	nvarchar	No nulo
Precio	decimal	No nulo
CategoriaId	int	FOREIGN KEY hacia Categorias (Id)

NOTA: No es necesario crear las tablas a mano. La app las genera automáticamente al iniciar si la base está vacía.

### 3. Cadena de conexión
La conexión a SQL Server se configura en el archivo AppDbContext.cs.
Por defecto, la cadena de conexión es la siguiente:


protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExamenLV1DB;Trusted_Connection=True;TrustServerCertificate=True;");
}


### ¿Qué debes revisar?

El nombre del servidor (localhost\\SQLEXPRESS) debe coincidir con tu instalación de SQL Server.

El nombre de la base de datos (ExamenLV1DB) debe ser igual al que creaste.

### Si tu SQL Server requiere usuario y contraseña:
Cambia la cadena por:


optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExamenLV1DB;User Id=TU_USUARIO;Password=TU_PASSWORD;TrustServerCertificate=True;");


### 4. Primer uso
### 5. Al ejecutar la aplicación, si la base de datos y las tablas no existen, la app las creará automáticamente usando Entity Framework.

Puedes gestionar la base y ver los datos con SQL Server Management Studio (SSMS).

##Notas importantes
**Permisos: Si tienes problemas de acceso, revisa que tu usuario de SQL Server tenga permisos de lectura y escritura en la base.

Servicio de SQL Server: Verifica que el servicio de SQL Server esté iniciado antes de abrir la aplicación.

Errores de conexión: Si ves errores al iniciar la app, revisa la cadena de conexión y que la base de datos exista y esté accesible.

Modificaciones: Si cambias el nombre de la base de datos o el servidor, recuerda actualizar la cadena de conexión en el código.


Autor
Dennis Brunaga
Proyecto final de la materia "Lenguajes Visuales 1"
