Sistema de Gestión de Empleados (SGE)

Descripción General
El Sistema de Gestión de Empleados (SGE) es una aplicación de escritorio diseñada para la administración eficiente de la información del personal. La aplicación permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre los registros de empleados y departamentos, facilitando la gestión de la base de datos de recursos humanos de una empresa.

La arquitectura del proyecto está construida para ser modular, extensible y mantenible, asegurando que el sistema sea robusto y escalable para futuras funcionalidades.

Características Principales

Gestión de Empleados:

-Creación de nuevos registros de empleados.

-Actualización de información de empleados existentes.

-Eliminación de empleados.

-Visualización y búsqueda de empleados por ID o nombre.

Arquitectura de 3 Capas: Clara separación entre la interfaz de usuario, la lógica de negocio y la capa de acceso a datos.

Seguridad: Uso de comandos SQL parametrizados para prevenir ataques de inyección SQL.

Manejo de Errores: Implementación de excepciones personalizadas para mensajes de error claros y específicos para el usuario.

Patrón de Repositorio: Desacoplamiento de la lógica de negocio de la base de datos, lo que permite cambiar la tecnología de la base de datos sin alterar las capas superiores.

Tecnologías Utilizadas

-Lenguaje de Programación: C# (o VB.NET, dependiendo de tu implementación)

-Framework: .NET Framework (Windows Forms)

-Base de Datos: SQL Server

-Arquitectura: 3-Tier Architecture (3 capas)

-Patrones de Diseño: Patrón de Repositorio

Estructura del Proyecto

El proyecto está organizado en las siguientes capas, siguiendo la arquitectura de 3 capas:

-SGE.UI: Capa de la interfaz de usuario. Contiene los formularios de la aplicación.

-SGE.BLL: Capa de la lógica de negocio. Contiene las reglas y validaciones de la aplicación.

-SGE.DAL: Capa de acceso a datos. Se encarga de la comunicación con la base de datos.

-SGE.Entities: Contiene las entidades (modelos de datos) compartidas entre las capas, como Empleado.vb y Departamento.vb.

Configuración y Despliegue

Requisitos

-Visual Studio (2019 o superior)

-SQL Server (Express, LocalDB, o cualquier versión)

Configuración de la Base de Datos

Asegúrate de que tu instancia de SQL Server esté en funcionamiento.

Ejecuta el script SQL proporcionado en el proyecto para crear la base de datos y las tablas (SGE_DB).

Abre el archivo App.config en la capa de la UI y actualiza la cadena de conexión (connectionString) para que apunte a tu instancia de SQL Server.

<connectionStrings>
  <add name="SGE.DB.Connection" connectionString="Data Source=TU_SERVIDOR;Initial Catalog=SGE_DB;User ID=TU_USUARIO;Password=TU_PASSWORD;" providerName="System.Data.SqlClient" />
</connectionStrings>
    
Ejecutar la Aplicación

-Abre la solución (.sln) en Visual Studio.
-Compila la solución para restaurar las dependencias.
-Ejecuta el proyecto (SGE.UI).

Contribuciones
Este proyecto fue desarrollado por Lady Johana Suárez Esquinas.
