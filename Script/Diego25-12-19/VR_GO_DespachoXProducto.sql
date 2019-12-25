SELECT        e.oaccbnumi, c.canumi, c.cacod, c.cadesc, c.cadesc2, d.cadesc AS categoria, SUM(b.obpcant) AS obpcant, F.chprecio, SUM(b.obpcant) * F.chprecio AS Total,e.oacnconc
FROM            dbo.TO001 AS a INNER JOIN
                         dbo.TO0011 AS b ON a.oanumi = b.obnumi INNER JOIN
                         dbo.TC001 AS c ON b.obcprod = c.canumi INNER JOIN
                         dbo.TC005C AS d ON c.cacat = d.canumi INNER JOIN
                         dbo.TO001C AS e ON a.oanumi = e.oacoanumi INNER JOIN
						 dbo.TC003 AS f ON f.chcprod = C.canumi AND F.chcatcl = 2
WHERE  e.oaccbnumi = 10 and a.oaest < 3
GROUP BY e.oaccbnumi, c.canumi, c.cacod, c.cadesc, c.cadesc2, d.cadesc, f.chprecio,e.oacnconc


SELECT * FROM TO0011
SELECT * FROM TO001C
SELECT * FROM TC003 WHERE chcatcl = 2 --Precio
SELECT * FROM TC001

SELECT        e.oaccbnumi, c.canumi, c.cacod, c.cadesc, c.cadesc2, d.cadesc AS categoria, SUM(b.obpcant) AS obpcant,e.oacnconc
FROM            dbo.TO001 AS a INNER JOIN
                         dbo.TO0011 AS b ON a.oanumi = b.obnumi INNER JOIN
                         dbo.TC001 AS c ON b.obcprod = c.canumi INNER JOIN
                         dbo.TC005C AS d ON c.cacat = d.canumi INNER JOIN
                         dbo.TO001C AS e ON a.oanumi = e.oacoanumi 
GROUP BY e.oaccbnumi, c.canumi, c.cacod, c.cadesc, c.cadesc2, d.cadesc,e.oacnconc

SELECT *  FROM  TC002 A WHERE A.cbcat = 1

SELECT        e.oaccbnumi, c.canumi, c.cacod, c.cadesc, c.cadesc2, d.cadesc AS categoria, SUM(b.obpcant) AS obpcant, B.obpbase,SUM(b.obptot) as Total
FROM            dbo.TO001 AS a INNER JOIN
                         dbo.TO0011 AS b ON a.oanumi = b.obnumi INNER JOIN
                         dbo.TC001 AS c ON b.obcprod = c.canumi INNER JOIN
                         dbo.TC005C AS d ON c.cacat = d.canumi INNER JOIN
                         dbo.TO001C AS e ON a.oanumi = e.oacoanumi 
WHERE  e.oaccbnumi = 10 and a.oaest < 3
GROUP BY e.oaccbnumi, c.canumi, c.cacod, c.cadesc, c.cadesc2, d.cadesc, b.obpbase,b.obptot

SELECT        e.oaccbnumi, c.ccnumi, c.cccod, c.ccdesc, ISNULL(b.oacnrofac, 0) AS oacnrofact, SUM(d.obptot) AS obptot
FROM            dbo.TO001 AS a INNER JOIN
                         dbo.TO001C AS b ON a.oanumi = b.oacoanumi INNER JOIN
                         dbo.TC004 AS c ON a.oaccli = c.ccnumi INNER JOIN
                         dbo.TO0011 AS d ON a.oanumi = d.obnumi INNER JOIN
                         dbo.TO001C AS e ON a.oanumi = e.oacoanumi
WHERE a.oaest < 3
GROUP BY e.oaccbnumi, c.ccnumi, c.cccod, c.ccdesc, b.oacnrofac