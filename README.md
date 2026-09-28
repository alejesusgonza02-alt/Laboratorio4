Este proyecto consiste en el desarrollo de una aplicación de escritorio para la gestión de productos utilizando C# y Windows Forms. La aplicación permite realizar las operaciones básicas de un sistema CRUD: crear, consultar, modificar y eliminar productos.
Cada producto puede contener un nombre, precio, cantidad e imagen. La información se almacena utilizando una base de datos SQLite, permitiendo que los datos permanezcan guardados aunque se cierre la aplicación.
El proyecto fue desarrollado como parte de las actividades prácticas de programación y tiene como objetivo aplicar conceptos de programación orientada a objetos, manejo de bases de datos y desarrollo de interfaces gráficas.

Objetivos

•	Crear una aplicación CRUD utilizando C#.
•	Implementar una interfaz gráfica utilizando Windows Forms.
•	Utilizar SQLite para almacenar información.
•	Permitir agregar, modificar, eliminar y consultar productos.
•	Implementar la selección y almacenamiento de imágenes.
•	Realizar búsquedas de productos.
•	Aplicar validaciones básicas en los datos ingresados.

Tecnologías utilizadas
•	Lenguaje: C#
•	Framework: .NET Framework 4.7.2
•	Interfaz gráfica: Windows Forms
•	Base de datos: SQLite
•	IDE: Visual Studio
•	Librería utilizada: System.Data.SQLite.Core

Funcionalidades

Guardar productos
El usuario puede registrar un nuevo producto ingresando:
•	Nombre del producto.
•	Precio.
•	Cantidad.
•	Imagen del producto.
Al presionar el botón Guardar, la información se almacena en la base de datos y el producto aparece en la tabla.

Mostrar productos
Los productos registrados se muestran en un DataGridView, donde se pueden visualizar:
•	ID.
•	Producto.
•	Precio.
•	Cantidad.
•	Imagen.
Los productos se cargan automáticamente al iniciar la aplicación.

Modificar productos
Al seleccionar un producto de la tabla, sus datos aparecen en los campos de texto.
El usuario puede modificar:
•	Nombre.
•	Precio.
•	Cantidad.
•	Imagen.

Después de presionar Modificar, los cambios se guardan en la base de datos.
Eliminar productos

El usuario puede seleccionar un producto y presionar Eliminar.
Antes de eliminarlo, la aplicación muestra un mensaje de confirmación para evitar eliminaciones accidentales.
Buscar productos

La aplicación permite buscar productos mediante el campo de búsqueda.
La búsqueda se realiza utilizando el nombre del producto y permite encontrar coincidencias aunque el usuario no escriba el nombre completo.

Si no se encuentra ningún producto, se muestra el mensaje:
Producto no registrado.

Limpiar
El botón Limpiar permite borrar los datos de los campos de texto, quitar la imagen seleccionada y volver a mostrar todos los productos registrados.
Manejo de imágenes
Las imágenes seleccionadas para los productos se copian automáticamente a una carpeta llamada:

Imagenes
Esta carpeta se encuentra dentro de la carpeta de ejecución del proyecto.
En la base de datos solamente se almacena el nombre de la imagen, mientras que el archivo físico se guarda dentro de la carpeta Imagenes.
Esto permite mostrar posteriormente la imagen asociada al producto.

Base de datos
El proyecto utiliza SQLite para almacenar la información.
La base de datos se llama:
Productos.db

La tabla utilizada es:
Productos
La tabla contiene los siguientes campos:
Campo	Tipo	Descripción
Id	INTEGER	Identificador único del producto
Producto	TEXT	Nombre del producto
Precio	REAL	Precio del producto
Cantidad	INTEGER	Cantidad disponible
Imagen	TEXT	Nombre de la imagen almacenada
La tabla se crea automáticamente cuando se inicia el programa utilizando la instrucción CREATE TABLE IF NOT EXISTS.
Estructura principal del proyecto
Ejemplo_en_clase_1
│
├── Conexion.cs
├── Producto.cs
├── Form1.cs
├── Form1.Designer.cs
├── Productos.db
│
└── Imagenes
    └── imágenes de los productos
    
Conexion.cs
Contiene la configuración de la conexión con SQLite y el método encargado de crear la base de datos y la tabla Productos.
Producto.cs
Contiene la clase Producto, utilizada para representar la información de un producto.
Sus principales propiedades son:
•	Id
•	ProductoNombre
•	Precio
•	Cantidad
•	Imagen
Form1.cs
Contiene la lógica principal de la aplicación, incluyendo:
•	Guardar productos.
•	Modificar productos.
•	Eliminar productos.
•	Buscar productos.
•	Cargar productos.
•	Seleccionar imágenes.
•	Limpiar los campos.

Validaciones
La aplicación cuenta con algunas validaciones básicas para evitar errores en el ingreso de información.
Por ejemplo:
•	Los campos de nombre, precio y cantidad no pueden quedar vacíos.
•	El precio debe ser un número válido.
•	La cantidad debe ser un número entero.
•	Para modificar o eliminar es necesario seleccionar un producto.
•	Antes de eliminar se solicita confirmación.

Instalación y ejecución
Para ejecutar el proyecto se deben seguir estos pasos:
1.	Abrir el proyecto en Visual Studio.
2.	Restaurar los paquetes NuGet necesarios.
3.	Verificar que el proyecto esté configurado para ejecutarse en x86.
4.	Compilar el proyecto.
5.	Ejecutar la aplicación.
La base de datos Productos.db se crea automáticamente al iniciar el programa si todavía no existe.
La carpeta Imagenes también se crea automáticamente cuando se guarda un producto con una imagen.

Resultado
Al finalizar, se obtiene una aplicación de escritorio capaz de administrar productos mediante una interfaz sencilla. El usuario puede registrar productos, consultar la información almacenada, realizar búsquedas, modificar datos y eliminar registros.
Además, el proyecto permite trabajar con imágenes asociadas a cada producto, integrando el manejo de archivos con la base de datos SQLite.
Conclusión
Este proyecto permitió aplicar de manera práctica los conocimientos de C#, Windows Forms y bases de datos SQLite. A través del desarrollo del CRUD se pudo comprender mejor cómo conectar una aplicación con una base de datos, realizar consultas SQL y actualizar la información desde una interfaz gráfica.
También permitió trabajar con validaciones, eventos de Windows Forms y manejo de imágenes. En general, el proyecto ayudó a reforzar los conceptos necesarios para desarrollar aplicaciones de escritorio que puedan gestionar información de manera organizada.
