-- (1) build and populate a persisted (numbers) tally 
IF OBJECT_ID('dbo.tally') IS NOT NULL DROP TABLE dbo.tally;
CREATE TABLE dbo.tally (n int not null);

WITH DummyRows(V) AS(SELECT 1 FROM (VALUES (1),(1),(1),(1),(1),(1),(1),(1),(1),(1)) t(N))
INSERT dbo.tally
SELECT TOP (8000) ROW_NUMBER() OVER (ORDER BY (SELECT 1))
FROM DummyRows a CROSS JOIN DummyRows b CROSS JOIN DummyRows c CROSS JOIN DummyRows d;

-- (2) Add Required constraints (and indexes) for performance
ALTER TABLE dbo.tally 
ADD CONSTRAINT pk_tally PRIMARY KEY CLUSTERED(N) WITH FILLFACTOR = 100;

ALTER TABLE dbo.tally 
ADD CONSTRAINT uq_tally UNIQUE NONCLUSTERED(N);



--DECLARE @s1 varchar(8000) = 'abcd';

--SELECT
--  position  = t.N,
--  tokenSize = x.N,
--  string    = substring(@s1, t.N, x.N)  
--FROM       dbo.tally t -- token position
--CROSS JOIN dbo.tally x -- token length
--WHERE t.N <=  len(@s1) -- all positions
--AND   x.N <=  len(@s1) -- all lengths
--AND   len(@s1) - t.N - (x.N-1) >= 0 -- filter unessesary rows [e.g.substring('abcd',3,2)]

go

IF OBJECT_ID('dbo.getshortstring8k') IS NOT NULL DROP FUNCTION dbo.getshortstring8k;
GO
CREATE FUNCTION dbo.getshortstring8k(@s1 varchar(8000), @s2 varchar(8000))
RETURNS TABLE WITH SCHEMABINDING AS RETURN 
SELECT s1 = CASE WHEN LEN(@s1) < LEN(@s2) THEN @s1 ELSE @s2 END,
       s2 = CASE WHEN LEN(@s1) < LEN(@s2) THEN @s2 ELSE @s1 END;
	   go


--	   DECLARE @s1 varchar(8000) = 'bcdabc', @s2 varchar(8000) = 'abcd';

--SELECT
--  s.s1, -- test to make sure s.s1 is the shorter of the two strings
--  position  = t.N,
--  tokenSize = x.N,
--  string    = substring(s.s1, t.N, x.N)
--FROM dbo.getshortstring8k(@s1, @s2) s --<< get the shorter string
--CROSS JOIN dbo.tally t  
--CROSS JOIN dbo.tally x
--WHERE t.N between 1 and len(s.s1)
--AND   x.N between 1 and len(s.s1)
--AND   len(s.s1) - t.N - (x.N-1) >= 0
--AND   charindex(substring(s.s1, t.N, x.N), s.s2) > 0;

--go

DECLARE @s1 varchar(8000) = 'https://mail.google.com/mail/u/1/#inbox', @s2 varchar(8000) = 'google.com';

SELECT TOP (1) WITH TIES 
  position  = t.N,
  tokenSize = x.N,
  string    = substring(s.s1, t.N, x.N)
FROM dbo.getshortstring8k(@s1, @s2) s
CROSS JOIN dbo.tally t  
CROSS JOIN dbo.tally x
WHERE t.N between 1 and len(s.s1)
AND   x.N between 1 and len(s.s1)
AND   len(s.s1) - t.N - (x.N-1) >= 0
AND   charindex(substring(s.s1, t.N, x.N), s.s2) > 0
ORDER BY x.N DESC;