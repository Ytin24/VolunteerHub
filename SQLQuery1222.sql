-- Создание базы данных
CREATE DATABASE VolunteerDB;
GO

-- Использование базы данных VolunteerDB
USE VolunteerDB;
GO

-- Создание таблицы "Роли пользователей"
CREATE TABLE UserRoles (
    role_id INT PRIMARY KEY IDENTITY,
    role_name NVARCHAR(50) NOT NULL
);

-- Добавление ролей пользователей
INSERT INTO UserRoles (role_name) VALUES ('admin'), ('volunteer');

-- Создание таблицы "Пользователи"
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY,
    username NVARCHAR(50) NOT NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    password_hash NVARCHAR(255) NOT NULL,
    role_id INT NOT NULL,
    FOREIGN KEY (role_id) REFERENCES UserRoles(role_id)
);

-- Создание таблицы "Проекты"
CREATE TABLE Projects (
    project_id INT PRIMARY KEY IDENTITY,
    project_name NVARCHAR(100) NOT NULL,
    description NVARCHAR(MAX),
    start_date DATE,
    end_date DATE
);

-- Создание таблицы "Статусы проектов"
CREATE TABLE ProjectStatus (
    status_id INT PRIMARY KEY IDENTITY,
    status_name NVARCHAR(50) NOT NULL
);

-- Добавление статусов проектов
INSERT INTO ProjectStatus (status_name) VALUES ('active'), ('completed'), ('cancelled');

-- Создание таблицы "Проекты и их статусы"
CREATE TABLE ProjectProjectStatus (
    project_id INT NOT NULL,
    status_id INT NOT NULL,
    PRIMARY KEY (project_id, status_id),
    FOREIGN KEY (project_id) REFERENCES Projects(project_id),
    FOREIGN KEY (status_id) REFERENCES ProjectStatus(status_id)
);

-- Создание таблицы "Задачи"
CREATE TABLE Tasks (
    task_id INT PRIMARY KEY IDENTITY,
    project_id INT NOT NULL,
    task_name NVARCHAR(100) NOT NULL,
    description NVARCHAR(MAX),
    assigned_to INT,
    status NVARCHAR(50) DEFAULT 'assigned' NOT NULL,
    FOREIGN KEY (project_id) REFERENCES Projects(project_id),
    FOREIGN KEY (assigned_to) REFERENCES Users(user_id)
);

-- Создание таблицы "Участие в проектах"
CREATE TABLE ProjectParticipation (
    participation_id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL,
    project_id INT NOT NULL,
    participation_date DATE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (project_id) REFERENCES Projects(project_id)
);

-- Создание таблицы "Отчеты"
CREATE TABLE Reports (
    report_id INT PRIMARY KEY IDENTITY,
    task_id INT NOT NULL,
    user_id INT NOT NULL,
    report_date DATE,
    description NVARCHAR(MAX),
    attachment NVARCHAR(255),
    FOREIGN KEY (task_id) REFERENCES Tasks(task_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);
