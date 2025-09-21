CREATE DATABASE SGE_DB;
GO
USE SGE_DB;
GO
CREATE TABLE Departamentos (
    IdDepartamento INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);
CREATE TABLE Empleados (
    IdEmpleado INT IDENTITY PRIMARY KEY,
    NombreCompleto NVARCHAR(200) NOT NULL,
    FechaContratacion DATE NOT NULL,
    Cargo NVARCHAR(100) NOT NULL,
    Salario DECIMAL(18,2) NOT NULL,
    IdDepartamento INT NOT NULL FOREIGN KEY REFERENCES Departamentos(IdDepartamento)
);
INSERT INTO Departamentos (Nombre) VALUES ('Recursos Humanos'), ('Tecnología');
INSERT INTO Empleados (NombreCompleto, FechaContratacion, Cargo, Salario, IdDepartamento)
VALUES ('Juan Pérez', '2020-01-15', 'Analista', 2500, 1),
       ('María Gómez', '2021-06-10', 'Desarrolladora', 3200, 2),
       ('Carlos Ruiz', '2022-09-01', 'Administrador BD', 4000, 2);
