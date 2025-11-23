# 📘 Sistema de Administración de Salas de Sistemas – ASP.NET Core MVC

Este proyecto es una aplicación web desarrollada en ASP.NET Core MVC que permite la administración integral de las salas de sistemas de una universidad. Incluye módulos para salas, equipos, usuarios, préstamos, reportes de daños y asesorías técnicas, todo con control de acceso basado en roles.

# 🚀 Características principales
🔐 Autenticación y Roles

- Implementado con ASP.NET Identity, con los roles:

- Administrador

- Coordinador de Sala

- Usuario

🏫 Gestión de Salas

- Crear, editar y eliminar salas.

- Definir ubicación, capacidad y estado.

- Ver disponibilidad diaria y semanal.

💻 Gestión de Equipos

- Registrar equipos con serial y estado.

- Asignar equipos a salas.

- Bloquear, liberar o modificar equipos.

- Control visual de ocupación.

📅 Préstamos de Equipos y Salas

- Solicitud de préstamo por parte del usuario.

- Flujo de aprobación por coordinadores.

- Historial y estado de solicitudes.

🛠️ Reportes de Daños

- Registrar daños en salas o equipos.

- Seguimiento del estado del reporte.

- Atención de reportes por coordinadores.

👨‍🏫 Asesorías Técnicas

- Solicitud de asesorías por usuarios.

- Gestión del estado y atención.
  
# 🧬 Modelo de Datos

El sistema utiliza una arquitectura MVC con Entity Framework Core y migraciones para gestionar la base de datos SQL Server.

Modelos principales:

- Usuario

- Sala

- Equipo

- Prestamo_Equipo

- Prestamo_Sala

- Reporte

- Asesoria

# 🛠️ Tecnologías Utilizadas

| Área                 | Tecnologías           |
| -------------------- | --------------------- |
| Backend              | ASP.NET Core MVC 6+   |
| ORM                  | Entity Framework Core |
| BD                   | SQL Server            |
| Seguridad            | ASP.NET Identity      |
| Frontend             | Bootstrap 5, jQuery   |
| Control de Versiones | Git + GitHub          |
| Gestión del Proyecto | Trello                |

# 📂 Estructura del Proyecto
```bash
/Controllers
/Domain
/Infrastructure
/Views
/wwwroot
```
Incluye separación por capas: Domain, Application, Infrastructure, MvcSample (Presentación).

# ⚙️ Instalación y Configuración

1️⃣ Clonar el repositorio:

```bash
git clone https://github.com/DavidAl12/Administracion_Salas_OF.git
cd Administracion_Salas_OF
```
2️⃣ Configurar la base de datos

Editar appsettings.json:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=SalasDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
3️⃣ Aplicar migraciones
```bash
Add-Migration "Nombre migracion"
Update-Database
```
4️⃣ Ejecutar el proyecto
```bash
Run DataBase 
```
or
```bash
Ctrl + F5
```
# 👥 Autores

Proyecto desarrollado por estudiantes de la facultad de Ingeniería de Sistemas de la Universidad Santiago De Cali:
- Andres Silva Muñoz 
- Arley David Alpala Benavides
- Catalina Estrada Rivas
- Juan Felipe Valdez Muñoz
