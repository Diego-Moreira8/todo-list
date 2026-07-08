CREATE TABLE Todos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DescriptionText NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
)

SELECT * FROM Todos

INSERT INTO Todos (DescriptionText) VALUES ('Tarefa inserida via query')


