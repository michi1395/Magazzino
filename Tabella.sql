CREATE TABLE Prodotti (
	ID int IDENTITY (1,1) NOT NULL,
	CodiceProdotto nvarchar(20) UNIQUE NOT NULL,
	Categoria nvarchar(50) NOT NULL,
	Descrizione nvarchar(500) NOT NULL,
	PrezzoUnitario numeric(10,2) NOT NULL,
	QuantitaDisponibile int NOT NULL,
	CONSTRAINT PK_Prodotti PRIMARY KEY (ID),
	CONSTRAINT CK_Categoria CHECK (Categoria IN ('Alimentari','Cancelleria','Sanitari','Elettronica'))
)

SELECT * FROM Prodotti
WHERE QuantitaDisponibile < 10

SELECT 
 Categoria
 ,COUNT(*) AS [# Prodotti]
 FROM Prodotti
 GROUP BY Categoria
