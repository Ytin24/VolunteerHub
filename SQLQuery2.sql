-- Вставка данных в таблицу UserRoles
INSERT INTO UserRoles (role_name) VALUES ('admin');
INSERT INTO UserRoles (role_name) VALUES ('volunteer');

-- Вставка данных в таблицу Users
INSERT INTO Users (username, email, password_hash, role_id) VALUES ('admin1', 'admin1@example.com', 'hashed_password', 1);
INSERT INTO Users (username, email, password_hash, role_id) VALUES ('user1', 'user1@example.com', 'hashed_password', 2);
INSERT INTO Users (username, email, password_hash, role_id) VALUES ('user2', 'user2@example.com', 'hashed_password', 2);

-- Вставка данных в таблицу ProjectStatus
INSERT INTO ProjectStatus (status_name) VALUES ('active');
INSERT INTO ProjectStatus (status_name) VALUES ('completed');
INSERT INTO ProjectStatus (status_name) VALUES ('cancelled');

-- Вставка данных в таблицу Projects
INSERT INTO Projects (project_name, description, start_date, end_date) VALUES ('Project 1', 'Description of Project 1', '2024-04-01', '2024-04-30');
INSERT INTO Projects (project_name, description, start_date, end_date) VALUES ('Project 2', 'Description of Project 2', '2024-05-01', '2024-05-31');

-- Вставка данных в таблицу Tasks
INSERT INTO Tasks (project_id, task_name, description, assigned_to, status) VALUES (1, 'Task 1 for Project 1', 'Description of Task 1 for Project 1', 2, 'assigned');
INSERT INTO Tasks (project_id, task_name, description, assigned_to, status) VALUES (1, 'Task 2 for Project 1', 'Description of Task 2 for Project 1', 3, 'assigned');
INSERT INTO Tasks (project_id, task_name, description, assigned_to, status) VALUES (2, 'Task 1 for Project 2', 'Description of Task 1 for Project 2', 2, 'assigned');

-- Вставка данных в таблицу ProjectParticipation
INSERT INTO ProjectParticipation (user_id, project_id, participation_date) VALUES (2, 1, '2024-04-01');
INSERT INTO ProjectParticipation (user_id, project_id, participation_date) VALUES (3, 1, '2024-04-02');
INSERT INTO ProjectParticipation (user_id, project_id, participation_date) VALUES (2, 2, '2024-05-01');

-- Вставка данных в таблицу Reports
INSERT INTO Reports (task_id, user_id, report_date, description, attachment) VALUES (1, 2, '2024-04-05', 'Report for Task 1 for Project 1', 'report1.pdf');
INSERT INTO Reports (task_id, user_id, report_date, description, attachment) VALUES (2, 3, '2024-04-07', 'Report for Task 2 for Project 1', 'report2.pdf');
INSERT INTO Reports (task_id, user_id, report_date, description, attachment) VALUES (3, 2, '2024-05-05', 'Report for Task 1 for Project 2', 'report3.pdf');
