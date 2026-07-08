DROP TABLE Todos


CREATE TABLE Todos (
    Id INT PRIMARY KEY,
    DescriptionText NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
)


INSERT INTO Todos (Id, DescriptionText) 
VALUES 
    (1, 'Tarefa inserida via query'),
    (2, 'Tarefa inserida via query'),
    (3, 'Tarefa inserida via query')


SELECT * FROM Todos

SELECT MAX(Id) FROM Todos
