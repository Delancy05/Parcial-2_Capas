# Parcial Práctico: Laboratorio de MVP (Login + MDI + Juego de Craps)

**Universidad Tecnológica de Panamá**

Facultad de Ingeniería en Sistemas · Campus Víctor Levis Sasso

**Materia:** Herramientas de la Programación Aplicada III (.NET)

**Grupo:** 1IL133 · **Semestre:** II Semestre 2026

**Instructora:** Ing. Irina Fong

---

## 👤 Detalle del autor

| Campo | Información |
|---|---|
| **Nombre** | Chelsy Rios Garcia |
| **Correo** | chelsy.rios@utp.ac.pa |
| **GitHub** | [Delancy05](https://github.com/Delancy05) |
| **Fecha** | 8 de noviembre de 2026 |

---

## 📋 Descripción

Aplicación de escritorio en **Windows Forms (.NET Framework)** desarrollada con arquitectura en capas y el patrón de diseño **MVP (Modelo - Vista - Presentador)**. Incluye:

- Registro e inicio de sesión de usuarios con **contraseñas protegidas con hash (BCrypt)**.
- Conexión a **MySQL** con un usuario de **privilegios mínimos**.
- Formulario principal **MDI** con **menú lateral izquierdo**, **hora actual** y **usuario en sesión** en la parte inferior.
- **Juego de Craps** como formulario hijo, también implementado en MVP.
- **Validaciones** de entrada en el login.

---

## 🖼️ Imágenes de resultado

### MDI (menú lateral, hora y usuario en sesión)

<img width="1917" height="1078" alt="Captura de pantalla 2026-11-08 232601" src="https://github.com/user-attachments/assets/204ae6e8-c2c3-479b-9fda-f1402537b351" />

### Juego de Craps

<img width="1130" height="1073" alt="Captura de pantalla 2026-11-08 232833" src="https://github.com/user-attachments/assets/4bf82d5a-e7cf-41e4-a5e5-a6f9a04c1943" />

### Validaciones

<img width="1130" height="1078" alt="Captura de pantalla 2026-11-08 230819" src="https://github.com/user-attachments/assets/bc6f6c97-e676-48f6-93a6-409b08a98edf" />

---

## 🛠️ Tecnologías y versiones

| Tecnología | Versión |
|---|---|
| Lenguaje | C# |
| Framework | .NET Framework 4.8 |
| Interfaz | Windows Forms |
| IDE | Visual Studio Community 2026 (18.10.3) |
| Base de datos | MySQL Server |
| Cliente de BD | MySQL Workbench |
| Conector MySQL | MySql.Data 26.10.0 |
| Hash de contraseñas | BCrypt.Net-Next 4.2.0 |
| Control de versiones | Git + GitHub |

---

## 🏗️ Arquitectura

La solución tiene 4 proyectos:

```
Parcial 2_Capas (solución)
├── Entidades          → Clase Usuario (Id, Correo, PasswordHash)
├── Datos              → ConexionBD, IUsuarioRepositorio, UsuarioRepositorio
├── Negocio            → IHashService, IVerificarHashService, BcryptPasswordService
└── Parcial 2_Capas    → Vistas (FrmLogin, FrmPrincipal, FrmCraps) y Presentadores
```

### Patrón MVP

| Componente | Responsabilidad | Clases |
|---|---|---|
| **Modelo** | Datos y lógica de negocio | `Usuario`, `UsuarioRepositorio`, `BcryptPasswordService` |
| **Vista** | Solo muestra controles y avisa al presentador | `FrmLogin` (`ILoginView`), `FrmCraps` (`ICrapsView`) |
| **Presentador** | Intermediario: valida, consulta y actualiza la vista | `LoginPresenter`, `CrapsPresenter` |

### Dos contratos independientes (capa de Negocio)

- `IHashService`: única responsabilidad, **generar el hash**.
- `IVerificarHashService`: única responsabilidad, **verificar** si una contraseña en texto plano coincide con el hash almacenado.

Ambos los implementa `BcryptPasswordService`.

### Conexión con privilegios mínimos

La aplicación **no usa `root`**. Se conecta con el usuario `app_craps`, que solo tiene `SELECT` e `INSERT` sobre la tabla `usuarios`. Las consultas usan **parámetros** para evitar inyección SQL.

---

## 🧩 Controles utilizados

| Control | Uso |
|---|---|
| `Form` (con `IsMdiContainer = true`) | Contenedor MDI (`FrmPrincipal`) |
| `Panel` (`Dock = Left`) | Menú lateral |
| `Button` | Iniciar sesión, Registrar, Juego de Craps, Salir, Lanzar dados, Reiniciar |
| `Label` | Hora actual, usuario en sesión, dados, punto y resultado |
| `Timer` | Actualiza la hora cada segundo |
| `TextBox` (`PasswordChar = '*'`) | Correo y contraseña |
| `ListBox` | Historial de lanzamientos del Craps |
| `MessageBox` | Mensajes de validación y avisos |

---

## ⚙️ Proceso de instalación

### Requisitos

- Windows con **Visual Studio 2022 o superior** (carga de trabajo *Desarrollo de escritorio .NET*)
- **MySQL Server** y **MySQL Workbench**
- **Git**

### Pasos

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/Delancy05/Parcial-2_Capas.git
   ```

2. **Crear la base de datos.** En MySQL Workbench, con el usuario `root`, ejecutar:
   ```sql
   CREATE DATABASE craps_db;
   USE craps_db;

   CREATE TABLE usuarios (
       id INT AUTO_INCREMENT PRIMARY KEY,
       correo VARCHAR(100) NOT NULL UNIQUE,
       password_hash VARCHAR(255) NOT NULL
   );

   CREATE USER 'app_craps'@'localhost' IDENTIFIED BY 'Clave123!';
   GRANT SELECT, INSERT ON craps_db.usuarios TO 'app_craps'@'localhost';
   FLUSH PRIVILEGES;
   ```

3. **Abrir la solución** `Parcial 2_Capas.slnx` en Visual Studio.
4. **Restaurar paquetes NuGet** (automático al compilar): `MySql.Data` y `BCrypt.Net-Next`.
5. **Establecer como proyecto de inicio** el proyecto `Parcial 2_Capas`.
6. Compilar (`Ctrl + Mayús + B`) y ejecutar (`F5`).

### Uso

1. En el login, ingresar un correo y una contraseña (mínimo 6 caracteres) y presionar **Registrar**.
2. Presionar **Iniciar sesión**.
3. En el menú lateral, abrir **Juego de Craps** y presionar **Lanzar dados**.

### Reglas del Craps

- Primer lanzamiento: suma **7 u 11** gana; suma **2, 3 o 12** pierde.
- Cualquier otra suma se convierte en el **punto**: se sigue lanzando hasta sacar el punto (gana) o un 7 (pierde).

---

## 📝 Documentación del código

Todas las clases, interfaces y métodos públicos están documentados con comentarios XML (`/// <summary>`).
